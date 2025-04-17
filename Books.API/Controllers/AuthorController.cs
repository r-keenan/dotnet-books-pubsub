using Books.API.Constants;
using Books.API.Models.Mappers.Interfaces;
using Books.API.Repositories;
using Books.Common.Constants;
using Books.Common.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IKafkaProducerService _kafkaProducer;
        private readonly ApiEndpoints _apiEndpoints;
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorMapper _mapper;

        public AuthorController(
            IAuthorRepository authorRepository,
            IPublishEndpoint publishEndpoint,
            IKafkaProducerService kafkaProducer,
            ApiEndpoints apiEndpoints,
            ILogger<AuthorController> logger,
            IAuthorMapper mapper
        )
        {
            _authorRepository = authorRepository;
            _publishEndpoint = publishEndpoint;
            _kafkaProducer = kafkaProducer;
            _apiEndpoints = apiEndpoints;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        // TODO: Add Pagination to endpoint
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _authorRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _authorRepository.Get(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorDto = _mapper.ToDto(author);

            return authorDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var author = _mapper.ToEntity(dto);
                await _authorRepository.Update(author);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AuthorExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(
                        ex,
                        "Concurrency conflict occurred while updating author {AuthorId}. Error: {ErrorMessage}",
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
                    "Unexpected error occurred while updating author {AuthorId}. Error: {ErrorMessage}",
                    id,
                    ex.Message
                );
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorDto dto)
        {
            var author = _mapper.ToEntity(dto);

            var newAuthor = await _authorRepository.Add(author);

            var authorMessage = new AuthorMessage()
            {
                Id = newAuthor.Id,
                FirstName = newAuthor.FirstName,
                MiddleName = newAuthor.MiddleName,
                LastName = newAuthor.LastName,
                DateOfBirth = newAuthor.DateOfBirth,
                WritingAwards = newAuthor.WritingAwards,
            };

            // Publish to RabbitMQ with MassTransit
            await _publishEndpoint.Publish(authorMessage);

            // Publish to Kafka Topic
            await _kafkaProducer.ProduceAsync(KafkaTopics.AuthorsTopic, newAuthor);

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorRepository.Get(id);
            if (author == null)
                return NotFound();

            await _authorRepository.Delete(id);

            return NoContent();
        }

        private async Task<bool> AuthorExistsAsync(int id)
        {
            return await _authorRepository.Exists(id);
        }
    }
}
