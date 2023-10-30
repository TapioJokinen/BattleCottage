'use client';

import { SearchableSelectionOptionType } from '@/app/lib/types/components';
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
    <div className="searchable-selection-main">
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
          className="searchable-selection-input"
        />
        <div className={`${menuOpen ? 'block' : 'hidden'} searchable-selection-menu`}>
          <ul className="searchable-selection-menu-list">
            {options?.map((option, index) => (
              <li
                key={option.itemId}
                className={`${
                  index % 2 == 0
                    ? 'bg-[var(--palette-dark-grey)]'
                    : 'bg-[var(--palette-darkest-grey)]'
                } ${index === 0 ? 'rounded-t-lg' : ''} ${
                  index === options.length - 1 ? 'rounded-b-lg' : ''
                } p-2 hover:bg-[var(--palette-water-blue)]`}
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
        <span className="searchable-selection-error ">
          {error && error.length > 0 ? error : ''}
        </span>
      </fieldset>
    </div>
  );
}
