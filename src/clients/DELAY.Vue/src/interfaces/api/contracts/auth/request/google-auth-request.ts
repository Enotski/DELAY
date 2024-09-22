import { type IAuthUserAgentRequest } from "./auth-user-agent-request";

export interface IGoogleAuthRequest extends IAuthUserAgentRequest{
    code: string;
}