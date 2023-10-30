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
    <div className="filled-textarea-main">
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
          className="filled-textarea-textarea"
          onFocus={handleFocused}
          onBlur={handleBlurred}
        />
        <span className="filled-textarea-error">{error && error.length > 0 ? error : ''}</span>
      </fieldset>
    </div>
  );
}
