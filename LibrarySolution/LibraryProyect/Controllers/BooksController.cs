using Library.Domain.DTOs;
using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    PublishedYear = book.PublishedYear,
                    AuthorId = book.AuthorId
                });
            }

            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            var dto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            var created = await _bookService.AddAsync(book);

            var resultDto = new BookDto
            {
                Id = created.Id,
                Title = created.Title,
                ISBN = created.ISBN,
                PublishedYear = created.PublishedYear,
                AuthorId = created.AuthorId
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                ISBN = dto.ISBN,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            var updated = await _bookService.UpdateAsync(book);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
