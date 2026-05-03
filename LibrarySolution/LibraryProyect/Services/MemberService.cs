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
            return await _memberRepository.AddAsync(member);
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            return await _memberRepository.UpdateAsync(member);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _memberRepository.DeleteAsync(id);
        }
    }
}
