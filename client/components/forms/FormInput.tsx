import { ChangeEventHandler, HTMLInputTypeAttribute } from 'react';

export default function FormInput({
  inputId,
  inputValue,
  inputOnChange,
  inputType,
  label,
}: {
  inputId: string;
  inputValue: string;
  inputOnChange: ChangeEventHandler<HTMLInputElement>;
  inputType: HTMLInputTypeAttribute;
  label: string;
}) {
  return (
    <div className="form-input-wrapper">
      <label htmlFor={inputId} className="pb-2">
        {label}
      </label>
      <input
        id={inputId}
        value={inputValue}
        onChange={inputOnChange}
        autoComplete="off"
        className="form-input"
        type={inputType}
      />
    </div>
  );
}
