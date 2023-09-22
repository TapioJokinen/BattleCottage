'use client';

import SearchableSelection from '@/components/selections/SearchableSelection';
import { useState } from 'react';
import Option from './Option';
import { gamesFetchByName } from '@/app/api/games';
import { SearchableSelectionOptionType } from '@/app/types';
import { useSession } from 'next-auth/react';
import OptionFormTextInput from './OptionFormTextInput';
import OptionFormTextareaInput from './OptionFormTextareaInput';

interface OptionFormType {
  gameId: number | null;
  title: string | null;
  description: string | null;
  duration: number | null;
  mode: number | null;
  style: number | null;
}

const durations: SearchableSelectionOptionType[] = [
  { text: 'One Hour', value: 60 },
  { text: 'Five Hours', value: 60 * 5 },
  { text: 'One Day', value: 60 * 24 },
  { text: 'One Week', value: 60 * 24 * 7 },
  { text: 'Thirty Days', value: 60 * 24 * 30 },
];

const gameFormats: SearchableSelectionOptionType[] = [
  { text: 'PvP', value: 1 },
  { text: 'PvE', value: 2 },
];

const gameStyles: SearchableSelectionOptionType[] = [
  { text: 'Competetive', value: 1 },
  { text: 'Casual', value: 2 },
];

export default function OptionForm() {
  const session = useSession();

  const [optionForm, setOptionForm] = useState<OptionFormType>({
    gameId: null,
    title: null,
    description: null,
    duration: null,
    mode: null,
    style: null,
  });
  const [gameOptions, setGameOptions] = useState<SearchableSelectionOptionType[] | null>(null);
  const [durationOptions] = useState<SearchableSelectionOptionType[]>(durations);
  const [gameFormatOptions] = useState<SearchableSelectionOptionType[]>(gameFormats);
  const [gameStyleOptions] = useState<SearchableSelectionOptionType[]>(gameStyles);

  console.log(optionForm);

  async function handleGameOptionInputChange(input: string) {
    const data = await gamesFetchByName(input, 1, 100, session.data?.accessToken);

    if (data.responseOk) {
      setGameOptions(data.results.map((r) => ({ value: r.id, text: r.name })));
    }
  }

  function handleGameSelect(gameId: number) {
    setOptionForm((prev) => ({ ...prev, gameId }));
  }

  function handleTitleInput(title: string) {
    setOptionForm((prev) => ({ ...prev, title }));
  }

  function handleDescriptionInput(description: string) {
    setOptionForm((prev) => ({ ...prev, description }));
  }

  function handleDurationSelect(duration: number) {
    setOptionForm((prev) => ({ ...prev, duration }));
  }

  function handleGameModeSelect(mode: number) {
    setOptionForm((prev) => ({ ...prev, mode }));
  }

  function handleGameFormatSelect(style: number) {
    setOptionForm((prev) => ({ ...prev, style }));
  }

  return (
    <div className="mb-5 block w-full grid-cols-2 gap-4 lg:grid lg:px-10 lg:py-5 3xl:px-20 3xl:py-10">
      <div className="flex w-full flex-col items-center px-5">
        <Option text="Choose a game">
          <SearchableSelection
            placeholder="Type to search game"
            options={gameOptions}
            handleInputChange={handleGameOptionInputChange}
            handleSelectedValue={handleGameSelect}
          />
        </Option>
        <Option text="Give your post a title">
          <OptionFormTextInput
            handleInputChange={handleTitleInput}
            placeholder="Type your title here"
          />
        </Option>
        <Option text="Add a description">
          <OptionFormTextareaInput
            handleInputChange={handleDescriptionInput}
            placeholder="Type your description here"
          />
        </Option>
      </div>
      <div className="flex w-full flex-col items-center px-5">
        <Option text="Select post duration">
          <SearchableSelection
            placeholder="Select duration"
            options={durationOptions}
            handleInputChange={() => {}}
            handleSelectedValue={handleDurationSelect}
          />
        </Option>
        <Option text="Choose game mode">
          <SearchableSelection
            placeholder="PvP/PvE"
            options={gameFormatOptions}
            handleInputChange={() => {}}
            handleSelectedValue={handleGameModeSelect}
          />
        </Option>
        <Option text="Choose gaming style">
          <SearchableSelection
            placeholder="Competetive/Casual"
            options={gameStyleOptions}
            handleInputChange={() => {}}
            handleSelectedValue={handleGameFormatSelect}
          />
        </Option>
      </div>
    </div>
  );
}
