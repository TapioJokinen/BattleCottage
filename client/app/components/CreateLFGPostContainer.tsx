'use client';

import { gamesFetchByName } from '@/app/api/games';
import {
  DurationOptionType,
  GameModeOptionType,
  GameOptionType,
  GameStyleOptionType,
  SearchableSelectionOptionType,
} from '@/app/lib/types/components';
import FilledTextArea from '@/app/components/inputs/FilledTextArea';
import FilledTextField from '@/app/components/inputs/FilledTextField';
import SearchableSelection from '@/app/components/inputs/SearchableSelection';
import { useSession } from 'next-auth/react';
import { Dispatch, SetStateAction, useState } from 'react';
import {
  searchableSelectionDefaultOption,
  durationOptions,
  gameModeOptions,
  gameStyleOptions,
} from '@/app/lib/utils/options';
import { roles } from '@/app/lib/utils/roles';
import RoleButton from '@/app/components/buttons/RoleButton';
import SquaredButton from '@/app/components/buttons/SquaredButton';
import { LFGPost } from '../lib/types/api';

export default function CreateLFGPostContainer() {
  const session = useSession();

  /* SearchableSelection options */
  const [gameOptions, setGameOptions] = useState<GameOptionType[]>([]);
  const [durationChoices, setDurationChoices] = useState<DurationOptionType[]>(durationOptions);
  const [gameModeChoices, setGameModeChoices] = useState<GameModeOptionType[]>(gameModeOptions);
  const [gameStyleChoices, setGameStyleChoices] = useState<GameStyleOptionType[]>(gameStyleOptions);

  /* Form values */
  const [game, setGame] = useState<GameOptionType>(searchableSelectionDefaultOption);
  const [title, setTitle] = useState<string>('');
  const [titleError, setTitleError] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [descriptionError, setDescriptionError] = useState<string>('');
  const [duration, setDuration] = useState<DurationOptionType>(searchableSelectionDefaultOption);
  const [gameMode, setGameMode] = useState<GameModeOptionType>(searchableSelectionDefaultOption);
  const [gameStyle, setGameStyle] = useState<GameStyleOptionType>(searchableSelectionDefaultOption);
  const [playerRoles, setPlayerRoles] = useState<number[]>([]);

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
   * Handles input change for searchable selection component.
   * @template T - The type of the searchable selection option.
   * @param {React.ChangeEvent<HTMLInputElement>} event - The input change event.
   * @param {React.Dispatch<React.SetStateAction<T>>} setOption - The state setter for the selected option.
   * @param {React.Dispatch<React.SetStateAction<T[]>>} setOptions - The state setter for the available options.
   * @param {T[]} defaultOptions - The default options for the searchable selection.
   */
  function handleInputChange<T extends SearchableSelectionOptionType>(
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

  /**
   * Handles the addition of a role to the LFG form.
   * @param event - The mouse event that triggered the role addition.
   */
  function handleRoleAdd(event: React.MouseEvent<HTMLDivElement>) {
    const target = event.target as HTMLDivElement;
    const roleId = target.id;
    if (roleId !== null) {
      setPlayerRoles((prev) => {
        if (prev.length < 101) {
          return [...prev, parseInt(roleId)];
        }
        return [...prev];
      });
    }
  }

  /**
   * Handles the removal of a role from the LFG form.
   * @param event - The mouse event that triggered the role removal.
   */
  function handleRoleRemove(event: React.MouseEvent<HTMLDivElement>) {
    const target = event.target as HTMLDivElement;
    const roleId = target.id;
    if (roleId !== null) {
      setPlayerRoles((prev) => {
        const index = prev.indexOf(parseInt(roleId));
        if (index > -1) {
          const newValues = [...prev];
          newValues.splice(index, 1);
          return newValues;
        }
        return prev;
      });
    }
  }

  function handleFormSubmit() {
    const post: LFGPost = {
      title: title,
      description: description,
      gameId: game.itemId,
      gameModeId: gameMode.itemId,
      gameStyleId: gameStyle.itemId,
      durationId: duration.itemId,
      gameRoleIds: playerRoles,
    };
  }

  return (
    <div className="flex h-full flex-col items-center">
      <div className="h-fit w-3/4 max-w-[1440px] border-2 border-[var(--palette-baltic-sea)] bg-[var(--palette-dark-jungle-green)] p-5 shadow-md">
        <div className="grid h-fit w-full grid-cols-1 place-items-start justify-items-center sm:grid-cols-2 sm:gap-4">
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
        <div>
          <div className="inline-block w-full text-center">
            {roles.map((role) => (
              <RoleButton
                key={role.id}
                roleId={role.id}
                label={role.label}
                Icon={role.icon}
                numOfSelected={playerRoles.filter((r) => r === role.id).length}
                handleAdd={handleRoleAdd}
                handleRemove={handleRoleRemove}
              />
            ))}
          </div>
        </div>
        <div className="flex w-full justify-end">
          <SquaredButton
            type="button"
            text="Create Post"
            isActive={true}
            loading={false}
            onClick={() => {}}
          />
        </div>
      </div>
    </div>
  );
}
