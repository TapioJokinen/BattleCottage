import CredentialsProvider from 'next-auth/providers/credentials';
import { authLogin, authRefresh } from '../../auth';
import NextAuth from 'next-auth/next';
import { User, Session } from 'next-auth';
import { JWT } from 'next-auth/jwt';

import { NextAuthOptions } from 'next-auth';

export const authOptions: NextAuthOptions = {
  providers: [
    CredentialsProvider({
      name: 'Credentials',
      id: 'credentials',

      credentials: {
        email: { label: 'Email', type: 'email' },
        password: { label: 'Password', type: 'password' },
      },

      async authorize(credentials, req) {
        const data = await authLogin({
          email: credentials?.email,
          password: credentials?.password,
        });

        if (data.responseOk) {
          return data as any;
        }

        return Promise.reject(new Error(data.message));
      },
    }),
  ],
  secret: process.env.NEXTAUTH_SECRET,

  session: {
    strategy: 'jwt',
    maxAge: 60 * 60 * 24 * 7,
  },

  pages: {
    signIn: '/login',
  },

  callbacks: {
    async jwt({ token, user }: { token: JWT; user: User }) {
      if (user) {
        token.email = user.email;
        token.accessToken = user.accessToken;
        token.accessTokenExpires = Math.floor(new Date(user.accessTokenExpires).getTime() / 1000);
        token.refreshToken = user.refreshToken;
        token.refreshTokenExpires = Math.floor(new Date(user.refreshTokenExpires).getTime() / 1000);
      }
      return token;
    },
    async session({ session, token }: { session: Session; user: User; token: JWT }) {
      session.email = token.email;
      session.accessToken = token.accessToken;
      session.accessTokenExpires = Math.floor(new Date(token.accessTokenExpires).getTime() / 1000);
      session.refreshToken = token.refreshToken;
      session.refreshTokenExpires = token.refreshTokenExpires;

      if (Date.now() / 1000 > token.accessTokenExpires) {
        const data = await authRefresh({
          accessToken: token.accessToken,
          refreshToken: token.refreshToken,
        });

        if (data.responseOk) {
          session.accessToken = data.accessToken;
          session.refreshToken = data.refreshToken;
          session.accessTokenExpires = Math.floor(
            new Date(data.accessTokenExpiration).getTime() / 1000,
          );
          session.refreshTokenExpires = Math.floor(
            new Date(data.refreshTokenExpiration).getTime() / 1000,
          );
        }
      }
      return session;
    },
  },
};

const handler = NextAuth(authOptions);

export { handler as GET, handler as POST };
