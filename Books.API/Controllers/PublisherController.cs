using Books.API;
using Books.API.Constants;
using Books.API.Repositories;
using Books.Shared.Constants;
using Books.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IKafkaProducerService _kafkaProducer;
        private readonly ApiEndpoints _apiEndpoints;

        public PublisherController(
            IPublisherRepository publisherRepository,
            IPublishEndpoint publishEndpoint,
            IKafkaProducerService kafkaProducer,
            ApiEndpoints apiEndpoints
        )
        {
            _publisherRepository = publisherRepository;
            _publishEndpoint = publishEndpoint;
            _kafkaProducer = kafkaProducer;
            _apiEndpoints = apiEndpoints;
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

            var publisherDto = publisher.ToDto();

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
                var publisher = new Publisher(dto);
                await _publisherRepository.Update(publisher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PublisherExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // I would actually log this in prod and not writeline it
                WriteLine(ex);
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(PublisherDto publisherDto)
        {
            var publisher = new Publisher(publisherDto);

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
}
