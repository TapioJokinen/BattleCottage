import NavbarLayout from '@/components/layouts/NavbarLayout';
import SidebarLayout from '@/components/layouts/SidebarLayout';
import { ReactNode } from 'react';

export default function LFGLayout({ children }: { children: ReactNode }) {
  return (
    <SidebarLayout>
      <NavbarLayout>{children}</NavbarLayout>
    </SidebarLayout>
  );
}
