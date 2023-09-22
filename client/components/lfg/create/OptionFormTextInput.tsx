import { ChangeEvent, useEffect, useState } from 'react';

export default function OptionFormTextInput({
  handleInputChange,
  placeholder,
}: {
  handleInputChange: Function;
  placeholder: string;
}) {
  const [value, setValue] = useState<string>('');

  function handleInternalInputChange(event: ChangeEvent<HTMLInputElement>) {
    setValue(event.currentTarget.value);
  }

  useEffect(() => {
    handleInputChange(value);
  }, [value]);

  return (
    <div className="w-full min-w-[300px]">
      <input
        className="option-form-text-input"
        type="text"
        value={value}
        onChange={handleInternalInputChange}
        placeholder={placeholder}
      />
    </div>
  );
}
