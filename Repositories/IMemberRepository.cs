using Library_Management_System.Models;
using Library_Management_System.Repositories;

namespace LibraryManagementSystem.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<IEnumerable<Member>> GetRecentMembersAsync(int months);
    }
}
