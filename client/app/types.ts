/* Credentials which user uses to sign-in. */
export interface LoginCredentialType {
  email?: string;
  password?: string;
}

export interface RegisterCredentialType extends LoginCredentialType {
  passwordAgain?: string;
}

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

export interface Tokens {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshResponse extends APIBaseResponseType, APIMessageResponseType, Tokens {
  accessTokenExpiration?: string;
  refreshTokenExpiration?: string;
}

export interface SearchableSelectionOptionType {
  text: string;
  value: number;
}

export interface Game {
  name: string;
  backgroundImage: string;
  id: number;
  dateAdded: string;
  dateUpdated: string;
}

export interface GamesResponseType extends APIBaseResponseType {
  next: string;
  previous: string;
  results: Array<Game>;
}
