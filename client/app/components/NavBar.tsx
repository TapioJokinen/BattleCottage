'use client';

import { authRevoke } from '@/app/api/auth';
import { signOut } from 'next-auth/react';
import { useSession } from 'next-auth/react';
import SquaredButton from './buttons/SquaredButton';
import { useState } from 'react';
import Link from 'next/link';
import useAlert from '@/app/hooks/useAlert';

export default function NavBar() {
  const session = useSession();
  const alert = useAlert();

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
      alert.raiseAlert(
        'Something failed while signing out. You should be good though :)',
        'warning',
      );
    }

    await signOut({ callbackUrl: '/' });

    alert.raiseAlert('You have been signed out.', 'info');

    setLoading(false);
  }

  return (
    <nav className="navbar-main">
      <div className="navbar-content">
        <a href="/cottage" className="navbar-title-wrapper">
          <span className="navbar-title">Battle Cottage</span>
        </a>
        <button
          data-collapse-toggle="navbar-default"
          type="button"
          className="navbar-burgermenu"
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
        <div className="navbar-link-container" id="navbar-default">
          <ul className="navar-link-wrapper">
            <Link href="/cottage" className="navbar-link">
              <span>Home</span>
            </Link>
            <Link href="/faq" className="navbar-link">
              <span>FAQ</span>
            </Link>
            <SquaredButton
              type="button"
              text="Log out"
              isActive={true}
              onClick={handleLogout}
              loading={loading}
              className="squared-button-main bg-[var(--palette-light-burgundy-35)] text-[var(--text-light)]"
            />
          </ul>
        </div>
      </div>
    </nav>
  );
}
