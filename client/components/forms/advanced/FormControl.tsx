import CheckCircleIcon from '@/components/icons/CheckCircleIcon';
import CloseCircleIcon from '@/components/icons/CloseCircleIcon';
import { ReactNode } from 'react';

export default function FormControl({
  text,
  ok,
  error,
  children,
}: {
  text: string;
  ok?: boolean;
  error?: string;
  children: ReactNode;
}) {
  function getStatus() {
    if (error) {
      return (
        <>
          <CloseCircleIcon className="ml-2 h-5 w-5 text-[var(--palette-error)]" />
          <span className="ml-1 text-[var(--palette-error)]">{error}</span>
        </>
      );
    }
    if (ok) {
      return <CheckCircleIcon className="ml-2 h-5 w-5 text-[var(--palette-success)]" />;
    }
    return '';
  }

  return (
    <div className="form-control">
      {(text || ok || error) && (
        <div className="my-3 flex items-center">
          <span className="font-sourcecodepro text-xl">{text}</span>
          {getStatus()}
        </div>
      )}
      {children}
    </div>
  );
}
