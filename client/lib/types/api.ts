import { Tokens } from '@/lib/types/auth';

export interface APIBaseResponseType {
  responseOk: boolean;
}

export interface APIMessageResponseType {
  message: string;
}

export interface LoginResponseType extends APIBaseResponseType {
  accessToken: string;
  refreshToken: string;
  expiration: string;
  message?: string;
}

export interface RegisterResponseType extends APIBaseResponseType, APIMessageResponseType {}

export interface RevokeResponseType extends APIBaseResponseType, APIMessageResponseType {}

export interface VerifyResponseType extends LoginResponseType {}

export interface RefreshResponse extends APIBaseResponseType, APIMessageResponseType, Tokens {
  accessTokenExpiration?: string;
  refreshTokenExpiration?: string;
}

export interface GamesResponseType extends APIBaseResponseType {
  next: string;
  previous: string;
  results: Array<Game>;
}
