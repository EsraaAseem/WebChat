using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Commands.ChangePassword
{
    public record ResetPasswordCommand(string phoneNumber,string oldPassword,string newPassword):ICommand<BaseResponse>;
}
