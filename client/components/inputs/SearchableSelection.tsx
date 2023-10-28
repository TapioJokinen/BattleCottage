'use client';

import { SearchableSelectionOptionType } from '@/lib/types/components';
import { useState } from 'react';

interface FilledTextFieldProps {
  label?: string;
  value?: string;
  options?: SearchableSelectionOptionType[];
  className?: string;
  autocomplete?: string;
  onInputChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onMenuSelection: (option: SearchableSelectionOptionType) => void;
  error?: string;
}

export default function SearchableSelection({
  label,
  value,
  options,
  autocomplete,
  onInputChange,
  onMenuSelection,
  error,
}: FilledTextFieldProps) {
  const [focused, setFocused] = useState<boolean>(false);
  const [menuOpen, setMenuOpen] = useState<boolean>(false);

  function handleFocused() {
    setFocused(true);
    setMenuOpen(true);
  }

  function handleBlurred(event: React.FocusEvent<HTMLInputElement>) {
    if (event.target.value.length < 1) {
      setFocused(false);
    }
    setMenuOpen(false);
  }

  return (
    <div className="relative mb-7 flex max-h-[54px] w-full flex-col justify-center bg-transparent">
      <fieldset>
        <legend
          className={`absolute text-[#868b8b] ${
            focused || value ? 'z-40 animate-input_label text-xs' : 'left-[18px] top-[15.5px]'
          }`}
        >
          {label}
        </legend>
        <input
          type="text"
          value={value}
          onChange={onInputChange}
          autoComplete={autocomplete}
          onFocus={handleFocused}
          onBlur={handleBlurred}
          className="relative z-30 h-full w-full rounded bg-[var(--palette-black-20)] p-[18px] pl-4 shadow-[0_1px_0_var(--palette-light-burgundy)] focus:shadow-[0_1px_0_var(--text-light)] focus:outline-none"
        />
        <div
          className={`${
            menuOpen ? 'block' : 'hidden'
          } absolute top-[60px] z-50 w-full rounded bg-[var(--palette-baltic-sea)]`}
        >
          <ul className="max-h-[300px] overflow-y-auto">
            {options?.map((option, index) => (
              <li
                key={option.itemId}
                className={`${
                  index % 2 == 0
                    ? 'bg-[var(--palette-dark-grey)]'
                    : 'bg-[var(--palette-darkest-grey)]'
                } ${index === 0 ? 'rounded-t-lg' : ''} ${
                  index === options.length - 1 ? 'rounded-b-lg' : ''
                } p-2`}
              >
                <button
                  type="button"
                  className="w-full text-left"
                  onMouseDown={() => {
                    setMenuOpen(false);
                    onMenuSelection(option);
                  }}
                >
                  {option.label}
                </button>
              </li>
            ))}
          </ul>
        </div>
        <span className="absolute bottom-[-25px] left-[5px] text-[var(--error)]">
          {error && error.length > 0 ? error : ''}
        </span>
      </fieldset>
    </div>
  );
}
