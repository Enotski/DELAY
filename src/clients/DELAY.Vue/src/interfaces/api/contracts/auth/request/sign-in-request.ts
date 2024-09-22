import { type IAuthUserAgentRequest } from "./auth-user-agent-request";

export interface ISignInRequest extends IAuthUserAgentRequest{
      phone: string;
      email: string;
      password: string;
}