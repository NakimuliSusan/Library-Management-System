using Library_Management_System.Data;
using Library_Management_System.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetRecentMembersAsync(int months)
        {
            DateTime fromDate = DateTime.Now.AddMonths(-months);
            return await _context.Members.Where(m => m.dateJoined >= fromDate).ToListAsync();
        }
    }
}
