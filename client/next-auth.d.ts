import { DefaultJWT } from 'next-auth/jwt';

declare module 'next-auth' {
  interface Session {
    accessTokenExpiration: number;
    refreshTokenExpiration: number;
    refreshToken: string;
    accessToken: string;
    email: string;
    error?: string;
  }

  interface User {
    email: string;
    accessToken: string;
    refreshToken: string;
    accessTokenExpiration: string;
    refreshTokenExpiration: string;
  }
}

declare module 'next-auth/jwt' {
  /** Returned by the `jwt` callback and `getToken`, when using JWT sessions */
  interface JWT {
    email: string;
    accessTokenExpiration: number;
    refreshTokenExpiration: number;
    refreshToken: string;
    accessToken: string;
    exp?: number;
    iat?: number;
    jti?: string;
  }
}
