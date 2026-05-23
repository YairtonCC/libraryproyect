using Library.DataAccess.Context;
using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<Member?> GetByEmailAsync(string email) 
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Member> AddAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return false;

            _context.Members.Remove(member);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
