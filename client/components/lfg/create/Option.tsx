import { ReactNode } from 'react';

export default function Option({ text, children }: { text: string; children: ReactNode }) {
  return (
    <div className="lfg-create-option">
      <span className="font-sourcecodepro text-2xl">{text}</span>
      {children}
    </div>
  );
}
