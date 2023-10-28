'use client';

import { useState } from 'react';

interface FilledTextFieldProps {
  label?: string;
  className?: string;
  value?: string;
  autocomplete?: string;
  onChange?: (event: React.ChangeEvent<HTMLTextAreaElement>) => void;
  error?: string;
}

export default function FilledTextArea({
  label,
  value,
  autocomplete,
  onChange,
  error,
}: FilledTextFieldProps) {
  const [focused, setFocused] = useState<boolean>(false);

  function handleFocused() {
    setFocused(true);
  }

  function handleBlurred() {
    setFocused(false);
  }
  return (
    <div className="relative my-3 mb-7 flex min-h-[54px] w-full flex-col justify-center bg-transparent">
      <fieldset>
        <legend
          className={`absolute text-[#868b8b] ${
            focused || value ? 'z-40 animate-input_label text-xs' : 'left-[18px] top-[15.5px]'
          }`}
        >
          {label}
        </legend>
        <textarea
          value={value}
          onChange={onChange}
          autoComplete={autocomplete}
          className="relative z-30 h-full w-full resize rounded bg-[var(--palette-black-20)] p-[18px] pl-4 shadow-[0_1px_0_var(--palette-light-burgundy)] focus:shadow-[0_1px_0_var(--text-light)] focus:outline-none"
          onFocus={handleFocused}
          onBlur={handleBlurred}
        />
        <span className="absolute bottom-[-25px] left-[5px] text-[var(--error)]">
          {error && error.length > 0 ? error : ''}
        </span>
      </fieldset>
    </div>
  );
}
