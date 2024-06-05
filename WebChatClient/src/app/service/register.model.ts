export class Register{
  password: string;
  name:string;
  phoneNumber:string;
  userName:string;
  userImg:File
  constructor(pass:string,na:string,phone:string,user:string,userImg:File)
  {
    this.password=pass;
    this.name=na;
    this.phoneNumber=phone;
    this.userName=user;
    this.userImg=userImg;
  }
}