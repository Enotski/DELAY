import { type INameDto } from '../base/name-dto';

export interface ITicketDto extends INameDto{
    description: string,
    changedBy: string,
    createdBy: string,
    dateChange: string,
    createDate: string,
    deadLineDate: string,
    ticketListId: string,
    boardId: string,
    isDone: boolean,
    users: INameDto[]
}