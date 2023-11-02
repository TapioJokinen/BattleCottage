import Spinner from '@/app/components/Spinner';
import { ButtonHTMLAttributes } from 'react';

/**
 * Props for the SquaredButton component.
 */
interface SquaredButtonProps {
  /** The text to display on the button. */
  text: string;
  /** The type of the button. */
  type: ButtonHTMLAttributes<HTMLButtonElement>['type'];
  /** The font size of the button text. */
  fontSize?: string;
  /** Whether the button is currently active. (Show / do not show border) */
  isActive: boolean;
  /** Whether the button is currently in a loading state. */
  loading: boolean;
  /** The function to call when the button is clicked. */
  onClick?: () => void;
  /** The CSS class name to apply to the button. */
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
