using AutoMapper;
using StudentRegistration.DTO;
using StudentRegistration.Profiles.AfterMaps;

namespace StudentRegistration.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Models.Student, Student>()
                .ReverseMap();
            CreateMap<Models.Gender, Gender>() 
                .ReverseMap();
            CreateMap<Models.Address, Address>() 
                .ReverseMap();
            CreateMap<UpdateStudentRequest, Models.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<AddStudentRequest, Models.Student>()
                .AfterMap<AddStudentRequestAfterMap>();

        }
    }
}
