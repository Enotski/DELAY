import { type INameDto } from '../base/name-dto';

export interface ITicketDto extends INameDto{
    id: string,
    name: string,
    isDone: boolean,
    description: string,
    changedDate: string,
    createDate: string,
    changedBy: string,
    createdBy: string,
    boardId: string,
    ticketListId: string,
    users: INameDto[],
}