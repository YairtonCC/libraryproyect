using Library.Domain.Enities;
using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> AddAsync(Member member);
        Task<bool> UpdateAsync(Member member);
        Task<bool> DeleteAsync(int id);
    }
}
