using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebChat.Domain.Entities
{
    public class Group
    {
        private readonly List<GroupMessages> _groupMessages = new();
        internal Group(string groupName, string createdGroupBy, string groupimgurl)
        {
            GroupName = groupName;
            CreatedGroupBy = createdGroupBy;
            this.groupimgurl = groupimgurl;
        }
        internal Group(int groupId,string groupName,string groupimgurl, GroupMessages? groupMessage = null)
        {
            GroupId = groupId;
            GroupName = groupName;
            this.groupimgurl = groupimgurl;
            _groupMessages.Add(groupMessage);
        }


        [Key]
        public int GroupId { get; private set; }
        public string GroupName { get; private set; }
        public string CreatedGroupBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string groupimgurl { get; private set; }
        public virtual ICollection<UserGroup>UserGroups { get; private set; } 
        public  IReadOnlyCollection<GroupMessages>? Messages => _groupMessages;

        public static Group CreateGroup(string groupName, string createdGroupBy, string groupimgurl, List<string>? users)
        {
            var userGroup = users.Select(userId => new UserGroup(userId)).ToList();
            var group =new Group(groupName, createdGroupBy, groupimgurl);
            group.UserGroups = userGroup;
            group.CreatedOn = DateTime.Now;
            return group;

        }
        public static Group getGroupwithLastMsg(int groupId,string groupName, string groupimgurl,GroupMessages message)
        {

            return new Group(groupId,groupName, groupimgurl, message);
        }
    }
}
