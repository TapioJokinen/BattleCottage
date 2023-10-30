import NavBar from '@/app/components/NavBar';
import ProtectedSiteLayout from '@/app/components/layouts/ProtectedSiteLayout';
import { ReactNode } from 'react';

export default function FAQLayout({ children }: { children: ReactNode }) {
  return (
    <ProtectedSiteLayout>
      <NavBar />
      {children}
    </ProtectedSiteLayout>
  );
}
