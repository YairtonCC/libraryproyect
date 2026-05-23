using Library.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace LibraryProyect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCategoriesController : ControllerBase
    {
        private readonly IBookCategoryService _bookCategoryService;

        public BookCategoriesController(IBookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCategory>>> GetAll()
        {
            var bookCategories = await _bookCategoryService.GetAllAsync();
            return Ok(bookCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookCategory>> GetById(int id)
        {
            var bookCategory = await _bookCategoryService.GetByIdAsync(id);
            if (bookCategory == null)
                return NotFound();

            return Ok(bookCategory);
        }

        [HttpPost]
        public async Task<ActionResult<BookCategory>> Create(BookCategory bookCategory)
        {
            var created = await _bookCategoryService.AddAsync(bookCategory);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookCategory bookCategory)
        {
            if (id != bookCategory.Id)
                return BadRequest();

            var updated = await _bookCategoryService.UpdateAsync(bookCategory);
            if (!updated)
                return NotFound();

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookCategoryService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
