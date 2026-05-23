using Library.Domain.Enities;
using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetActiveLoansByMemberIdAsync(int memberId);
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
    }
}
