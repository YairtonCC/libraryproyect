using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;

namespace LibraryProyect.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Member> CreateAsync(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.Name))
                throw new Exception("El nombre del miembro es obligatorio.");

            if (string.IsNullOrWhiteSpace(member.Email))
                throw new Exception("El email es obligatorio.");

            var existing = await _repository.GetAllAsync();
            if (existing.Any(m => m.Email == member.Email))
                throw new Exception("Ya existe un miembro con ese email.");

            if (member.JoinDate > DateTime.Now)
                throw new Exception("La fecha de ingreso no puede ser futura.");

            await _repository.AddAsync(member);
            return member;
        }

        public async Task<bool> UpdateAsync(int id, Member member)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = member.Name;
            existing.Email = member.Email;
            existing.JoinDate = member.JoinDate;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
