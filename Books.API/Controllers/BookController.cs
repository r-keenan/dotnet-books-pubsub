using Books.API.Constants;
using Books.API.Repositories;
using Books.API.Services;
using Books.Shared.Constants;
using Books.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IKafkaProducerService _kafkaProducer;
        private readonly ApiEndpoints _apiEndpoints;

        public BookController(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IPublisherRepository publisherRepository,
            IPublishEndpoint publishEndpoint,
            IKafkaProducerService kafkaProducer,
            ApiEndpoints apiEndpoints
        )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _publishEndpoint = publishEndpoint;
            _kafkaProducer = kafkaProducer;
            _apiEndpoints = apiEndpoints;
        }

        [HttpGet]
        // TODO: Add Pagination to endpoint
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _bookRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = book.ToDto();

            return bookDto;
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<BookDetailsDto>> GetBookWithDetails(int id)
        {
            var book = await _bookRepository.GetWithDetails(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = book.ToDetailsDto();

            return bookDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var book = new Book(dto);
                await _bookRepository.Update(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExistsAsync(id))
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
        public async Task<IActionResult> CreateBook(BookDto bookDto)
        {
            var book = new Book(bookDto);

            var newBook = await _bookRepository.Add(book);

            var bookMessage = new BookMessage()
            {
                Id = newBook.Id,
                Title = newBook.Title,
                PageLength = newBook.PageLength,
                Genre = newBook.Genre,
                DatePublished = newBook.DatePublished,
                AuthorId = newBook.AuthorId,
                PublisherId = newBook.PublisherId,
            };

            // Publish to RabbitMQ with MassTransit
            await _publishEndpoint.Publish(bookMessage);

            await _kafkaProducer.ProduceAsync(KafkaTopics.BooksTopic, newBook);

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.Get(id);
            if (book == null)
                return NotFound();

            await _bookRepository.Delete(id);

            return NoContent();
        }

        private async Task<bool> BookExistsAsync(int id)
        {
            return await _bookRepository.Exists(id);
        }
    }
}
