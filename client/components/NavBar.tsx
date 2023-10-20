'use client';

import { authRevoke } from '@/app/api/auth';
import { signOut } from 'next-auth/react';
import { useSession } from 'next-auth/react';

export default function NavBar() {
  const session = useSession();

  async function handleLogout() {
    const revokeResponse = await authRevoke(session.data?.accessToken);

    if (!revokeResponse.responseOk) {
      console.error(revokeResponse.message);
    }

    await signOut({ callbackUrl: '/' });
  }

  return (
    <nav className="border-b-[1px] border-b-[var(--palette-ebony-clay)] bg-[var(--palette-onyx)]">
      <div className="flex flex-wrap justify-between p-2">
        <a href="/cottage" className="flex items-center">
          <span className="font-russoone text-[1.5em] sm:text-[2em]">Battle Cottage</span>
        </a>
        <button
          data-collapse-toggle="navbar-default"
          type="button"
          className="inline-flex h-10 w-10 items-center justify-center rounded-lg p-2 text-sm focus:outline-none active:bg-[var(--palette-water-blue)] md:hidden"
          aria-controls="navbar-default"
          aria-expanded="false"
        >
          <span className="sr-only">Open main menu</span>
          <svg
            className="h-5 w-5"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 17 14"
          >
            <path
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M1 1h15M1 7h15M1 13h15"
            />
          </svg>
        </button>
        <div className="hidden md:block">
          <ul className="flex flex-row p-2">
            <li className="block border-b-2 border-b-transparent py-2 pl-3 pr-4 hover:border-b-2 hover:border-b-[var(--text-light)] active:border-b-[var(--palette-water-blue)] active:text-[var(--palette-water-blue)]">
              <a href="/cottage">Home</a>
            </li>
            <li className="block border-b-2 border-b-transparent py-2 pl-3 pr-4 hover:border-b-2 hover:border-b-[var(--text-light)] active:border-b-[var(--palette-water-blue)] active:text-[var(--palette-water-blue)]">
              <a href="#">FAQ</a>
            </li>
            <button
              onClick={handleLogout}
              className="mx-3 block rounded bg-[var(--palette-light-burgundy)] py-2 pl-3 pr-4 shadow-md hover:bg-[var(--palette-light-burgundy-35)]"
            >
              Log out
            </button>
          </ul>
        </div>
      </div>
    </nav>
  );
}
