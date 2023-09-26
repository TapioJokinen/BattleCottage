'use client';

import { SearchableSelectionOptionType } from '@/app/types';
import { useEffect, useState } from 'react';
import Select, { OnChangeValue } from 'react-select';

type SelectOptionType = { label: string; value: number };

const customStyles = {
  option: (defaultStyles: any, state: any) => ({
    ...defaultStyles,
    color: state.isSelected ? 'var(--base-dark)' : '#fff',
    backgroundColor: state.isFocused || state.isSelected ? 'var(--palette-smoky-topaz)' : '#212529',
  }),

  control: (defaultStyles: any, state: any) => ({
    ...defaultStyles,
    backgroundColor: 'var(--base-dark)',
    padding: '2px',
    width: '100%',
    border: state.isFocused ? '1px solid var(--palette-smoky-topaz)' : '1px solid #cccccc',
    boxShadow: state.isFocused ? '0px 0px 1px var(--palette-smoky-topaz)' : 'none',
    '&:hover': {
      border: '1px solid var(--palette-smoky-topaz)',
      boxShadow: '0px 0px 1px var(--palette-smoky-topaz)',
    },
  }),

  singleValue: (defaultStyles: any) => ({ ...defaultStyles, color: 'var(--base-light)' }),

  input: (defaultStyles: any) => ({
    ...defaultStyles,
    color: 'var(--base-light)',
    width: '100%',
  }),

  container: (defaultStyles: any) => ({
    ...defaultStyles,
    width: '100%',
  }),

  menuList: (defaultStyles: any) => ({
    ...defaultStyles,
    backgroundColor: 'var(--base-dark)',
  }),
};

export default function SearchableSelection({
  placeholder,
  options,
  handleInputChange,
  handleSelectedValue,
}: {
  placeholder: string;
  options: SearchableSelectionOptionType[] | null;
  handleInputChange: Function;
  handleSelectedValue: Function;
}) {
  const [selectedOption, setSelectedOption] = useState<SelectOptionType | null>(null);

  function handleSelectInputChange(input: string) {
    handleInputChange(input);
  }

  function getOptions(): SelectOptionType[] {
    if (!options || !options.length) {
      return [];
    }
    return options.map((o) => ({ label: o.text, value: o.value }));
  }

  useEffect(() => {
    if (selectedOption) {
      handleSelectedValue(selectedOption.value);
    }
  }, [selectedOption]);

  return (
    <div className="flex w-full flex-col">
      <Select
        instanceId={1}
        options={getOptions()}
        value={selectedOption}
        onChange={(selectedOption: OnChangeValue<SelectOptionType, false>) => {
          if (selectedOption?.value) {
            setSelectedOption(selectedOption);
          }
        }}
        onInputChange={(newValue: string) => {
          handleSelectInputChange(newValue);
        }}
        placeholder={placeholder}
        styles={customStyles}
        classNames={{}}
      />
    </div>
  );
}
