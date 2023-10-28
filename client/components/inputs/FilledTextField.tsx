'use client';

import { useState } from 'react';

interface FilledTextFieldProps {
  type: string;
  label?: string;
  className?: string;
  value?: string;
  autocomplete?: string;
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  error?: string;
  containerClassName?: string;
}

export default function FilledTextField({
  type,
  label,
  value,
  autocomplete,
  onChange,
  error,
  containerClassName,
}: FilledTextFieldProps) {
  const [focused, setFocused] = useState<boolean>(false);

  return (
    <div
      className={`relative mb-7 flex max-h-[54px] w-full flex-col justify-center bg-transparent ${containerClassName}`}
    >
      <fieldset>
        <legend
          className={`absolute text-[#868b8b] ${
            focused || value ? 'z-40 animate-input_label text-xs' : 'left-[18px] top-[15.5px]'
          }`}
        >
          {label}
        </legend>
        <input
          type={type}
          value={value}
          onChange={onChange}
          autoComplete={autocomplete}
          className="relative z-30 h-full w-full rounded bg-[var(--palette-black-20)] p-[18px] pl-4 shadow-[0_1px_0_var(--palette-light-burgundy)] focus:shadow-[0_1px_0_var(--text-light)] focus:outline-none"
          onFocus={() => setFocused(true)}
          onBlur={() => setFocused(false)}
        />
        <span className="absolute bottom-[-25px] left-[5px] text-[var(--error)]">
          {error && error.length > 0 ? error : ''}
        </span>
      </fieldset>
    </div>
  );
}
