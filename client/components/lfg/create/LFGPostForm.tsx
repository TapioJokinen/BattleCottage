'use client';

import SearchableSelection from '@/components/selections/SearchableSelection';
import { useState } from 'react';
import FormControl from '../../forms/advanced/FormControl';
import { gamesFetchByName } from '@/app/api/games';
import { SearchableSelectionOptionType } from '@/app/types';
import { useSession } from 'next-auth/react';
import FormTextInput from '@/components/forms/advanced/FormTextInput';
import FormTextareaInput from '@/components/forms/advanced/FormTextareaInput';
import Role from './Role';
import TankIcon from '@/components/icons/TankIcon';
import HealerIcon from '@/components/icons/HealerIcon';
import SwordIcon from '@/components/icons/SwordIcon';
import TopLaneIcon from '@/components/icons/TopLaneIcon';
import BottomLaneIcon from '@/components/icons/BottomLaneIcon';
import JungleIcon from '@/components/icons/JungleIcon';
import MidLaneIcon from '@/components/icons/MidLaneIcon';
import SupportIcon from '@/components/icons/SupportIcon';
import EntryFraggerIcon from '@/components/icons/EntryFraggerIcon';
import RefraggerIcon from '@/components/icons/RefraggerIcon';
import StrategyCallerIcon from '@/components/icons/StrategyCallerIcon';
import LurkerIcon from '@/components/icons/LurkerIcon';
import AWPerIcon from '@/components/icons/AWPerIcon';
import CombatSupportIcon from '@/components/icons/CombatSupportIcon';
import MedicIcon from '@/components/icons/MedicIcon';
import AssaultIcon from '@/components/icons/AssaultIcon';

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

export default function LFGPostForm() {
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
    <div className="w-full">
      <div className="mb-5 block w-full grid-cols-2 gap-4 lg:grid lg:px-10 lg:py-5 3xl:px-20 3xl:py-10">
        <div className="flex w-full flex-col items-center px-5">
          <FormControl text="Choose a game" ok={!!optionForm.gameId}>
            <SearchableSelection
              placeholder="Type to search game"
              options={gameOptions}
              handleInputChange={handleGameOptionInputChange}
              handleSelectedValue={handleGameSelect}
            />{' '}
          </FormControl>
          <FormControl
            text="Give your post a title"
            ok={!!optionForm.title && optionForm.title.length > 2 && optionForm.title.length < 100}
            error={optionForm.title && optionForm.title.length > 100 ? 'Title is too long' : ''}
          >
            <FormTextInput
              handleInputChange={handleTitleInput}
              placeholder="Type your title here"
            />
          </FormControl>
          <FormControl
            text="Add a description"
            ok={!!optionForm.description && optionForm.description.length > 2}
          >
            <FormTextareaInput
              handleInputChange={handleDescriptionInput}
              placeholder="Type your description here"
            />
          </FormControl>
        </div>
        <div className="flex w-full flex-col items-center px-5">
          <FormControl text="Select post duration" ok={!!optionForm.duration}>
            <SearchableSelection
              placeholder="Select duration"
              options={durations}
              handleInputChange={() => {}}
              handleSelectedValue={handleDurationSelect}
            />
          </FormControl>
          <FormControl text="Choose game mode" ok={!!optionForm.mode}>
            <SearchableSelection
              placeholder="PvP/PvE"
              options={gameFormats}
              handleInputChange={() => {}}
              handleSelectedValue={handleGameModeSelect}
            />
          </FormControl>
          <FormControl text="Choose gaming style" ok={!!optionForm.style}>
            <SearchableSelection
              placeholder="Competetive/Casual"
              options={gameStyles}
              handleInputChange={() => {}}
              handleSelectedValue={handleGameFormatSelect}
            />
          </FormControl>
        </div>
      </div>
      <div className="flex w-full flex-col items-center">
        <hr className="w-[95%] rounded border-[var(--palette-smoky-topaz)]" />
        <div className="mt-3 flex w-full flex-col">
          <div className="flex h-full w-full items-center justify-center">
            <div>
              <Role role="Tank" icon={<TankIcon />} />
              <Role role="Healer" icon={<HealerIcon />} />
              <Role role="DPS" icon={<SwordIcon />} />
            </div>
            <div>
              <Role role="Top Lane" icon={<TopLaneIcon />} />
              <Role role="Bot Lane" icon={<BottomLaneIcon />} />
              <Role role="Mid Lane" icon={<MidLaneIcon />} />
              <Role role="Jungle" icon={<JungleIcon />} />
              <Role role="Support" icon={<SupportIcon />} />
            </div>
            <div>
              <Role role="Entry Fragger" icon={<EntryFraggerIcon />} />
              <Role role="Refragger" icon={<RefraggerIcon />} />
              <Role role="Strategy Caller" icon={<StrategyCallerIcon />} />
              <Role role="Lurker" icon={<LurkerIcon />} />
              <Role role="AWPer" icon={<AWPerIcon />} />
            </div>
            <div>
              <Role role="Assault" icon={<AssaultIcon />} />
              <Role role="Medic" icon={<MedicIcon />} />
              <Role role="Combat Support" icon={<CombatSupportIcon />} />
              <Role role="Recon" icon={<AWPerIcon />} />
            </div>
            <div>
              <Role role="Friendly" icon={<TankIcon />} />
              <Role role="Funny" icon={<TankIcon />} />
              <Role role="Serious" icon={<TankIcon />} />
              <Role role="e-Girl" icon={<TankIcon />} />
              <Role role="Smurfer" icon={<TankIcon />} />
              <Role role="Carry" icon={<TankIcon />} />
              <Role role="Bait" icon={<TankIcon />} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
