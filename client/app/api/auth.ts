import { BASE_URL } from '@/app/lib/utils/urls';
import {
  LoginResponseType,
  RefreshResponse,
  RegisterResponseType,
  RevokeResponseType,
  VerifyResponseType,
} from '@/app/lib/types/api';
import { LoginCredentialType, RegisterCredentialType, Tokens } from '@/app/lib/types/auth';
import { handleResponse, makeRequestHeaders } from './base';

export async function authLogin(credentials: LoginCredentialType) {
  const response = await fetch(`${BASE_URL}/api/a/login`, {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify(credentials),
  });

  return handleResponse<LoginResponseType>(response);
}

export async function authVerify() {
  const response = await fetch(`${BASE_URL}/api/a/verify`, {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify({}),
  });

  return handleResponse<VerifyResponseType>(response);
}

export async function authRevoke(token?: string) {
  const response = await fetch(`${BASE_URL}/api/a/revoke`, {
    method: 'POST',
    headers: makeRequestHeaders(token),
    body: JSON.stringify({}),
  });

  return handleResponse<RevokeResponseType>(response);
}

export async function authRefresh(tokens: Tokens) {
  const response = await fetch(`${BASE_URL}/api/a/refresh`, {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify(tokens),
  });

  return handleResponse<RefreshResponse>(response);
}

export async function authRegister(credentials: RegisterCredentialType) {
  const response = await fetch(`${BASE_URL}/api/a/register`, {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify(credentials),
  });

  return handleResponse<RegisterResponseType>(response);
}
