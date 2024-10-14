import { type INameDto } from '../base/name-dto';

export interface ITicketsListDto<T = string> extends INameDto{
      boardId: T;
      tickets: INameDto[];
}