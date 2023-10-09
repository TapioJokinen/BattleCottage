import { BASE_URL } from '@/utils/urls';
import {
  LoginCredentialType,
  LoginResponseType,
  RefreshResponse,
  Tokens,
  VerifyResponseType,
} from '../types';
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

export async function authRefresh(tokens: Tokens) {
  const response = await fetch(`${BASE_URL}/api/a/refresh`, {
    method: 'POST',
    headers: makeRequestHeaders(),
    body: JSON.stringify(tokens),
  });

  return handleResponse<RefreshResponse>(response);
}
