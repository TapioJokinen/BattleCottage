import { ButtonHTMLAttributes } from 'react';

interface SquaredCardProps {
  title: string;
  textSecondary: string;
  type: ButtonHTMLAttributes<HTMLButtonElement>['type'];
  fontSize?: string;
  isActive: boolean;
  loading: boolean;
  onClick?: () => void;
  className?: string;
}

export default function SquaredCard({
  title,
  textSecondary,
  type,
  isActive,
  loading,
  onClick,
  className,
}: SquaredCardProps) {
  return (
    <button
      type={type}
      onClick={onClick}
      className={`${
        isActive ? 'border-[var(--palette-baltic-sea)]' : 'border-transparent'
      } no-select gradient-1 m-2 flex w-full max-w-[300px] justify-start rounded border-2 p-2 pl-3 text-[var(--text-light)] shadow hover:border-[var(--palette-ebony-clay)] hover:bg-[var(--palette-water-blue)] hover:text-[var(--text-light)] active:animate-press sm:text-[1.3em] ${className}`}
    >
      <div className="flex h-full w-full flex-col items-start p-2">
        <div className="py-1">
          <span>{title}</span>
        </div>
        <div className="w-full border-[1px] border-[var(--palette-ebony-clay-35)]" />
        <div className="flex justify-start pt-1 text-sm text-[var(--palette-mid-grey)]">
          <span className="text-left">{textSecondary}</span>
        </div>
      </div>
    </button>
  );
}
