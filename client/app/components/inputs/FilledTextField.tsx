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
    <div className={`filled-textfield-main ${containerClassName}`}>
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
          className="filled-textfield-input"
          onFocus={() => setFocused(true)}
          onBlur={() => setFocused(false)}
        />
        <span className="filled-textfield-error">{error && error.length > 0 ? error : ''}</span>
      </fieldset>
    </div>
  );
}
