export interface SearchableSelectionOptionType {
  label: string;
  itemId: number;
}

export interface GameOptionType extends SearchableSelectionOptionType {}
export interface DurationOptionType extends SearchableSelectionOptionType {}
export interface GameModeOptionType extends SearchableSelectionOptionType {}
export interface GameStyleOptionType extends SearchableSelectionOptionType {}
