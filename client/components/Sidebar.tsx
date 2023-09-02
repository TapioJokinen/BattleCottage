import { ReactNode } from 'react';

export default function Sidebar({ children }: { children: ReactNode }) {
  return (
    <div className="hidden h-full flex-col items-center bg-[var(--base-dark)] sm:flex sm:w-[90px] xl:w-[300px]">
      {children}
    </div>
  );
}
