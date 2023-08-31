'use client';

import Header from '@/components/home/Header';
import Link from 'next/link';
import { ChangeEvent, useEffect, useRef, useState } from 'react';
import { signIn } from 'next-auth/react';
import { useRouter } from 'next/navigation';
import FormContainer from '@/components/forms/FormContainer';
import FormInput from '@/components/forms/FormInput';
import FormActionButton from '@/components/forms/FormActionButton';
import FormLinkButton from '@/components/forms/FormLinkButton';

export default function LoginPage() {
  const router = useRouter();

  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string | null | undefined>('');

  function handleEmailChange(e: ChangeEvent<HTMLInputElement>) {
    setError('');
    setEmail(e.target.value);
  }

  function handlePasswordChange(e: ChangeEvent<HTMLInputElement>) {
    setError('');
    setPassword(e.target.value);
  }

  async function handleLogin() {
    setError('');
    const response = await signIn('credentials', {
      email: email,
      password: password,
      redirect: false,
    });

    if (!response?.ok) {
      setError(response?.error);
    } else {
      router.push('/cottage');
    }
  }

  return (
    <main className="main items-center">
      <Header textMain="Sign-In" textSecondary="Let's hope you have sharpened your blades..." />
      <FormContainer>
        <FormInput
          inputId="email"
          inputValue={email}
          inputOnChange={handleEmailChange}
          inputType="email"
          label="Email"
        />
        <FormInput
          inputId="password"
          inputValue={password}
          inputOnChange={handlePasswordChange}
          inputType="password"
          label="Password"
        />
        {error && <span className="text-sm text-[#f44336]">{error}</span>}
        <FormActionButton text="⚔️ Login ⚔️" onClick={handleLogin} />
        <FormLinkButton text="I forgot my password" link="/recover-password" />
        <FormLinkButton text="Register here!" link="/register" />
      </FormContainer>
    </main>
  );
}
