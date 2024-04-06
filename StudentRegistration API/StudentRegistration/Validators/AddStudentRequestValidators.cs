﻿using FluentValidation;
using StudentRegistration.DTO;
using StudentRegistration.Repositories;

namespace StudentRegistration.Validators
{
    public class AddStudentRequestValidators : AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidators(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(1000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGenders().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
        }
    }
}
