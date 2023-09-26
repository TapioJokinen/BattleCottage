'use client';

import { ReactNode } from 'react';
import { SessionProvider } from 'next-auth/react';

export default function Providers({ children }: { children: ReactNode }) {
  return (
    <SessionProvider
      refetchInterval={60}
      refetchOnWindowFocus={typeof navigator !== 'undefined' && navigator.onLine}
      refetchWhenOffline={false}
    >
      {children}
    </SessionProvider>
  );
}
