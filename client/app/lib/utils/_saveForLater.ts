import { SearchSelectInputOptionType } from '../types/components';

/**
 * Handles input change for searchable selection component.
 * @template T - The type of the searchable selection option.
 * @param {React.ChangeEvent<HTMLInputElement>} event - The input change event.
 * @param {React.Dispatch<React.SetStateAction<T>>} setOption - The state setter for the selected option.
 * @param {React.Dispatch<React.SetStateAction<T[]>>} setOptions - The state setter for the available options.
 * @param {T[]} defaultOptions - The default options for the searchable selection.
 */
function handleInputChange<T extends SearchSelectInputOptionType>(
  event: React.ChangeEvent<HTMLInputElement>,
  setOption: React.Dispatch<React.SetStateAction<T>>,
  setOptions: React.Dispatch<React.SetStateAction<T[]>>,
  defaultOptions: T[],
) {
  const value = event.target.value;
  setOption({ label: value, itemId: -1 } as T);

  const options = defaultOptions.filter((option) => option.label.includes(value));

  if (value.length < 1) {
    setOptions(defaultOptions);
  } else {
    setOptions(options);
  }
}
