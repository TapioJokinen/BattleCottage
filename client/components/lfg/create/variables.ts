import { SearchableSelectionOptionType } from '@/app/types';
import AWPerIcon from '@/components/icons/AWPerIcon';
import AssaultIcon from '@/components/icons/AssaultIcon';
import BottomLaneIcon from '@/components/icons/BottomLaneIcon';
import CarryIcon from '@/components/icons/CarryIcon';
import CombatSupportIcon from '@/components/icons/CombatSupportIcon';
import EGirlIcon from '@/components/icons/EGirlIcon';
import EntryFraggerIcon from '@/components/icons/EntryFraggerIcon';
import FriendlyFaceIcon from '@/components/icons/FriedlyFaceIcon';
import FunnyFaceIcon from '@/components/icons/FunnyFaceIcon';
import HealerIcon from '@/components/icons/HealerIcon';
import JungleIcon from '@/components/icons/JungleIcon';
import LurkerIcon from '@/components/icons/LurkerIcon';
import MedicIcon from '@/components/icons/MedicIcon';
import MidLaneIcon from '@/components/icons/MidLaneIcon';
import ReconIcon from '@/components/icons/ReconIcon';
import RefraggerIcon from '@/components/icons/RefraggerIcon';
import SeriousFaceIcon from '@/components/icons/SeriousFaceIcon';
import SilentIcon from '@/components/icons/SilentIcon';
import StrategyCallerIcon from '@/components/icons/StrategyCallerIcon';
import SupportIcon from '@/components/icons/SupportIcon';
import SwordIcon from '@/components/icons/SwordIcon';
import TankIcon from '@/components/icons/TankIcon';
import TopLaneIcon from '@/components/icons/TopLaneIcon';

export const durations: SearchableSelectionOptionType[] = [
  { text: 'One Hour', value: 60 },
  { text: 'Five Hours', value: 60 * 5 },
  { text: 'One Day', value: 60 * 24 },
  { text: 'One Week', value: 60 * 24 * 7 },
  { text: 'Thirty Days', value: 60 * 24 * 30 },
];

export const gameFormats: SearchableSelectionOptionType[] = [
  { text: 'PvP', value: 1 },
  { text: 'PvE', value: 2 },
];

export const gameStyles: SearchableSelectionOptionType[] = [
  { text: 'Competetive', value: 1 },
  { text: 'Casual', value: 2 },
];

export const firstColRoles = [
  {
    text: 'Tank',
    value: 'tank',
    icon: TankIcon,
  },
  {
    text: 'Healer',
    value: 'healer',
    icon: HealerIcon,
  },
  {
    text: 'DPS',
    value: 'dps',
    icon: SwordIcon,
  },
];

export const secondColRoles = [
  {
    text: 'Top Lane',
    value: 'toplane',
    icon: TopLaneIcon,
  },
  {
    text: 'Bot Lane',
    value: 'botlane',
    icon: BottomLaneIcon,
  },
  {
    text: 'Mid Lane',
    value: 'midlane',
    icon: MidLaneIcon,
  },
  {
    text: 'Jungle',
    value: 'jungle',
    icon: JungleIcon,
  },
  {
    text: 'Support',
    value: 'support',
    icon: SupportIcon,
  },
];

export const thirdColRoles = [
  {
    text: 'Entry Fragger',
    value: 'entryfragger',
    icon: EntryFraggerIcon,
  },
  {
    text: 'Refragger',
    value: 'refragger',
    icon: RefraggerIcon,
  },
  {
    text: 'Strategy Caller',
    value: 'strategycaller',
    icon: StrategyCallerIcon,
  },
  {
    text: 'Lurker',
    value: 'lurker',
    icon: LurkerIcon,
  },
  {
    text: 'AWPer',
    value: 'awper',
    icon: AWPerIcon,
  },
];

export const fourthColRoles = [
  {
    text: 'Combat Support',
    value: 'combatsupport',
    icon: CombatSupportIcon,
  },
  {
    text: 'Medic',
    value: 'medic',
    icon: MedicIcon,
  },
  {
    text: 'Assault',
    value: 'assault',
    icon: AssaultIcon,
  },
  {
    text: 'Recon',
    value: 'recon',
    icon: ReconIcon,
  },
];

export const fifthColRoles = [
  {
    text: 'Friendly',
    value: 'friendly',
    icon: FriendlyFaceIcon,
  },
  {
    text: 'Funny',
    value: 'funny',
    icon: FunnyFaceIcon,
  },
  {
    text: 'Serious',
    value: 'serious',
    icon: SeriousFaceIcon,
  },
  {
    text: 'e-Girl',
    value: 'egirl',
    icon: EGirlIcon,
  },
  {
    text: 'Silent',
    value: 'silent',
    icon: SilentIcon,
  },
  {
    text: 'Carry',
    value: 'carry',
    icon: CarryIcon,
  },
];
