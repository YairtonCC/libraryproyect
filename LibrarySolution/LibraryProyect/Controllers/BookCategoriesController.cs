using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoriesController : ControllerBase
    {
        private readonly IBookCategoryService _service;

        public BookCategoriesController(IBookCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCategory>>> GetBookCategories()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{bookId}/{categoryId}")]
        public async Task<ActionResult<BookCategory>> GetBookCategory(int bookId, int categoryId)
        {
            var bookCategory = await _service.GetByIdsAsync(bookId, categoryId);
            if (bookCategory == null) return NotFound();
            return Ok(bookCategory);
        }

        [HttpPost]
        public async Task<ActionResult<BookCategory>> PostBookCategory(BookCategory bookCategory)
        {
            var created = await _service.CreateAsync(bookCategory);
            return CreatedAtAction(nameof(GetBookCategory), new { bookId = created.BookId, categoryId = created.CategoryId }, created);
        }

        [HttpDelete("{bookId}/{categoryId}")]
        public async Task<IActionResult> DeleteBookCategory(int bookId, int categoryId)
        {
            var deleted = await _service.DeleteAsync(bookId, categoryId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
