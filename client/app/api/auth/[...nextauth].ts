import CredentialsProvider from 'next-auth/providers/credentials';
import { authLogin } from '../auth';
import { LoginCredentialType } from '@/app/types';

const authOptions = {
  providers: [
    CredentialsProvider({
      // The name to display on the sign in form (e.g. "Sign in with...")
      name: 'Credentials',
      // `credentials` is used to generate a form on the sign in page.
      // You can specify which fields should be submitted, by adding keys to the `credentials` object.
      // e.g. domain, username, password, 2FA token, etc.
      // You can pass any HTML attribute to the <input> tag through the object.

      //@ts-ignore
      async authorize(credentials: LoginCredentialType) {
        // Add logic here to look up the user from the credentials supplied
        const data = await authLogin({
          email: credentials.email,
          password: credentials.password,
        });

        if (data.responseOk) {
          // Any object returned will be saved in `user` property of the JWT
          return data.email;
        } else {
          // If you return null then an error will be displayed advising the user to check their details.
          return null;

          // You can also Reject this callback with an Error thus the user will be sent to the error page with the error message as a query parameter
        }
      },
    }),
  ],

  session: {
    strategy: 'jwt',
  },
};
