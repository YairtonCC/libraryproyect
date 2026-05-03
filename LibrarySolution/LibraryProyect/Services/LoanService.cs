using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _loanRepository.GetByIdAsync(id);
        }

        public async Task<Loan> AddAsync(Loan loan)
        {
            return await _loanRepository.AddAsync(loan);
        }

        public async Task<bool> UpdateAsync(Loan loan)
        {
            return await _loanRepository.UpdateAsync(loan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _loanRepository.DeleteAsync(id);
        }
    }
}
