using Books.API.Dtos;
using Books.API.Models.Mappers.Interfaces;
using Books.API.Repositories;
using Books.Common.Constants;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Controllers;

public class BaseController<TEntity, TDto, TMapper> : ControllerBase where TEntity : BaseModel
where TDto : BaseDto
where TMapper : IBaseMapper<TEntity, TDto>

{
    private readonly IBaseRepository<TEntity> _baseRepository;
    private readonly TMapper _mapper;
    private readonly ILogger<BaseController<TEntity, TDto, TMapper>> _logger;
    private readonly IKafkaProducerService _kafkaProducer;
    private readonly IPublishEndpoint _publishEndpoint;

    public BaseController(IBaseRepository<TEntity> baseRepository, TMapper mapper, ILogger<BaseController<TEntity, TDto, TMapper>> logger, IKafkaProducerService kafkaProducer, IPublishEndpoint publishEndpoint)
    {
        _baseRepository = baseRepository;
        _logger = logger;
        _mapper = mapper;
        _kafkaProducer = kafkaProducer;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    // Extend to have pagination
    public async Task<ActionResult<IEnumerable<TEntity>>> GetEntities()
    {
        return await _baseRepository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> GetEntity(int id)
    {
        var entity = await _baseRepository.Get(id);

        if (entity is null)
        {
            return NotFound();
        }

        var dto = _mapper.ToDto(entity);

        return dto;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEntity(int id, TDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }


        var entity = _mapper.ToEntity(dto);
        try
        {
            await _baseRepository.Update(entity);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await EntityExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                _logger.LogError(
                    ex,
                    $"Concurrency conflict occurred while updating {typeof(TEntity)} {entity.Id}. Error: {ex}",
                    id,
                    ex.Message
                );
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unexpected error occurred while updating publisher {PublisherId}. Error: {ErrorMessage}",
                id,
                ex.Message
            );
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisher(TDto dto)
    {
        var entity = _mapper.ToEntity(dto);

        var newEntity = await _baseRepository.Add(entity);

        var rabbitMqMessage = new PublisherMessage()
        {
            Id = newPublisher.Id,
            Name = newPublisher.Name,
            Address1 = newPublisher.Address1,
            Address2 = newPublisher.Address2,
            City = newPublisher.City,
            State = newPublisher.State,
            ZipCode = newPublisher.ZipCode,
            DateFounded = newPublisher.DateFounded,
        };

        // Publish to RabbitMQ with MassTransit
        await _publishEndpoint.Publish(rabbitMqMessage);

        // Publish to Kafka Topic
        await _kafkaProducer.ProduceAsync(KafkaTopics.PublishersTopic, newEntity);

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var entity = await _baseRepository.Get(id);
        if (entity == null)
            return NotFound();

        await _baseRepository.Delete(id);

        return NoContent();
    }

    private async Task<bool> EntityExistsAsync(int id)
    {
        return await _baseRepository.Exists(id);
    }
}
