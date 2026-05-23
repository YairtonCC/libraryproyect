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
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAll()
        {
            var loans = await _loanService.GetAllAsync();
            // Mapear a DTO (puedes usar AutoMapper si ya lo tienes configurado)
            var loanDtos = new List<LoanDto>();
            foreach (var loan in loans)
            {
                loanDtos.Add(new LoanDto
                {
                    Id = loan.Id,
                    BookId = loan.BookId,
                    MemberId = loan.MemberId,
                    LoanDate = loan.LoanDate,
                    ReturnDate = loan.ReturnDate ?? default,
                    Status = loan.Status
                });
            }
            return Ok(loanDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            var dto = new LoanDto
            {
                Id = loan.Id,
                BookId = loan.BookId,
                MemberId = loan.MemberId,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate ?? default,
                Status = loan.Status
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<LoanDto>> Create([FromBody] CreateLoanDto dto)
        {
            var loan = new Loan
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                LoanDate = dto.LoanDate,
                ReturnDate = dto.ReturnDate,
                Status = 0 // activo por defecto
            };

            var created = await _loanService.AddAsync(loan);

            var resultDto = new LoanDto
            {
                Id = created.Id,
                BookId = created.BookId,
                MemberId = created.MemberId,
                LoanDate = created.LoanDate,
                ReturnDate = created.ReturnDate ?? default,
                Status = created.Status
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoanDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var loan = new Loan
            {
                Id = dto.Id,
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                LoanDate = dto.LoanDate,
                ReturnDate = dto.ReturnDate,
                Status = dto.Status
            };

            var updated = await _loanService.UpdateAsync(loan);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _loanService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
