using Books.API;
using Books.API.Constants;
using Books.API.Repositories;
using Books.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
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
        private readonly ApiEndpoints _apiEndpoints;

        public PublisherController(IPublisherRepository publisherRepository, IPublishEndpoint publishEndpoint, ApiEndpoints apiEndpoints)
        {
            _publisherRepository = publisherRepository;
            _publishEndpoint = publishEndpoint;
            _apiEndpoints = apiEndpoints;
        }

        [HttpGet]
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
            var publisherMessage = new PublisherMessage()
            {
                Name = publisherDto.Name,
                Address1= publisherDto.Address1,
                Address2 = publisherDto.Address2,
                City = publisherDto.City,
                State = publisherDto.State,
                ZipCode = publisherDto.ZipCode,
                DateFounded = publisherDto.DateFounded
            };

            // Publish to RabbitMQ with MassTransit
            await _publishEndpoint.Publish(publisherMessage);

            return Ok();
        }

        private async Task<bool> PublisherExistsAsync(int id)
        {
            return await _publisherRepository.Exists(id);
        }
    }
}
