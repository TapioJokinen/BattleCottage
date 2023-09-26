import { ReactNode } from 'react';

export default function Role({ role, icon }: { role: string; icon: ReactNode }) {
  return (
    <div className="m-2 flex min-w-[200px] items-center justify-between rounded bg-[var(--palette-dark-grey)] px-5 py-2 font-sourcecodepro text-xl">
      <span className="mr-2">{role}</span>
      {icon}
    </div>
  );
}
