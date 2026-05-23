using Library.Domain.Enities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;


namespace LibraryProyect.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public async Task<Member> AddAsync(Member member)
        {
            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(member.Name))
                throw new ArgumentException("El nombre es obligatorio.");

            // Validación: email obligatorio
            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            // Validación: email único
            var existing = await _memberRepository.GetByEmailAsync(member.Email);
            if (existing != null)
                throw new ArgumentException("El correo electrónico ya está registrado.");

            // Validación: fecha de ingreso no futura
            if (member.JoinDate > DateTime.Now)
                throw new ArgumentException("La fecha de ingreso no puede ser futura.");

            return await _memberRepository.AddAsync(member);
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(member.Name))
                throw new ArgumentException("El nombre es obligatorio.");

            // Validación: email obligatorio
            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            // Validación: email único (excepto el mismo miembro)
            var existing = await _memberRepository.GetByEmailAsync(member.Email);
            if (existing != null && existing.Id != member.Id)
                throw new ArgumentException("El correo electrónico ya está registrado por otro miembro.");

            // Validación: fecha de ingreso no futura
            if (member.JoinDate > DateTime.Now)
                throw new ArgumentException("La fecha de ingreso no puede ser futura.");

            return await _memberRepository.UpdateAsync(member);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _memberRepository.DeleteAsync(id);
        }
    }
}
