import { useState } from 'react';
import FilledTextField from '../inputs/FilledTextField';
import SquaredButton from '../buttons/SquaredButton';
import { signIn } from 'next-auth/react';

export default function SignInForm() {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
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

  async function handleLogin(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setError('');
    setLoading(true);

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

    setLoading(false);
  }

  return (
    <form onSubmit={handleLogin} className="mt-10 flex w-full flex-col items-center">
      <FilledTextField
        type="email"
        label="Email"
        value={email}
        onChange={handleEmailChange}
        autocomplete="on"
        containerClassName="max-w-[368px]"
      />
      <FilledTextField
        type="password"
        label="Password"
        value={password}
        onChange={handlePasswordChange}
        autocomplete="off"
        containerClassName="max-w-[368px]"
      />
      <span className="flex h-[32px] items-center text-[var(--error)]">{error}</span>
      <SquaredButton type="submit" text="Log in" isActive={true} loading={loading} />
    </form>
  );
}
