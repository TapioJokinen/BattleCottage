import { ReactNode } from 'react';

export default function NavbarLayout({ children }: { children: ReactNode }) {
  return (
    <main className="main">
      <div className="hidden w-full bg-[var(--palette-onyx)] p-2 shadow-lg lg:block">
        <span>Nav bar</span>
      </div>
      {children}
    </main>
  );
}
