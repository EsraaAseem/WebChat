export interface CreateGroupDto{
    groupName:string;
    createdGroupBy:string;
    groupimg:File;
    users?:string[];
  }