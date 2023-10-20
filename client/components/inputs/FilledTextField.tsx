'use client';

interface FilledTextFieldProps {
  type: string;
  placeholder: string;
  className?: string;
  value?: string;
  autocomplete?: string;
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function FilledTextField({
  type,
  placeholder,
  value,
  autocomplete,
  onChange,
}: FilledTextFieldProps) {
  return (
    <div className="relative my-3 flex max-h-[54px] min-h-[54px] w-full max-w-[368px] justify-center bg-transparent">
      <input
        type={type}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
        autoComplete={autocomplete}
        className="absolute h-full w-full rounded bg-[var(--palette-black-20)] p-3 pl-4 shadow-[0_1px_0_var(--palette-light-burgundy)] focus:shadow-[0_1px_0_var(--text-light)] focus:outline-none"
      />
    </div>
  );
}
