import { GroupFriendMessageDto } from "./group-friend-message-dto.model"
export interface UserChatsDto{
    name:string,
    id:number,
    imgUrl :string,
    type:string,
    message:GroupFriendMessageDto
}