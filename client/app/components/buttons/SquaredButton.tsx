import Spinner from '@/app/components/Spinner';
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
      /* For some reason bg-color has to be set here and not in .css file. If set in file it gets
      overridden to transparent */
      className={`${className || 'squared-button-main bg-[var(--palette-black-50)]'}  no-select ${
        isActive ? 'border-[var(--palette-light-burgundy)]' : 'border-transparent'
      }`}
    >
      {loading ? <Spinner /> : text}
    </button>
  );
}
