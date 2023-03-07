using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Dtos;
using TaskManagerDomain.Abstractions.Repositories;
using TaskManagerDomain.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AssignmentController(IAssignmentRepository assignmentRepository,IMapper mapper, IUserRepository userRepository)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost("user-profile/{username}/create-task"), Authorize]
        public async Task<ActionResult<AssignmentPutDto>> CreateAssignment([FromRoute]string username, AssignmentPutDto assignmentDto)
        {
            var user = await _userRepository.GetUserByName(username);
            var assignment = _mapper.Map<Assignment>(assignmentDto);
            assignment.User = user;
            assignment.UserId = user.Id;

            var newAssignment = await _assignmentRepository.CreateAssignment(assignment);
            var newAssignmentDto = _mapper.Map<AssignmentPutDto>(newAssignment);
            return Ok(newAssignmentDto);
        }

        [HttpGet, Authorize]
        [Route("user-profile/{username}")]
        public async Task<ActionResult> GetAllUserAssignments([FromRoute] string username)
        {
            var user = await _userRepository.GetUserByName(username);
            var assignments = await _assignmentRepository.GetAllTasksByUserId(user.Id);
            var assignmentsDtos = _mapper.Map<List<AssignmentPutDto>>(assignments);

            return Ok(assignmentsDtos);
        }

        [HttpGet, Authorize]
        [Route("user-profile/{username}/{id}")]
        public async Task<ActionResult> GetUserAssignment([FromRoute]Guid id)
        {

            var assignments = await _assignmentRepository.GetAssignmentById(id);
            var assignmentsDtos = _mapper.Map<AssignmentPutDto>(assignments);

            return Ok(assignmentsDtos);
        }

        [HttpPut("user-profile/{username}/update/{id}"), Authorize]
        public async Task<ActionResult> UpdateAssignment([FromRoute] string username, AssignmentPutDto updatedAssignment)
        {
            var user = await _userRepository.GetUserByName(username);
            var assignmentToUpdate = _mapper.Map<Assignment>(updatedAssignment);
            assignmentToUpdate.User = user;
            assignmentToUpdate.UserId = user.Id;

            var assignment = await _assignmentRepository.UpdateAssignment(assignmentToUpdate);
            var assignmentDto = _mapper.Map<AssignmentPutDto>(assignment);

            return Ok(assignmentDto);
        }

        [HttpDelete, Authorize]
        [Route("user-profile/{username}/{id}")]
        public async Task<ActionResult> DeleteAssignmentById([FromRoute]Guid id)
        {
            await _assignmentRepository.DeleteAssignmentById(id);
            return Ok();
        }


    }
}
