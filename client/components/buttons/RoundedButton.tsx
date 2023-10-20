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
      className={`no-select border-[1px] ${
        isActive ? 'border-[var(--palette-light-burgundy)]' : 'border-transparent'
      } m-2 w-[156px] rounded-full bg-[var(--palette-black-50)] p-2 px-7 text-[var(--palette-light-burgundy)] shadow-md hover:bg-[var(--palette-black-65)] active:animate-press sm:text-[1.2em]`}
    >
      {text}
    </button>
  );
}
