using AutoMapper;
using de_training.DTOs;
using de_training.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace de_training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("Books")]
        [Authorize]
        public async Task<IEnumerable<GetBookDto>> GetBooksAsync()
        {
            var books = await _bookService.GetAllAsync();
            var bookDtos = _mapper.Map<List<GetBookDto>>(books);
            return bookDtos;
        }

        [HttpGet("Book/{id}")]
        [Authorize]
        public async Task<Book> GetBookByIdAsync(long id)
        {
            return await _bookService.GetBookAsync(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<GetBookDto> CreateBookAsync(PostBookDto postBookDto)
        {
            var requestBook = _mapper.Map<Book>(postBookDto);
            var insertedBook = await _bookService.CreateBookAsync(requestBook);
            var bookDto = _mapper.Map<GetBookDto>(insertedBook);
            return bookDto;
        }
    }
}
