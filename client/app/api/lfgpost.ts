import { BASE_URL } from '@/app/lib/utils/urls';
import { LFGPostCreateResponseType, LFGPostFormOptionsResponseType } from '@/app/lib/types/api';
import { handleResponse, makeRequestHeaders } from '@/app/api/base';
import { LFGPost } from '../lib/types/components';

export async function lfgPostCreate(post: LFGPost, token?: string) {
  const response = await fetch(`${BASE_URL}/api/lfgposts`, {
    method: 'POST',
    headers: makeRequestHeaders(token),
    body: JSON.stringify(post),
  });

  return handleResponse<LFGPostCreateResponseType>(response);
}

export async function lfgPostGetFormOptions(token?: string) {
  const response = await fetch(`${BASE_URL}/api/lfgposts/form-options`, {
    method: 'GET',
    headers: makeRequestHeaders(token),
  });

  return handleResponse<LFGPostFormOptionsResponseType>(response);
}
