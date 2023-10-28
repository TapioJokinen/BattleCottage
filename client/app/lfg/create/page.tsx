'use client';

import { gamesFetchByName } from '@/app/api/games';
import {
  DurationOptionType,
  GameModeOptionType,
  GameOptionType,
  GameStyleOptionType,
  SearchableSelectionOptionType,
} from '@/lib/types/components';
import FilledTextArea from '@/components/inputs/FilledTextArea';
import FilledTextField from '@/components/inputs/FilledTextField';
import SearchableSelection from '@/components/inputs/SearchableSelection';
import { useSession } from 'next-auth/react';
import { Dispatch, SetStateAction, useState } from 'react';
import {
  searchableSelectionDefaultOption,
  durationOptions,
  gameModeOptions,
  gameStyleOptions,
} from '@/lib/utils/options';

export default function LFGCreate() {
  const session = useSession();

  /*SearchableSelection options */
  const [gameOptions, setGameOptions] = useState<GameOptionType[]>([]);
  const [durationChoices, setDurationChoices] = useState<DurationOptionType[]>(durationOptions);
  const [gameModeChoices, setGameModeChoices] = useState<GameModeOptionType[]>(gameModeOptions);
  const [gameStyleChoices, setGameStyleChoices] = useState<GameStyleOptionType[]>(gameStyleOptions);

  /*Form values */
  const [game, setGame] = useState<GameOptionType>(searchableSelectionDefaultOption);
  const [title, setTitle] = useState<string>('');
  const [titleError, setTitleError] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [descriptionError, setDescriptionError] = useState<string>('');
  const [duration, setDuration] = useState<DurationOptionType>(searchableSelectionDefaultOption);
  const [gameMode, setGameMode] = useState<GameModeOptionType>(searchableSelectionDefaultOption);
  const [gameStyle, setGameStyle] = useState<GameStyleOptionType>(searchableSelectionDefaultOption);

  /**
   * Handles the selection of a menu option.
   * @template T - The type of the selected option.
   * @param {T} option - The selected option.
   * @param {Dispatch<SetStateAction<T>>} setOption - The callback function to update the state with the selected option.
   */
  function handleMenuSelection<T extends SearchableSelectionOptionType>(
    option: T,
    setOption: Dispatch<SetStateAction<T>>,
  ) {
    setOption(option);
  }

  /**
   * Handles the input change event for the game input field.
   * Sets the game state to the input value and fetches game options from the server.
   * @param event - The input change event.
   */
  async function handleGameInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    const value = event.target.value;
    setGame({ label: value, itemId: -1 });

    const data = await gamesFetchByName(value, 1, 100, session.data?.accessToken);

    if (data.responseOk) {
      const options = data.results.map((game) => ({ label: game.name, itemId: game.id }));
      setGameOptions(options);

      const option = options.find((option) => option.label === value);

      if (option) {
        setGame(option);
      }
    }
  }

  /**
   * Handles the change event for the title input field.
   * @param event - The change event object.
   */
  function handleTitleChange(event: React.ChangeEvent<HTMLInputElement>) {
    const value = event.target.value;
    setTitle(value);

    if (value.length > 50) {
      setTitleError('Title is too long.');
    } else {
      setTitleError('');
    }
  }

  /**
   * Handles the change event for the description input field.
   * @param event - The change event object.
   */
  function handleDescriptionChange(event: React.ChangeEvent<HTMLTextAreaElement>) {
    const value = event.target.value;
    setDescription(value);

    if (value.length > 250) {
      setDescriptionError('Description is too long.');
    } else {
      setDescriptionError('');
    }
  }

  /**
   * Handles the change event for the duration input field.
   * @param event - The change event object.
   */
  function handleInputChange<T extends SearchableSelectionOptionType>(
    event: React.ChangeEvent<HTMLInputElement>,
    setOption: Dispatch<SetStateAction<T>>,
    setOptions: Dispatch<SetStateAction<T[]>>,
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

  return (
    <main className="main bg-gradient">
      <div className="flex w-full justify-center p-5 font-permanentmarker text-[2em]">
        <span className="curved-underline gradient-text">Create Your own LFG post!</span>
      </div>
      <div className="flex h-full flex-col items-center">
        <div className="h-fit w-3/4 border-2 border-[var(--palette-baltic-sea)] bg-[var(--palette-dark-jungle-green)] p-5 shadow-md">
          <div className="grid h-full w-full grid-cols-2 place-items-start justify-items-center gap-4">
            <div className="flex w-full flex-col items-center">
              <SearchableSelection
                label="Choose a game"
                value={game?.label}
                options={gameOptions}
                onInputChange={handleGameInputChange}
                onMenuSelection={(option: GameOptionType) => handleMenuSelection(option, setGame)}
              />
              <FilledTextField
                type="text"
                label="Give your post a title"
                error={titleError}
                value={title}
                onChange={handleTitleChange}
              />
              <FilledTextArea
                label="Give your post a description"
                error={descriptionError}
                value={description}
                onChange={handleDescriptionChange}
              />
            </div>
            <div className="flex w-full flex-col items-center">
              <SearchableSelection
                label="Choose a game mode"
                value={gameMode?.label}
                options={gameModeChoices}
                onInputChange={(event: React.ChangeEvent<HTMLInputElement>) =>
                  handleInputChange(event, setGameMode, setGameModeChoices, gameModeOptions)
                }
                onMenuSelection={(option: GameModeOptionType) =>
                  handleMenuSelection(option, setGameMode)
                }
              />
              <SearchableSelection
                label="Choose a gaming style"
                value={gameStyle?.label}
                options={gameStyleChoices}
                onInputChange={(event: React.ChangeEvent<HTMLInputElement>) =>
                  handleInputChange(event, setGameStyle, setGameStyleChoices, durationOptions)
                }
                onMenuSelection={(option: GameStyleOptionType) =>
                  handleMenuSelection(option, setGameStyle)
                }
              />
              <SearchableSelection
                label="Choose a duration for post"
                value={duration?.label}
                options={durationChoices}
                onInputChange={(event: React.ChangeEvent<HTMLInputElement>) =>
                  handleInputChange(event, setDuration, setDurationChoices, durationOptions)
                }
                onMenuSelection={(option: DurationOptionType) =>
                  handleMenuSelection(option, setDuration)
                }
              />
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
