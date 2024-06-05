import { GroupDto } from "./group-dto.model";
import { UserChatsDto } from "./user-chats-dto.model";

export interface GroupMemberDto
{
 group:UserChatsDto;
 users?:string[];
      
}