﻿using Microsoft.EntityFrameworkCore;
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
            //-- await -- allows for waiting on long-running tasks (such as database access
            //or waiting for a network response) in an efficient way without freezing
            //the user interface or blocking the application.
            return await _context.Hostels
                .Include(h => h.Students)
                .ToListAsync();
        }

        // GetHostelById: Fetch details of a specific hostel with associated students
        public async Task<Hostel> GetHostelById(int hostelId)
        {
            return await _context.Hostels
                .Include(h => h.Students)
                .FirstOrDefaultAsync(h => h.Hostel_id == hostelId);
        }
        
        // AddHostel: Add a new hostel
        public async Task AddHostel(Hostel hostel)
        {
            await _context.Hostels.AddAsync(hostel);
            await _context.SaveChangesAsync();
        }

        // UpdateHostel: Modify an existing hostel's details
        public async Task UpdateHostel(Hostel hostel)
        {
            _context.Hostels.Update(hostel);
            await _context.SaveChangesAsync();
        }

        // DeleteHostel: Remove a hostel by ID and ensure no orphaned student data
        public async Task DeleteHostel(int hostelId)
        {
            var hostel = await _context.Hostels
                .Include(h => h.Students)
                .FirstOrDefaultAsync(h => h.Hostel_id == hostelId);

            if (hostel != null)
            {
                // Remove associated students' hostel reference if needed
                foreach (var student in hostel.Students)
                {
                    student.Hostel_id = null;
                }

                _context.Hostels.Remove(hostel);
                await _context.SaveChangesAsync();
            }
        }
        // GetHostelsByCity: List hostels in a specific city using LINQ
        public async Task<List<Hostel>> GetHostelsByCity(string city)
        {
            return await _context.Hostels
                .Where(h => h.Students.Any(s => s.City == city))
                .Include(h => h.Students)
                .ToListAsync();
        }

        // CountHostelsWithAvailableSeats: Provide a count of hostels that have available seats
        public async Task<int> CountHostelsWithAvailableSeats()
        {
            return await _context.Hostels
                .CountAsync(h => h.No_of_seats > h.Students.Count);
        }
        public async Task<List<Student>> GetStudentsInHostel(int hostelId)
        {
            // Use the Hostel navigation property to load students for the specified hostel.
            return await _context.Students
                                 .Where(student => student.Hostel_id == hostelId)
                                 .ToListAsync();
        }

    }
}
