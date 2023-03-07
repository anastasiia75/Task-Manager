using AutoMapper;
using TaskManagerAPI.Dtos;
using TaskManagerDomain.Models;

namespace TaskManagerAPI.Mapper
{
    public class AssignmentMappingProfiles: Profile
    {
        public AssignmentMappingProfiles()
        {
            CreateMap<AssignmentDto, Assignment>();
            CreateMap<Assignment, AssignmentDto>();
            CreateMap<Assignment, AssignmentPutDto>();

            CreateMap<AssignmentPutDto, Assignment>();


        }
    }
}
