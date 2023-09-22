export async function handleResponse<T>(response: Response): Promise<T> {
  const contentType = response.headers.get('Content-Type') || '';
  const isJson = contentType.includes('application/json');
  const data = isJson ? await response.json() : await response.text();

  data.responseOk = response.ok;

  return data as T;
}

export function makeRequestHeaders(token: string | null | undefined = null): HeadersInit {
  const requestHeaders: HeadersInit = new Headers();

  requestHeaders.set('Content-Type', 'application/json');

  if (token) {
    requestHeaders.set('Authorization', `Bearer ${token}`);
  }

  return requestHeaders;
}
