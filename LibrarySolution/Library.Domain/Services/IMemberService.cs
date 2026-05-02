using Library.Domain.Enities;
using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> CreateAsync(Member member);
        Task<bool> UpdateAsync(int id, Member member);
        Task<bool> DeleteAsync(int id);
    }
}
