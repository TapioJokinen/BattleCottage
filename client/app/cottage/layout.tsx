import SidebarLayout from '@/components/layouts/SidebarLayout';
import { ReactNode } from 'react';

export default function CottageLayout({ children }: { children: ReactNode }) {
  return <SidebarLayout>{children}</SidebarLayout>;
}
