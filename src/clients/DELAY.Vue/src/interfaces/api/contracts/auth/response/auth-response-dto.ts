import { type IApiEndpoint } from "./api-endpoint";
import { type ITokensResponseDto } from "./tokens-response-dto";

export interface IAuthResponseDto {
    endpoints: IApiEndpoint[];
    tokens: ITokensResponseDto;
}