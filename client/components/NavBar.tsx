'use client';

import { authRevoke } from '@/app/api/auth';
import { signOut } from 'next-auth/react';
import { useSession } from 'next-auth/react';
import SquaredButton from './buttons/SquaredButton';
import { useState } from 'react';
import Link from 'next/link';

export default function NavBar() {
  const session = useSession();

  const [loading, setLoading] = useState<boolean>(false);

  function handleBurgerMenu() {
    const navbar = document.getElementById('navbar-default')!;

    if (navbar.classList.contains('hidden')) {
      navbar.classList.remove('hidden');
    } else {
      navbar.classList.add('hidden');
    }
  }

  async function handleLogout() {
    setLoading(true);

    const revokeResponse = await authRevoke(session.data?.accessToken);

    if (!revokeResponse.responseOk) {
      console.error(revokeResponse.message);
    }

    await signOut({ callbackUrl: '/' });

    setLoading(false);
  }

  return (
    <nav className="border-b-2 border-b-[var(--palette-baltic-sea)] font-roboto shadow-lg">
      <div className="flex flex-wrap justify-between p-2 pl-3 sm:items-center">
        <a href="/cottage" className="flex items-center">
          <span className="font-permanentmarker text-[1.5em] sm:text-[3em]">Battle Cottage</span>
        </a>
        <button
          data-collapse-toggle="navbar-default"
          type="button"
          className="inline-flex h-10 w-10 items-center justify-center rounded-lg p-2 text-sm focus:outline-none active:bg-[var(--palette-water-blue)] md:hidden"
          aria-controls="navbar-default"
          aria-expanded="false"
          onClick={handleBurgerMenu}
        >
          <span className="sr-only">Open menu</span>
          <svg
            className="h-5 w-5"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 17 14"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M1 1h15M1 7h15M1 13h15"
            />
          </svg>
        </button>
        <div className="hidden w-full md:block md:w-auto" id="navbar-default">
          <ul className="flex flex-col items-center p-2 md:flex-row">
            <Link
              href="/cottage"
              className="block border-b-2 border-b-transparent py-2 pl-3 pr-4 hover:border-b-2 hover:border-b-[var(--text-light)] active:border-b-[var(--palette-water-blue)] active:text-[var(--palette-water-blue)]"
            >
              <span>Home</span>
            </Link>
            <Link
              href="/faq"
              className="block border-b-2 border-b-transparent py-2 pl-3 pr-4 hover:border-b-2 hover:border-b-[var(--text-light)] active:border-b-[var(--palette-water-blue)] active:text-[var(--palette-water-blue)]"
            >
              <span>FAQ</span>
            </Link>
            <SquaredButton
              type="button"
              text="Log out"
              isActive={true}
              onClick={handleLogout}
              loading={loading}
              className="bg-[var(--palette-light-burgundy-35)] text-[var(--text-light)]"
            />
          </ul>
        </div>
      </div>
    </nav>
  );
}
