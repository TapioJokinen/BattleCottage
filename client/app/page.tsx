'use client';

import { useRouter } from 'next/navigation';
import { useSession } from 'next-auth/react';

import AuthContainer from '@/app/components/AuthContainer';
import LandingTitle from '@/app/components/LandingTitle';
import { useEffect } from 'react';

export default function Home() {
  const { status } = useSession();
  const router = useRouter();

  console.log(status);

  useEffect(() => {
    if (status === 'authenticated') {
      router.push('/cottage');
    }
  }, [status]);

  return (
    <main className="main">
      {status === 'unauthenticated' && (
        <div className="h-full w-full">
          <LandingTitle />
          <AuthContainer />
        </div>
      )}
    </main>
  );
}
