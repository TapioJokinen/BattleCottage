import NavBar from '@/components/NavBar';
import ProtectedSiteLayout from '@/components/layouts/ProtectedSiteLayout';
import { ReactNode } from 'react';

export default function FAQLayout({ children }: { children: ReactNode }) {
  return (
    <ProtectedSiteLayout>
      <NavBar />
      {children}
    </ProtectedSiteLayout>
  );
}
