import { type INameDto } from "../../base";
import { type ISignInRequest } from "./sign-in-request";

export interface ISignUpRequest extends ISignInRequest, INameDto{
}