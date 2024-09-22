import { type IAuthUserAgentRequest } from "./auth-user-agent-request";

export interface IVkAuthRequest extends IAuthUserAgentRequest{
    code: string;
    deviceId: string;
    codeVerifier: string;
}