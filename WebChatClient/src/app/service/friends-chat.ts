export interface FriendsChat{
    messageId: number,
    senderId: string,
    content:string,
    isRead: boolean,
    isDeleteBySender:number
    isDeleteByReciver:number
    messageTime: string
}