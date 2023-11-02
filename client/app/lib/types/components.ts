/**
 * Types that are used in components can be found here.
 */

export interface SearchSelectInputOptionType {
  label: string;
  itemId: number;
}

export interface GameOptionType extends SearchSelectInputOptionType {}
export interface DurationOptionType extends SearchSelectInputOptionType {}
export interface GameModeOptionType extends SearchSelectInputOptionType {}
export interface GameStyleOptionType extends SearchSelectInputOptionType {}

export interface LFGPostFormOptionType {
  id: number;
  name: string;
}

export interface LFGPostDurationInMinutesOptionType extends LFGPostFormOptionType {
  durationInMinutes: number;
}

export interface LFGPostFormOptionsType {
  gameModes: Array<LFGPostFormOptionType>;
  gameStyles: Array<LFGPostFormOptionType>;
  gameRoles: Array<LFGPostFormOptionType>;
  durationsInMinutes: Array<LFGPostDurationInMinutesOptionType>;
}

export interface LFGPost {
  title: string;
  description: string;
  durationId: number;
  gameId: number;
  gameModeId: number;
  gameStyleId: number;
  gameRoleIds: Array<number>;
}
