//using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Cqrs.Authentication.Commands.Register;

namespace WebChat.Application.Cqrs.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandValidator //: AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {

           // RuleFor(x => x.GroupName).NotEmpty().WithMessage("Group name required");
           // RuleFor(x => x.groupimg).NotEmpty().WithMessage("Group Image  required");

        }
    }
 
}
