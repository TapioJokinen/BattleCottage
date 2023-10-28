import { useState } from 'react';

import FilledTextField from '@/components/inputs/FilledTextField';
import SquaredButton from '../buttons/SquaredButton';
import { authRegister } from '@/app/api/auth';
import { signIn } from 'next-auth/react';

export default function RegisterForm() {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [passwordAgain, setPasswordAgain] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(false);

  function handleEmailChange(event: React.ChangeEvent<HTMLInputElement>) {
    setError('');
    setEmail(event.target.value);
  }

  function handlePasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
    setError('');
    setPassword(event.target.value);
  }

  function handlePasswordAgainChange(event: React.ChangeEvent<HTMLInputElement>) {
    setError('');
    setPasswordAgain(event.target.value);
  }

  async function handleRegister(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setError('');
    setLoading(true);

    const registerResponse = await authRegister({ email, password, passwordAgain });

    if (!registerResponse.responseOk) {
      setError(registerResponse?.message || 'Registering user failed. Try again later.');
    } else {
      const loginResponse = await signIn('credentials', {
        email: email,
        password: password,
        redirect: false,
      });

      if (!loginResponse?.ok) {
        setError(loginResponse?.error || 'Sign-In failed. Try again later.');
      } else {
        window.location.href = '/cottage';
      }
    }

    setLoading(false);
  }

  return (
    <form
      autoComplete="off"
      onSubmit={handleRegister}
      className="mt-10 flex w-full flex-col items-center"
    >
      <FilledTextField
        type="email"
        label="Email"
        value={email}
        onChange={handleEmailChange}
        autocomplete="none"
        containerClassName="max-w-[368px]"
      />
      <FilledTextField
        type="password"
        label="Password"
        value={password}
        onChange={handlePasswordChange}
        autocomplete="new-password"
        containerClassName="max-w-[368px]"
      />
      <FilledTextField
        type="password"
        label="Password again"
        value={passwordAgain}
        onChange={handlePasswordAgainChange}
        autocomplete="new-password"
        containerClassName="max-w-[368px]"
      />
      <span className="flex h-[32px] items-center text-[var(--error)]">{error}</span>
      <SquaredButton
        type="submit"
        text="Register"
        isActive={true}
        loading={loading}
        className="rounded hover:border-[var(--palette-ebony-clay)] hover:bg-[var(--palette-water-blue)] hover:text-[var(--text-light)] sm:text-[1.3em]"
      />
    </form>
  );
}
