using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Enum.Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del préstamo debe ser mayor que cero.");

            return await _loanRepository.GetByIdAsync(id);
        }

        public async Task<Loan> AddAsync(Loan loan)
        {
            // Validar libro existente
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book == null)
                throw new ArgumentException("El libro especificado no existe.");

            // Validar miembro existente
            var member = await _memberRepository.GetByIdAsync(loan.MemberId);
            if (member == null)
                throw new ArgumentException("El miembro especificado no existe.");

            // Validar disponibilidad del libro (no prestado actualmente)
            var existingLoans = await _loanRepository.GetAllAsync();
            foreach (var l in existingLoans)
            {
                if (l.BookId == loan.BookId && l.Status == LoanStatus.Active)
                    throw new ArgumentException("El libro ya está prestado y no está disponible.");
            }

            // Validar fechas
            if (loan.LoanDate > DateTime.Now)
                throw new ArgumentException("La fecha de préstamo no puede ser futura.");

            if (loan.ReturnDate.HasValue && loan.ReturnDate.Value < loan.LoanDate)
                throw new ArgumentException("La fecha de devolución debe ser posterior a la fecha de préstamo.");

            return await _loanRepository.AddAsync(loan);
        }

        public async Task<bool> UpdateAsync(Loan loan)
        {
            if (loan.Id <= 0)
                throw new ArgumentException("El ID del préstamo es inválido.");

            if (loan.ReturnDate.HasValue && loan.ReturnDate.Value < loan.LoanDate)
                throw new ArgumentException("La fecha de devolución debe ser posterior a la fecha de préstamo.");

            return await _loanRepository.UpdateAsync(loan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del préstamo debe ser mayor que cero.");

            return await _loanRepository.DeleteAsync(id);
        }
    }
}
