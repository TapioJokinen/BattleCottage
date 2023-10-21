import Spinner from '@/components/Spinner';
import { ButtonHTMLAttributes } from 'react';

interface SquaredButtonProps {
  text: string;
  type: ButtonHTMLAttributes<HTMLButtonElement>['type'];
  fontSize?: string;
  isActive: boolean;
  loading: boolean;
  onClick?: () => void;
  className?: string;
}

export default function SquaredButton({
  text,
  type,
  isActive,
  loading,
  onClick,
  className,
}: SquaredButtonProps) {
  return (
    <button
      type={type}
      onClick={onClick}
      className={`no-select flex justify-center border-[1px] ${
        isActive ? 'border-[var(--palette-light-burgundy)]' : 'border-transparent'
      } m-2 w-[156px] rounded bg-[var(--palette-black-50)] p-2 px-7 text-[var(--palette-light-burgundy)] shadow-md hover:border-[var(--palette-ebony-clay)] hover:bg-[var(--palette-water-blue)] hover:text-[var(--text-light)] active:animate-press sm:text-[1.3em] ${className}`}
    >
      {loading ? <Spinner /> : text}
    </button>
  );
}
