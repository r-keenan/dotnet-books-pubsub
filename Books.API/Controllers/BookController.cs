using AutoMapper;
using Books.API.Constants;
using Books.API.Repositories;
using Books.API.Services;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IPubSubMessagePublisher _pubsubMessagePublisher;
        private readonly ApiEndpoints _apiEndpoints;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, IMapper mapper, IPublishEndpoint publishEndpoint, IPubSubMessagePublisher pubSubMessagePublisher, ApiEndpoints apiEndpoints)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _pubsubMessagePublisher = pubSubMessagePublisher;
            _apiEndpoints = apiEndpoints;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _bookRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _bookRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            var publisher = await _publisherRepository.Get(book.PublisherId);

            var author = await _authorRepository.Get(book.AuthorId);

            var bookDto = _mapper.Map<BookDetailsDto>(book);
            bookDto.Author = _mapper.Map<AuthorDto>(author);
            bookDto.Publisher = _mapper.Map<PublisherDto>(publisher);

            return bookDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
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

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var bookMessage = new BookMessage()
            {
                Id = book.Id,
                Title = bookDto.Title,
                PageLength = bookDto.PageLength,
                Genre = bookDto.Genre.ToString(),
                DatePublished = bookDto.DatePublished,
                AuthorId = bookDto.AuthorId,
                PublisherId = bookDto.PublisherId
            };

            // Publish to RabbitMQ with MassTransit
            await _publishEndpoint.Publish(bookMessage);

            // Publish to GCP PubSub with MassTransit
            await _pubsubMessagePublisher.PublishMessage(bookMessage, "books");

            return Ok();
        }


        private async Task<bool> BookExistsAsync(int id)
        {
            return await _bookRepository.Exists(id);
        }

    }
}
