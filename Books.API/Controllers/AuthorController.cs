using Books.API.Constants;
using Books.API.Repositories;
using Books.Shared.Messages;
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
        private readonly ApiEndpoints _apiEndpoints;

        public AuthorController(IAuthorRepository authorRepository, IPublishEndpoint publishEndpoint, ApiEndpoints apiEndpoints)
        {
            _authorRepository = authorRepository;
            _publishEndpoint = publishEndpoint;
            _apiEndpoints = apiEndpoints;
        }

        [HttpGet]
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

            var authorDto = author.ToDto();

            return authorDto;
        }

        [HttpPut("{id")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var author = new Author(dto);
                await _authorRepository.Update(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    // This would get logged in Prod and not a writeline
                    throw;
                }
            }
            catch (Exception ex)
            {
               // This would get logged in Prod and not a writeline
                WriteLine(ex);
                throw;
            }

            return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorDto dto)
        {
            var author = new Author(dto);
            var authorMessage = new AuthorMessage()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                WritingAwards = dto.WritingAwards
            };

            // Publish to RabbitMQ with MassTransit
            await _publishEndpoint.Publish(authorMessage);

            return Ok();
        }


        private async Task<bool> AuthorExistsAsync(int id)
        {
            return await _authorRepository.Exists(id);
        }
    }
}
