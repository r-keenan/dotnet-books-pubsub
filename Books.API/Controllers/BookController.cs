using Books.API.Constants;
using Books.API.Repositories;
using Books.API.Services;
using Books.Common.Constants;
using Books.Common.Messages;
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
        private readonly ILogger<BookController> _logger;

        public BookController(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IPublisherRepository publisherRepository,
            IPublishEndpoint publishEndpoint,
            IKafkaProducerService kafkaProducer,
            ApiEndpoints apiEndpoints,
            ILogger<BookController> logger
        )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _publishEndpoint = publishEndpoint;
            _kafkaProducer = kafkaProducer;
            _apiEndpoints = apiEndpoints;
            _logger = logger;
        }

        [HttpGet]
        // TODO: Add Pagination to endpoint
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _bookRepository.GetAll();
        }

        [HttpGet("/details")]
        // TODO: Add Pagination to endpoint
        public async Task<ActionResult<IEnumerable<BookDetailsDto>>> GetBooksWithDetails()
        {
            var books = await _bookRepository.GetAllWithDetails();

            var bookDto = books.ToDetailsDto();

            return bookDto;
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
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await BookExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(
                        ex,
                        "Concurrency conflict occurred while updating book {BookId}. Error: {ErrorMessage}",
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
                    "Unexpected error occurred while updating book {BookId}. Error: {ErrorMessage}",
                    id,
                    ex.Message
                );
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
