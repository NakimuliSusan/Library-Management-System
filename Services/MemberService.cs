using Library_Management_System.Models;
using LibraryManagementSystem.Repositories;


namespace Library_Management_System.Services
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task AddMemberAsync(Member member) => await _memberRepository.Add(member);
        public async Task<IEnumerable<Member>> GetRecentMembersAsync(int months) => await _memberRepository.GetRecentMembersAsync(months);
    }
}
