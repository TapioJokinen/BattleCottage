import { BASE_URL } from '@/app/lib/utils/urls';
import { GamesResponseType } from '@/app/lib/types/api';
import { handleResponse, makeRequestHeaders } from '@/app/api/base';

export async function gamesFetchByName(
  name: string,
  page: number,
  pageSize: number,
  token?: string,
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
