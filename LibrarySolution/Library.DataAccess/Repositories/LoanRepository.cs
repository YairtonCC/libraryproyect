using Library.DataAccess.Context;
using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DataAccess.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansByMemberIdAsync(int memberId)
        {
            return await _context.Loans
                .Where(l => l.MemberId == memberId && l.Status == LoanStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            return await _context.Loans
                .Where(l => l.Status == LoanStatus.Overdue)
                .ToListAsync();
        }
    }
}
