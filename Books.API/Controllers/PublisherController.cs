using Books.API.Constants;
using Books.API.Models.Mappers;
using Books.API.Models.Mappers.Interfaces;
using Books.API.Repositories;
using Books.Common.Constants;
using Books.Common.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublisherController : BaseController<Publisher, PublisherDto, PublisherMapper>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IKafkaProducerService _kafkaProducer;
    private readonly ApiEndpoints _apiEndpoints;
    private readonly ILogger<PublisherController> _logger;
    private readonly IPublisherMapper _mapper;

    public PublisherController(
        IPublisherRepository publisherRepository,
        IPublishEndpoint publishEndpoint,
        IKafkaProducerService kafkaProducer,
        ApiEndpoints apiEndpoints,
        ILogger<PublisherController> logger,
        IPublisherMapper mapper
    ) : base(publisherRepository, mapper, logger)
    {
        _publisherRepository = publisherRepository;
        _publishEndpoint = publishEndpoint;
        _kafkaProducer = kafkaProducer;
        _apiEndpoints = apiEndpoints;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    // TODO: Add Pagination to endpoint
    public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
    {
        return await _publisherRepository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
    {
        var publisher = await _publisherRepository.Get(id);

        if (publisher == null)
        {
            return NotFound();
        }

        var publisherDto = _mapper.ToDto(publisher);

        return publisherDto;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPublisher(int id, PublisherDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        try
        {
            var publisher = _mapper.ToEntity(dto);
            await _publisherRepository.Update(publisher);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await PublisherExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                _logger.LogError(
                    ex,
                    "Concurrency conflict occurred while updating publisher {PublisherId}. Error: {ErrorMessage}",
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
    public async Task<IActionResult> CreatePublisher(PublisherDto publisherDto)
    {
        var publisher = _mapper.ToEntity(publisherDto);

        var newPublisher = await _publisherRepository.Add(publisher);

        var publisherMessage = new PublisherMessage()
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
        await _publishEndpoint.Publish(publisherMessage);

        // Publish to Kafka Topic
        await _kafkaProducer.ProduceAsync(KafkaTopics.PublishersTopic, newPublisher);

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var publisher = await _publisherRepository.Get(id);
        if (publisher == null)
            return NotFound();

        await _publisherRepository.Delete(id);

        return NoContent();
    }

    private async Task<bool> PublisherExistsAsync(int id)
    {
        return await _publisherRepository.Exists(id);
    }
}

