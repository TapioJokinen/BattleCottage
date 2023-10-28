import { DurationOptionType, SearchableSelectionOptionType } from '@/lib/types/components';

export const searchableSelectionDefaultOption: SearchableSelectionOptionType = {
  label: '',
  itemId: -1,
};

// TODO: Fetch these from the API?

export const durationOptions: DurationOptionType[] = [
  { label: '1 hour', itemId: 1 },
  { label: '2 hours', itemId: 2 },
  { label: '5 hours', itemId: 3 },
  { label: '12 hours', itemId: 4 },
  { label: '1 day', itemId: 5 },
  { label: '3 days', itemId: 6 },
  { label: '7 days', itemId: 7 },
  { label: '30 days', itemId: 8 },
];

export const gameModeOptions: SearchableSelectionOptionType[] = [
  { label: 'PvP', itemId: 1 },
  { label: 'PvE', itemId: 2 },
  { label: 'Co-op', itemId: 3 },
  { label: 'Multiplayer', itemId: 5 },
  { label: 'Battle Royale', itemId: 7 },
  { label: 'Other', itemId: 8 },
];

export const gameStyleOptions: SearchableSelectionOptionType[] = [
  { label: 'Competetive', itemId: 1 },
  { label: 'Casual', itemId: 2 },
  { label: 'Other', itemId: 4 },
];
