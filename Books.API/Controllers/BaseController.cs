using Books.API.Dtos;
using Books.API.Models.Mappers.Interfaces;
using Books.API.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Controllers;

public class BaseController<TEntity, TDto, TMapper> : ControllerBase where TEntity : BaseModel
where TDto : BaseDto
where TMapper : IBaseMapper<TEntity, TDto, TMessage>

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
                    $"Concurrency conflict occurred while updating {typeof(TEntity).Name} {entity.Id}. Error: {ex.Message}",
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
                $"Unexpected error occurred while updating {typeof(TEntity).Name} {entity.Id}. Error: {ex.Message}",
                id,
                ex.Message
            );
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntity(TDto dto)
    {
        var entity = _mapper.ToEntity(dto);

        var newEntity = await _baseRepository.Add(entity);

        // Get type of DTO
        string entityTypeName = typeof(TEntity).Name;

        // Map a DTO to a message
        var entityMessage = _mapper.ToMessage(dto);

        // Publish to RabbitMQ with MassTransit
        await _publishEndpoint.Publish(entityMessage);

        string topicName = GetKafkaTopicForEntityType(entityTypeName);
        // Publish to Kafka Topic
        await _kafkaProducer.ProduceAsync(topicName, newEntity);

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEntity(int id)
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

    private string GetKafkaTopicForEntityType(string entityTypeName)
    {
        // This method would return the appropriate Kafka topic based on entity type
        switch (entityTypeName)
        {
            case "Publisher":
                return KafkaTopics.PublishersTopic;
            case "Book":
                return KafkaTopics.BooksTopic;
            default:
                return KafkaTopics.AuthorsTopic;
        }
    }
}
