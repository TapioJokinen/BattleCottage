'use client';

import FilledTextArea from '@/components/inputs/FilledTextArea';
import FilledTextField from '@/components/inputs/FilledTextField';
import SearchableSelection from '@/components/inputs/SearchableSelection';
import { useState } from 'react';

export default function LFGCreate() {
  const [title, setTitle] = useState<string>('');
  const [titleError, setTitleError] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [descriptionError, setDescriptionError] = useState<string>('');

  function handleTitleChange(event: React.ChangeEvent<HTMLInputElement>) {
    const value = event.target.value;
    setTitle(value);

    if (value.length > 50) {
      setTitleError('Title is too long.');
    } else {
      setTitleError('');
    }
  }

  function handleDescriptionChange(event: React.ChangeEvent<HTMLTextAreaElement>) {
    const value = event.target.value;
    setDescription(value);

    if (value.length > 250) {
      setDescriptionError('Description is too long.');
    } else {
      setDescriptionError('');
    }
  }

  return (
    <main className="main bg-gradient">
      <div className="mt-5 flex h-full flex-col items-center">
        <div className="h-[400px] w-3/4 border-2 border-[var(--palette-baltic-sea)] bg-[var(--palette-dark-jungle-green)] pl-5 pt-5 shadow-md">
          <div className="grid h-full w-full grid-cols-3 gap-4">
            <div>
              <FilledTextField
                type="text"
                placeholder="Title"
                error={titleError}
                value={title}
                onChange={handleTitleChange}
              />
              <FilledTextArea
                placeholder="Description"
                error={descriptionError}
                value={description}
                onChange={handleDescriptionChange}
              />
            </div>
            <div>
              <SearchableSelection
                placeholder="Post duration"
                options={[
                  { text: '1', value: 1 },
                  { text: '2', value: 2 },
                  { text: '3', value: 3 },
                  { text: '4', value: 3 },
                  { text: '5', value: 3 },
                  { text: '6', value: 3 },
                  { text: '7', value: 3 },
                ]}
                handleInputChange={() => {}}
                handleSelectedValue={() => {}}
              />
            </div>
            <div>
              <span>3</span>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
