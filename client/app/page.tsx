'use client';

import { useRouter } from 'next/navigation';
import { useSession } from 'next-auth/react';

import AuthContainer from '@/components/AuthContainer';
import LandingTitle from '@/components/LandingTitle';

export default function Home() {
  const { status } = useSession();
  const router = useRouter();

  if (status === 'authenticated') {
    router.push('/cottage');
  }

  return (
    <main className="main">
      {status !== 'authenticated' && (
        <div className="h-full w-full">
          <LandingTitle />
          <AuthContainer />
        </div>
      )}
    </main>
  );
}
