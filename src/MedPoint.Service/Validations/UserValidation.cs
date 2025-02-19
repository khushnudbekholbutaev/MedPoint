using FluentValidation;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Validations
{
    public class UserValidation : AbstractValidator<UserForCreationDto>
    {
        public UserValidation() 
        {
            //RuleFor(x => x.FirstName).MaximumLength(50);
            //RuleFor(x => x.LastName).MaximumLength(50);
            //RuleFor(x => x.PhoneNumber).Matches(@"^\+998\d{9}$");
            //RuleFor(x => x.Email).EmailAddress();
            //RuleFor(x => x.Age).InclusiveBetween(18, 65);
            //RuleFor(x => x.Address).MaximumLength(50);
        }
    }
}
