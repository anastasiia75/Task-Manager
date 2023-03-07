using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDomain.Abstractions.Repositories;
using TaskManagerDomain.Models;

namespace TaskManagerDAL.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly DataContext _dataContext;

        public AssignmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Assignment> CreateAssignment(Assignment Assignment)
        {
            _dataContext.Assignments.Add(Assignment);
            await _dataContext.SaveChangesAsync();
            return Assignment;
        }

        public async Task DeleteAssignmentById(Guid id)
        {
            var assignment = await _dataContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Remove(assignment);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Assignment>> GetAllTasksByUserId(Guid userId)
        {
            
            var assignments = await _dataContext.Assignments.Where(a => a.UserId == userId).ToListAsync();
            return assignments;
        }
        public async Task<Assignment> GetAssignmentById(Guid id)
        {
            var assignment = await _dataContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            return assignment;

        }


        public async Task<Assignment> UpdateAssignment(Assignment newAssignment)
        {
            _dataContext.Assignments.Update(newAssignment);
            await _dataContext.SaveChangesAsync();
            return newAssignment;
        }
    }
}
