

using WebChat.Domain.Entities;

namespace WebChat.Domain.Repository
{
    public interface IGroupRepositoy
    {
        void CreateGroup(Group group);
      //  Task<BaseResponse> CreateGroup(Group request);
        Task<bool> CheckGroupExist(string groupName, string groupCreatedBy);

    }
}
