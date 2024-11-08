export interface IMessageDto {
    text: string;
    author: string;
    time: string;
    isCurrentUserMessage: boolean;
    chatId: string;
}