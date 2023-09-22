'use client';

import Header from '@/components/home/Header';
import { ChangeEvent, useState } from 'react';
import { signIn, useSession } from 'next-auth/react';
import FormContainer from '@/components/forms/FormContainer';
import FormInput from '@/components/forms/FormInput';
import SlidingActionButton from '@/components/buttons/SlidingActionButton';
import FormLinkButton from '@/components/forms/FormLinkButton';
import Spinner from '@/components/Spinner';
import { useRouter } from 'next/navigation';

export default function LoginPage() {
  const { status } = useSession();
  const router = useRouter();
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string | null | undefined>('');
  const [loading, setLoading] = useState<boolean>(false);

  if (status === 'authenticated') {
    router.push('/cottage');
  }

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
    setLoading(true);

    const response = await signIn('credentials', {
      email: email,
      password: password,
      redirect: false,
    });

    if (!response?.ok) {
      setError(response?.error || 'Sign-In failed. Try again later.');
    } else {
      window.location.href = '/cottage';
    }

    setLoading(false);
  }

  return (
    <main className="main items-center">
      {status === 'unauthenticated' && (
        <>
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
            <SlidingActionButton text={loading ? <Spinner /> : 'Login'} onClick={handleLogin} />
            <FormLinkButton text="I forgot my password" link="/recover-password" />
            <FormLinkButton text="Register here!" link="/register" />
          </FormContainer>
        </>
      )}
    </main>
  );
}
