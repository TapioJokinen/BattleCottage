'use client';

import SearchableSelection from '@/components/selections/SearchableSelection';
import { MouseEventHandler, useEffect, useState } from 'react';
import FormControl from '../../forms/advanced/FormControl';
import { gamesFetchByName } from '@/app/api/games';
import { SearchableSelectionOptionType } from '@/app/types';
import { useSession } from 'next-auth/react';
import FormTextInput from '@/components/forms/advanced/FormTextInput';
import FormTextareaInput from '@/components/forms/advanced/FormTextareaInput';
import RoleButton from './RoleButton';

import {
  durations,
  fifthColRoles,
  firstColRoles,
  fourthColRoles,
  gameModes,
  gameStyles,
  secondColRoles,
  thirdColRoles,
} from './variables';

interface OptionFormType {
  gameId: number | null;
  title: string | null;
  description: string | null;
  duration: number | null;
  mode: number | null;
  style: number | null;
  roles: string[];
}

export default function LfgPostForm() {
  const session = useSession();

  const [optionForm, setOptionForm] = useState<OptionFormType>({
    gameId: null,
    title: null,
    description: null,
    duration: null,
    mode: null,
    style: null,
    roles: [],
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

  function handleRoleAdd(event: React.MouseEvent<HTMLDivElement>) {
    const target = event.target as HTMLDivElement;
    const role = target.id;
    if (role !== null) {
      setOptionForm((prev) => ({
        ...prev,
        roles: [...prev.roles].length < 100 ? [...prev.roles, role] : [...prev.roles],
      }));
    }
  }

  function handleRoleRemove(event: React.MouseEvent<HTMLDivElement>) {
    const target = event.target as HTMLDivElement;
    const role = target.id;
    if (role !== null) {
      setOptionForm((prev) => {
        const newRoles = [...prev.roles];
        const index = newRoles.indexOf(role);
        if (index > -1) {
          newRoles.splice(index, 1);
        }
        return { ...prev, roles: newRoles };
      });
    }
  }

  function handleCreatePost() {
    console.log(optionForm);
  }

  function validateForm() {
    if (
      optionForm.gameId &&
      optionForm.title &&
      optionForm.title.length > 2 &&
      optionForm.description &&
      optionForm.description.length > 2 &&
      optionForm.duration &&
      optionForm.mode &&
      optionForm.style &&
      optionForm.roles.length > 0
    ) {
      return true;
    }
    return false;
  }

  return (
    <div className="w-full">
      <div className="lfg-form-input-container-upper">
        <div className="lfg-form-input-wrapper">
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
            error={
              optionForm.description && optionForm.description.length > 2000
                ? 'Description is too long'
                : ''
            }
          >
            <FormTextareaInput
              handleInputChange={handleDescriptionInput}
              placeholder="Type your description here"
            />
          </FormControl>
        </div>
        <div className="lfg-form-input-wrapper">
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
              options={gameModes}
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
      <div className="lfg-input-container-lower">
        <hr className="lfg-divider" />
        <div className="lfg-form-input-wrapper">
          <FormControl
            text={`Select roles (${optionForm.roles.length}/100)`}
            ok={optionForm.roles.length > 0}
          >
            <div className="inline-block w-full text-center">
              {firstColRoles.map((role) => (
                <RoleButton
                  key={role.value}
                  role={role}
                  numOfSelected={optionForm.roles.filter((r) => r === role.value).length}
                  handleAdd={handleRoleAdd}
                  handleRemove={handleRoleRemove}
                />
              ))}
              {secondColRoles.map((role) => (
                <RoleButton
                  key={role.value}
                  role={role}
                  numOfSelected={optionForm.roles.filter((r) => r === role.value).length}
                  handleAdd={handleRoleAdd}
                  handleRemove={handleRoleRemove}
                />
              ))}
              {thirdColRoles.map((role) => (
                <RoleButton
                  key={role.value}
                  role={role}
                  numOfSelected={optionForm.roles.filter((r) => r === role.value).length}
                  handleAdd={handleRoleAdd}
                  handleRemove={handleRoleRemove}
                />
              ))}
              {fourthColRoles.map((role) => (
                <RoleButton
                  key={role.value}
                  role={role}
                  numOfSelected={optionForm.roles.filter((r) => r === role.value).length}
                  handleAdd={handleRoleAdd}
                  handleRemove={handleRoleRemove}
                />
              ))}
              {fifthColRoles.map((role) => (
                <RoleButton
                  key={role.value}
                  role={role}
                  numOfSelected={optionForm.roles.filter((r) => r === role.value).length}
                  handleAdd={handleRoleAdd}
                  handleRemove={handleRoleRemove}
                />
              ))}
            </div>
          </FormControl>
        </div>
      </div>
      <div className="flex w-full justify-end">
        <button
          disabled={!validateForm()}
          className="create-post-button"
          onClick={handleCreatePost}
        >
          Create a Post
        </button>
      </div>
    </div>
  );
}
