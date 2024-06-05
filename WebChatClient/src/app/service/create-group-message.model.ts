export interface CreateGroupMessageDto
{
    senderId:string,
    groupId:number,
    groupName?:string,
    content:string
}