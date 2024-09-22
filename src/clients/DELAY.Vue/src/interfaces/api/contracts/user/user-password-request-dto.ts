import { type IBaseDto } from "../base/base-dto";

export interface IUserPasswordRequestDto extends IBaseDto {
    password: string;
}