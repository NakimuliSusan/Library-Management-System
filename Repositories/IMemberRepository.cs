using Library_Management_System.Models;
using Library_Management_System.Repositories;

namespace LibraryManagementSystem.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<IEnumerable<Member>> GetRecentMembers(int months);

        Task<IEnumerable<Member>> SearchMembers(string? name = null, string? gender = null, DateTime? dateJoined = null);



    }
}
