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
    <nav className="border-b-[1px] border-b-[var(--palette-light-burgundy)] bg-[var(--palette-onyx)]">
      <div className="flex flex-wrap justify-between p-2">
        <a href="/cottage" className="flex items-center">
          <span className="font-russoone text-[2em]">Battle Cottage</span>
        </a>
        <div>
          <ul className="flex flex-row p-2">
            <li className="block border-b-2 border-b-transparent py-2 pl-3 pr-4 hover:border-b-2 hover:border-b-[var(--text-light)]">
              <a href="/cottage">Home</a>
            </li>
            <li className="block border-b-2 border-b-transparent py-2 pl-3 pr-4 hover:border-b-2 hover:border-b-[var(--text-light)]">
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
