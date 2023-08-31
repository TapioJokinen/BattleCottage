'use client';

import Spinner from '@/app/components/Spinner';
import Header from '@/app/components/home/Header';
import Link from 'next/link';
import { useState } from 'react';
import { signIn } from 'next-auth/react';

export default function LoginPage() {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  return (
    <main className="main items-center">
      <Header
        textMain="🔥 Sign-In 🔥"
        textSecondary="Let's hope you have sharpened your blades..."
      />
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
          <div
            className="button-slide-main !mt-10 justify-center"
            onClick={async () => {
              const response = await signIn('credentials', {
                email: email,
                password: password,
                callbackUrl: '/cottage',
              });
              console.log(response);
            }}
          >
            <button>{'⚔️ Login ⚔️'}</button>
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
