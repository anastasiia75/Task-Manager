using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDomain.Models;

namespace TaskManagerDomain.Abstractions.Repositories
{
    public interface IAssignmentRepository
    {
        Task<Assignment> CreateAssignment(Assignment Assignment);
        Task<Assignment> GetAssignmentById(Guid id);

        Task DeleteAssignmentById(Guid id);
        Task<List<Assignment>> GetAllTasksByUserId(Guid userId);
        Task<Assignment> UpdateAssignment(Assignment newAssignment);
    }
}
