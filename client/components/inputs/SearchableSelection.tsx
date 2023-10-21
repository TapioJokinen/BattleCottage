'use client';

import { SearchableSelectionOptionType } from '@/app/types';
import { useEffect, useState } from 'react';
import Select, { OnChangeValue } from 'react-select';

type SelectOptionType = { label: string; value: number };

const customStyles = {
  placeholder: (defaultStyles: any, state: any) => {
    return {
      ...defaultStyles,
      color: '#9ba1aa',
    };
  },
  option: (defaultStyles: any, state: any) => ({
    ...defaultStyles,
    color: state.isSelected ? 'var(--text-light)' : '#fff',
    backgroundColor: state.isFocused || state.isSelected ? 'var(--palette-water-blue)' : '#212529',
  }),

  control: (defaultStyles: any, state: any) => ({
    ...defaultStyles,
    backgroundColor: 'var(--palette-black-20)',
    padding: '0.532rem',
    width: '100%',
    border: 'none',
    borderBottom: state.isFocused
      ? '1px solid var(--text-light)'
      : '1px solid var(--palette-light-burgundy)',
    boxShadow: state.isFocused ? '0px 0px 1px var(--palette-light-burgundy)' : 'none',
    '&:hover': {
      border: 'none',
      borderBottom: '1px solid var(--text-light)',
      boxShadow: '0px 0px 1px var(--text-light)',
    },
  }),

  singleValue: (defaultStyles: any) => ({ ...defaultStyles, color: 'var(--base-light)' }),

  input: (defaultStyles: any) => ({
    ...defaultStyles,
    color: 'var(--text-light)',
    width: '100%',
  }),

  container: (defaultStyles: any) => ({
    ...defaultStyles,
    width: '100%',
  }),

  menuList: (defaultStyles: any) => ({
    ...defaultStyles,
    backgroundColor: 'var(--palette-black-20)',
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
    <div className="my-3 flex w-full max-w-[368px] flex-col">
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
