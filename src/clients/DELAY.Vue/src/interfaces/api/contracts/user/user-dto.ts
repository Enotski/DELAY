import { type INameDto } from "../base/name-dto";

export interface IUserDto extends INameDto{
    email: string;
    phoneNumber: string;
    password: string;
}