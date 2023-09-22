import { ReactNode } from 'react';

export default function Option({ text, children }: { text: string; children: ReactNode }) {
  return (
    <div className="lfg-create-option">
      <div className="my-3 flex">
        <span className="font-sourcecodepro text-xl">{text}</span>
      </div>
      {children}
    </div>
  );
}
