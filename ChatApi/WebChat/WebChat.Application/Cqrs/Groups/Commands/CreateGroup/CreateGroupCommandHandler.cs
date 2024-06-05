using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Domain.Repositories;
using WebChat.Domain.Shared;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Entities;

namespace WebChat.Application.Cqrs.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandHandler:ICommandHandler<CreateGroupCommand,BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;
        public CreateGroupCommandHandler( IUnitOfWork unitOfWork,IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }
        public async Task<BaseResponse> Handle(CreateGroupCommand command,CancellationToken cancellationToken)
        {
            var checkGroup = await _unitOfWork.GroupRepositoy.CheckGroupExist(command.GroupName, command.CreatedGroupBy);
            if (checkGroup)
                return await BaseResponse.BadRequestResponsAsync("this Group Already Exist");
            var imgUrl = await _mediaService.UploadImageAsync(command.groupimg, "GroupsImages");
            

            var members = new GroupMembersCommand();
            if (command.Users != null)
            {
                command.Users.Add(command.CreatedGroupBy);
                members.Users = command.Users;
            }
            var group=Group.CreateGroup(command.GroupName, command.CreatedGroupBy,imgUrl,command.Users);
             _unitOfWork.GroupRepositoy.CreateGroup(group);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var groupDto = new UserChatsResponse();
            groupDto.imgUrl = group.groupimgurl;
            groupDto.Message = null;
            groupDto.Id = group.GroupId;
            groupDto.Name = group.GroupName;
            groupDto.Type = "Group";
            members.Group = groupDto;
           return await BaseResponse.SuccessResponseWithDataAsync(members);

        }
    }
}
