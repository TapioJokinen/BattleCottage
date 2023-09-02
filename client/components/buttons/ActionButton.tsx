import 'server-only';

import Link from 'next/link';
import { ReactNode } from 'react';

export default function ActionButton({
  icon,
  link,
  primaryText,
  secondaryText,
}: {
  icon: ReactNode;
  link: string;
  primaryText: string;
  secondaryText: string;
}) {
  return (
    <Link role="button" href={link} className="action-button-link">
      <div className="mr-3">{icon}</div>
      <div className="flex max-w-[240px] flex-col">
        <span className="font-sourcecodepro text-lg">{primaryText}</span>
        <span className="hidden text-sm opacity-30 lg:block">{secondaryText}</span>
      </div>
    </Link>
  );
}
