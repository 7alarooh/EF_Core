using Microsoft.EntityFrameworkCore;
using OutSysCollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Repositories
{
    public class HostelRepository
    {
        private readonly CollegeDbContext _context;

        public HostelRepository(CollegeDbContext context)
        {
            _context = context;
        }

        // GetAllHostels: Retrieve all hostel information, including associated students
        public async Task<List<Hostel>> GetAllHostels()
        {
            return await _context.Hostels
                .Include(h => h.Students)
                .ToListAsync();
        }

       
    }
}
