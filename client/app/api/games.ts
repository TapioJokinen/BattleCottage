import { BASE_URL } from '@/utils/urls';
import { GamesResponseType } from '../types';
import { handleResponse, makeRequestHeaders } from './base';

export async function gamesFetchByName(
  name: string,
  page: number,
  pageSize: number,
  token: string | null | undefined,
) {
  let url = new URL(`${BASE_URL}/api/games`);
  const params = new URLSearchParams({
    contains: name,
    page: page.toString(),
    pageSize: pageSize.toString(),
  });

  const response = await fetch(`${url}?${params}`, {
    method: 'GET',
    headers: makeRequestHeaders(token),
  });

  return handleResponse<GamesResponseType>(response);
}
