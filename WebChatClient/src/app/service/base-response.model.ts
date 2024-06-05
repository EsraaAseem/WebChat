export interface BaseResponse<T> {
    status: number;
    isSuccess: boolean;
    message: string;
    data:T;
  
  /*  constructor(status: number, isSuccess: boolean, message: string, result: T) {
      this.status = status;
      this.isSuccess = isSuccess;
      this.message = message;
      this.result = result;
    }*/
  }