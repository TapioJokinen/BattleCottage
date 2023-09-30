import Link from 'next/link';
import { ReactNode } from 'react';
import PlusIcon from '../icons/PlusIcon';

const navbarLinks = [
  {
    text: 'Home',
    link: '/cottage',
  },
  {
    text: 'My Groups',
    link: '/lfg',
  },
  {
    text: 'FAQ',
    link: '/faq',
  },
];

export default function NavbarLayout({ children }: { children: ReactNode }) {
  return (
    <main className="main">
      <div
        className={`hidden w-full items-center justify-between bg-[var(--palette-onyx)] font-sourcecodepro shadow-[0px_2px_2px_-2px_var(--palette-smoky-topaz)] sm:flex`}
      >
        <div className="flex py-1">
          {navbarLinks.map(({ text, link }) => (
            <Link
              className="mr-2 w-fit min-w-[105px] rounded p-2 text-center hover:bg-[var(--palette-dark-grey)]"
              href={link}
              key={text}
            >
              {text}
            </Link>
          ))}
        </div>
        <Link
          className="mr-5 flex items-center rounded bg-[var(--palette-success)] p-1 px-2 hover:bg-[var(--palette-success-darker)]"
          href="/lfg/create"
        >
          <PlusIcon classname="h-3 w-3" />
          <span className="ml-1">Create a Post</span>
        </Link>
      </div>
      {children}
    </main>
  );
}
