import { ChangeEvent, useEffect, useState } from 'react';

export default function FormTextareaInput({
  handleInputChange,
  placeholder,
}: {
  handleInputChange: Function;
  placeholder: string;
}) {
  const [value, setValue] = useState<string>('');

  function handleInternalInputChange(event: ChangeEvent<HTMLTextAreaElement>) {
    setValue(event.currentTarget.value);
  }

  useEffect(() => {
    handleInputChange(value);
  }, [value]);

  return (
    <div className="flex min-w-[300px]">
      <textarea
        className="form-textarea-input"
        value={value}
        onChange={handleInternalInputChange}
        placeholder={placeholder}
      ></textarea>
    </div>
  );
}
