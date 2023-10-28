export interface LoginCredentialType {
  email?: string;
  password?: string;
}

export interface RegisterCredentialType extends LoginCredentialType {
  passwordAgain?: string;
}

export interface Tokens {
  accessToken: string;
  refreshToken: string;
}
