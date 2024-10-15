import { type INameDto } from '../base/name-dto';
import type { IBoardUserDto } from './board-user-dto';

export interface IBoardDto extends INameDto{
    isPublic: boolean;
    description: string;
    users: IBoardUserDto[];
}