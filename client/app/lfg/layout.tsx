import NavbarLayout from '@/components/layouts/NavbarLayout';
import ProtectedSiteLayout from '@/components/layouts/ProtectedSiteLayout';
import SidebarLayout from '@/components/layouts/SidebarLayout';
import { ReactNode } from 'react';

export default function LFGLayout({ children }: { children: ReactNode }) {
  return (
    <SidebarLayout>
      <NavbarLayout>
        <ProtectedSiteLayout>{children}</ProtectedSiteLayout>
      </NavbarLayout>
    </SidebarLayout>
  );
}
