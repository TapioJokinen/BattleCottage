'use client';

interface FilledTextFieldProps {
  placeholder: string;
  className?: string;
  value?: string;
  autocomplete?: string;
  onChange?: (event: React.ChangeEvent<HTMLTextAreaElement>) => void;
  error?: string;
}

export default function FilledTextArea({
  placeholder,
  value,
  autocomplete,
  onChange,
  error,
}: FilledTextFieldProps) {
  return (
    <div className="relative my-3 mb-7 flex min-h-[54px] w-full max-w-[368px] flex-col justify-center bg-transparent">
      <textarea
        placeholder={placeholder}
        value={value}
        onChange={onChange}
        autoComplete={autocomplete}
        className="h-full w-full resize-y rounded bg-[var(--palette-black-20)] p-3 pl-4 shadow-[0_1px_0_var(--palette-light-burgundy)] focus:shadow-[0_1px_0_var(--text-light)] focus:outline-none"
      />
      <span className="absolute bottom-[-25px] left-[5px] text-[var(--error)]">
        {error && error.length > 0 ? error : ''}
      </span>
    </div>
  );
}
