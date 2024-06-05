
namespace WebChat.Application.Cqrs.Groups.Qureies.GetGroupById
{
    public record GroupResponse(
       )
    {
        public int GroupId { get; private set; }
        public string GroupName { get; private set; }
        public string CreatedGroupBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string groupimgurl { get; private set; }
    }
    /*
      string GroupName,
        string groupimgurl, 
        string CreatedGroupBy,
        DateTime? CreatedOn, 
        int GroupId
     */

}
