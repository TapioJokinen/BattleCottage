import { ButtonHTMLAttributes } from 'react';

interface SquaredCardProps {
  title: string;
  textSecondary: string;
  type: ButtonHTMLAttributes<HTMLButtonElement>['type'];
  fontSize?: string;
  isActive: boolean;
  onClick?: () => void;
  className?: string;
}

export default function SquaredCard({
  title,
  textSecondary,
  type,
  isActive,
  onClick,
  className,
}: SquaredCardProps) {
  return (
    <button
      type={type}
      onClick={onClick}
      className={`${
        isActive ? 'border-[var(--palette-baltic-sea)]' : 'border-transparent'
      } squared-card-main no-select gradient-1 ${className}`}
    >
      <div className="squared-card-content">
        <div className="squared-card-title">
          <span>{title}</span>
        </div>
        <div className="squared-card-divider" />
        <div className="squared-card-description-wrapper">
          <span className="squared-card-description">{textSecondary}</span>
        </div>
      </div>
    </button>
  );
}
