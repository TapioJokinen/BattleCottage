'use client';

import { authLogin } from '@/app/api/auth';
import Spinner from '@/app/components/Spinner';
import Header from '@/app/components/home/Header';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { useState } from 'react';

export default function LoginPage() {
  const router = useRouter();

  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(true);

  async function loginUser() {
    setLoading(true);
    const data = await authLogin({ email, password });

    if (data.responseOk) {
      router.push('/cottage');
    } else {
      console.log('failed');
    }
    setLoading(false);
  }

  return (
    <main className="main items-center">
      <Header textMain="🔥 Sign-In 🔥" textSecondary="I hope you have sharpened your blades..." />
      <div className="auth-form-container">
        <div className="auth-form-input-container">
          <div className="form-input-wrapper">
            <label htmlFor="email" className="pb-2">
              Email
            </label>
            <input
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              autoComplete="off"
              id="email"
              className="form-input"
              type="email"
            />
          </div>
          <div className="form-input-wrapper">
            <label htmlFor="password" className="pb-2">
              Password
            </label>
            <input
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              id="password"
              className="form-input"
              type="password"
            />
          </div>
          <div className="button-slide-main !mt-10 justify-center" onClick={loginUser}>
            <button>{!loading ? '⚔️ Login ⚔️' : <Spinner />}</button>
          </div>
          <div className="secondary-link">
            <Link href="/recover-password" className="underline">
              I forgot my password
            </Link>
          </div>
          <div className="secondary-link">
            <Link href="/register" className="underline">
              Register here!
            </Link>
          </div>
        </div>
      </div>
    </main>
  );
}
