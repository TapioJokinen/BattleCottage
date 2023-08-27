/* Credentials which user uses to sign-in. */
export interface LoginCredentialType {
  email: string;
  password: string;
}

export interface APIBaseResponseType {
  responseOk: boolean;
}

export interface LoginResponseType extends APIBaseResponseType {
  email: string;
}

export interface VerifyResponseType extends LoginResponseType {}
