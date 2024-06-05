import { GroupFriendMessageDto } from "./group-friend-message-dto.model";

export interface GroupWithMessages
{
    groupId:number
    groupName:string;
    createdGroupBy:string;
    messages:GroupFriendMessageDto[];
}