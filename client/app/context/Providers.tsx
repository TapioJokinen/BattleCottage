'use client';

import { ReactNode } from 'react';
import { SessionProvider } from 'next-auth/react';
import { AlertProvider } from './AlertProvider';

export default function Providers({ children }: { children: ReactNode }) {
  return (
    <SessionProvider
      refetchInterval={60}
      refetchOnWindowFocus={typeof navigator !== 'undefined' && navigator.onLine}
      refetchWhenOffline={false}
    >
      <AlertProvider>{children}</AlertProvider>
    </SessionProvider>
  );
}
