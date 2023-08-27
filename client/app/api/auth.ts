import { LoginCredentialType, LoginResponseType, VerifyResponseType } from '../types';
import { handleResponse, makeRequestHeaders } from './base';

export async function authLogin(credentials: LoginCredentialType) {
  const response = await fetch('https://localhost:7069/api/auth/login', {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify(credentials),
    credentials: 'include',
  });

  return handleResponse<LoginResponseType>(response);
}

export async function authVerify() {
  const response = await fetch('https://localhost:7069/api/auth/verify', {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify({}),
    credentials: 'include',
  });

  return handleResponse<VerifyResponseType>(response);
}
