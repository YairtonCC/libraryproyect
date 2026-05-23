using Library.Domain.Enities;
using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member?> GetByEmailAsync(string email); 
        Task<Member> AddAsync(Member member);
        Task<bool> UpdateAsync(Member member);
        Task<bool> DeleteAsync(int id);
    }
}

