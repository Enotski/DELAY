import type { INameDto } from "../base";
import type { RoomRoleType } from "../enums";

export interface IRoomUserDto{
    user: INameDto;
    role: string;
}