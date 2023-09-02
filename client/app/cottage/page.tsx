import Sidebar from '@/components/Sidebar';
import ActionButton from '@/components/buttons/ActionButton';
import CottageContent from '@/components/cottage/CottageContent';
import HamburgerMenuIcon from '@/components/icons/HamburgerMenuIcon';
import Avatar from '@/components/profile/Avatar';
import Profile from '@/components/profile/Profile';

export default function CottagePage() {
  return (
    <main className="main">
      <div className="hidden h-full sm:flex">
        <Sidebar>
          <Profile />
        </Sidebar>
        <CottageContent />
      </div>
      <div className="flex h-full flex-col sm:hidden">
        <nav className="static flex h-[60px] flex-row-reverse items-center justify-between bg-[var(--palette-grey)]">
          <Avatar />
          <span className="font-sourcecodepro">Battle Cottage</span>
          <HamburgerMenuIcon />
        </nav>
        <CottageContent />
      </div>
    </main>
  );
}
