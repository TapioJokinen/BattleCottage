interface RoundedButtonProps {
  text: string;
  fontSize?: string;
  isActive: boolean;
  onClick: () => void;
  bgColor?: string;
}

export default function RoundedButton({ text, isActive, onClick }: RoundedButtonProps) {
  return (
    <button
      onClick={onClick}
      className={`rounded-button-main no-select ${
        isActive ? 'border-[var(--palette-light-burgundy)]' : 'border-transparent'
      }`}
    >
      {text}
    </button>
  );
}
