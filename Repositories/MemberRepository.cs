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

        public async Task<IEnumerable<Member>> GetRecentMembers(int months)
        {
            DateTime fromDate = DateTime.UtcNow.AddMonths(-months);
            return await _context.Members.Where(m => m.dateJoined >= fromDate).ToListAsync();
        }

        // Filter members based on any member property
        public async Task<IEnumerable<Member>> SearchMembers(string? name = null, string? gender = null, DateTime? dateJoined = null)
        {

            //dynamic filtering.
            var query = _context.Members.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(m => m.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(gender))
                query = query.Where(m => m.Gender == gender); 

            if (dateJoined.HasValue)
                query = query.Where(m => m.dateJoined.Date == dateJoined.Value.Date); 

            return await query.ToListAsync();
        }

    }
}
