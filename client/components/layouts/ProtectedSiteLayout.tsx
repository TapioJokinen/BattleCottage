import { ReactNode } from 'react';
import { getServerSession } from 'next-auth/next';
import { authOptions } from '@/app/api/auth/[...nextauth]/route';

export default async function ProtectedSiteLayout({ children }: { children: ReactNode }) {
  const session = await getServerSession(authOptions);

  function isUserAuthenticated() {
    if (
      session?.accessToken &&
      session?.refreshToken &&
      session.email &&
      session.refreshTokenExpiration * 1000 >= Date.now()
    ) {
      return true;
    }
    return false;
  }

  return isUserAuthenticated() ? (
    <>{children}</>
  ) : (
    <div>
      <span>You need to log in to view this page :|</span>
    </div>
  );
}
