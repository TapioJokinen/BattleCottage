import { ReactNode } from 'react';
import Sidebar from '../Sidebar';
import Profile from '../profile/Profile';
import Avatar from '../profile/Avatar';
import HamburgerMenuIcon from '../icons/HamburgerMenuIcon';

export default function SidebarLayout({ children }: { children: ReactNode }) {
  return (
    <main className="main">
      <div className="hidden h-full sm:flex">
        <Sidebar>
          <Profile />
        </Sidebar>
        {children}
      </div>
      <div className="flex h-full flex-col sm:hidden">
        <nav className="static flex h-[60px] flex-row-reverse items-center justify-between bg-[var(--palette-grey)]">
          <Avatar />
          <span className="font-sourcecodepro">Battle Cottage</span>
          <HamburgerMenuIcon />
        </nav>
        {children}
      </div>
    </main>
  );
}
