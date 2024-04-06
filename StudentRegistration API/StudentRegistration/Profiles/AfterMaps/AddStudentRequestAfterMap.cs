using AutoMapper;
using StudentRegistration.DTO;

namespace StudentRegistration.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, Models.Student>
    {
        public void Process(AddStudentRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new Models.Address()
            {
               Id = Guid.NewGuid(),
               PhysicalAddress = source.PhysicalAddress,
            };
        }
    }
}
