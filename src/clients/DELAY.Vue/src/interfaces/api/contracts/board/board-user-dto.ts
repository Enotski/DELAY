import type { INameDto } from "../base";
import type { BoardRoleType } from "../enums";

export interface IBoardUserDto{
    user: INameDto;
    userRole: string;
}