using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using Library.Domain.Services;

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
            return await _loanRepository.GetByIdAsync(id);
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            // Validación 1: libro existe
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book == null)
                throw new Exception("El libro no existe.");

            // Validación 2: miembro existe
            var member = await _memberRepository.GetByIdAsync(loan.MemberId);
            if (member == null)
                throw new Exception("El miembro no existe.");

            // Validación 3: libro no prestado actualmente
            var existingLoans = await _loanRepository.GetAllAsync();
            if (existingLoans.Any(l => l.BookId == loan.BookId && l.Status == 0))
                throw new Exception("El libro ya está prestado.");

            // Validación 4: máximo 5 préstamos activos por miembro
            var activeLoans = existingLoans.Count(l => l.MemberId == loan.MemberId && l.Status == 0);
            if (activeLoans >= 5)
                throw new Exception("El miembro ya tiene el máximo de préstamos activos.");

            // Validación 5: fechas correctas
            if (loan.ReturnDate <= loan.LoanDate)
                throw new Exception("La fecha de devolución debe ser posterior a la fecha de préstamo.");

            await _loanRepository.AddAsync(loan);
            return loan;
        }

        public async Task<bool> UpdateAsync(int id, Loan loan)
        {
            var existing = await _loanRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.BookId = loan.BookId;
            existing.MemberId = loan.MemberId;
            existing.LoanDate = loan.LoanDate;
            existing.ReturnDate = loan.ReturnDate;
            existing.Status = loan.Status;

            await _loanRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _loanRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _loanRepository.DeleteAsync(id);
            return true;
        }
    }
}
