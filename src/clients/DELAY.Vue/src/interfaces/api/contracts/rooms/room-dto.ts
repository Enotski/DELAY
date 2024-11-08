import { type INameDto } from '../base/name-dto';
import type { IRoomUserDto } from './room-user-dto';

export interface IRoomDto extends INameDto{
    chatType: string;
    boards: INameDto[];
    users: IRoomUserDto[];
}