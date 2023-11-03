'use client';

import { gamesFetchByName } from '@/app/api/games';
import {
  DurationOptionType,
  GameModeOptionType,
  GameOptionType,
  GameStyleOptionType,
  LFGPostFormOptionsType,
  SearchSelectInputOptionType,
} from '@/app/lib/types/components';
import FilledTextArea from '@/app/components/inputs/FilledTextArea';
import FilledTextField from '@/app/components/inputs/FilledTextField';
import SearchSelectInput, { SSIDefaultOption } from '@/app/components/inputs/SearchSelectInput';
import { useSession } from 'next-auth/react';
import { Dispatch, SetStateAction, useEffect, useState } from 'react';

import { roles } from '@/app/lib/utils/roles';
import RoleButton from '@/app/components/buttons/RoleButton';
import SquaredButton from '@/app/components/buttons/SquaredButton';
import { LFGPost } from '@/app/lib/types/components';
import { lfgPostCreate, lfgPostGetFormOptions } from '@/app/api/lfgpost';
import useAlert from '@/app/hooks/useAlert';
import { useRouter } from 'next/navigation';

const MAX_ROLE_SELECTIONS = 100;

export default function CreateLFGPostContainer() {
  const session = useSession();
  const router = useRouter();
  const alert = useAlert();

  /* SearchSelectInput default options */
  const [gameModeOptions, setGameModeOptions] = useState<GameModeOptionType[]>([]);
  const [gameStyleOptions, setGameStyleOptions] = useState<GameStyleOptionType[]>([]);
  const [durationOptions, setDurationOptions] = useState<DurationOptionType[]>([]);

  /* SearchSelectInput filtered options */
  const [gameOptions, setGameOptions] = useState<GameOptionType[]>([]);
  const [durationChoices, setDurationChoices] = useState<DurationOptionType[]>([]);
  const [gameModeChoices, setGameModeChoices] = useState<GameModeOptionType[]>([]);
  const [gameStyleChoices, setGameStyleChoices] = useState<GameStyleOptionType[]>([]);

  /* Form values */
  const [game, setGame] = useState<GameOptionType>(SSIDefaultOption);
  const [title, setTitle] = useState<string>('');
  const [titleError, setTitleError] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [descriptionError, setDescriptionError] = useState<string>('');
  const [duration, setDuration] = useState<DurationOptionType>(SSIDefaultOption);
  const [gameMode, setGameMode] = useState<GameModeOptionType>(SSIDefaultOption);
  const [gameStyle, setGameStyle] = useState<GameStyleOptionType>(SSIDefaultOption);
  const [playerRoles, setPlayerRoles] = useState<number[]>([]);

  /* Others */
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    async function fetchFormOptions() {
      if (session.data?.accessToken) {
        setLoading(true);
        const data = await lfgPostGetFormOptions(session.data?.accessToken);

        if (data.responseOk) {
          setGameModeOptions(data.gameModes.map((o) => ({ label: o.name, itemId: o.id })));
          setGameStyleOptions(data.gameStyles.map((o) => ({ label: o.name, itemId: o.id })));
          setDurationOptions(data.durationsInMinutes.map((o) => ({ label: o.name, itemId: o.id })));
        } else {
          alert.raiseAlert(data.message, 'error');
        }
        setLoading(false);
      }
    }

    fetchFormOptions();
  }, [session.data?.accessToken]);

  /**
   * Handles the selection of a menu option.
   * @template T - The type of the selected option.
   * @param {T} option - The selected option.
   * @param {Dispatch<SetStateAction<T>>} setOption - The callback function to update the state with the selected option.
   */
  function handleMenuSelection<T extends SearchSelectInputOptionType>(
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
   * Handles the addition of a role to the LFG form.
   * @param event - The mouse event that triggered the role addition.
   */
  function handleRoleAdd(event: React.MouseEvent<HTMLDivElement>) {
    const target = event.target as HTMLDivElement;
    const roleId = target.id;
    if (roleId !== null) {
      setPlayerRoles((prev) => {
        if (prev.length < MAX_ROLE_SELECTIONS) {
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

  async function handleFormSubmit() {
    setLoading(true);

    if (
      title.length < 3 ||
      description.length < 3 ||
      playerRoles.length === 0 ||
      !game.itemId ||
      !gameMode.itemId ||
      !gameStyle.itemId ||
      !duration.itemId
    ) {
      alert.raiseAlert('Please fill in all fields.', 'error');
    } else {
      const post: LFGPost = {
        title,
        description,
        gameId: game.itemId,
        gameModeId: gameMode.itemId,
        gameStyleId: gameStyle.itemId,
        durationId: duration.itemId,
        gameRoleIds: playerRoles,
      };

      const data = await lfgPostCreate(post, session.data?.accessToken);

      if (data.responseOk) {
        alert.raiseAlert('LFG post created!', 'success');
        router.push('/cottage');
      } else {
        alert.raiseAlert(data.message, 'error');
      }
    }

    setLoading(false);
  }

  return (
    <div className="flex h-full flex-col items-center">
      <div className="h-fit w-11/12 max-w-[1440px] border-2 border-[var(--palette-baltic-sea)] bg-[var(--palette-dark-jungle-green)] p-5 shadow-md sm:w-3/4">
        <div className="grid h-fit w-full grid-cols-1 place-items-start justify-items-center sm:grid-cols-2 sm:gap-4">
          <div className="flex w-full flex-col items-center">
            <SearchSelectInput
              label={game?.label ? 'Choose a game' : 'Choose a game (type to search)'}
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
            <SearchSelectInput
              label="Choose a game mode"
              value={gameMode?.label}
              options={gameModeOptions}
              onInputChange={() => {}}
              onMenuSelection={(option: GameModeOptionType) =>
                handleMenuSelection(option, setGameMode)
              }
            />
            <SearchSelectInput
              label="Choose a gaming style"
              value={gameStyle?.label}
              options={gameStyleOptions}
              onInputChange={() => {}}
              onMenuSelection={(option: GameStyleOptionType) =>
                handleMenuSelection(option, setGameStyle)
              }
            />
            <SearchSelectInput
              label="Choose a duration for post"
              value={duration?.label}
              options={durationOptions}
              onInputChange={() => {}}
              onMenuSelection={(option: DurationOptionType) =>
                handleMenuSelection(option, setDuration)
              }
            />
          </div>
        </div>
        <div>
          <span className="font-permanentmarker text-2xl text-[var(--palette-light-burgundy)]">
            Roles: {playerRoles.length} / {MAX_ROLE_SELECTIONS}
          </span>
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
            onClick={handleFormSubmit}
          />
        </div>
      </div>
    </div>
  );
}
