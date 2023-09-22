/* Credentials which user uses to sign-in. */
export interface LoginCredentialType {
  email: string | undefined;
  password: string | undefined;
}

export interface APIBaseResponseType {
  responseOk: boolean;
}

export interface LoginResponseType extends APIBaseResponseType {
  accessToken: string;
  refreshToken: string;
  expiration: string;
  message: string | undefined;
}

export interface VerifyResponseType extends LoginResponseType {}

export interface Tokens {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshResponse extends APIBaseResponseType, Tokens {
  accessTokenExpiration: string;
  refreshTokenExpiration: string;
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
