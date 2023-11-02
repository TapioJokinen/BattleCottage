/**
 * All API response types and types related to responses can be found here.
 */

import { Tokens } from '@/app/lib/types/auth';
import { LFGPostFormOptionsType } from './components';

export interface APIBaseResponseType {
  responseOk: boolean;
}

export interface APIMessageResponseType {
  message: string;
}

export interface LoginResponseType extends APIBaseResponseType {
  accessToken: string;
  refreshToken: string;
  expiration: string;
  message?: string;
}

export interface RegisterResponseType extends APIBaseResponseType, APIMessageResponseType {}

export interface RevokeResponseType extends APIBaseResponseType, APIMessageResponseType {}

export interface VerifyResponseType extends LoginResponseType {}

export interface RefreshResponse extends APIBaseResponseType, APIMessageResponseType, Tokens {
  accessTokenExpiration?: string;
  refreshTokenExpiration?: string;
}

export interface GamesResponseType extends APIBaseResponseType, APIMessageResponseType {
  next: string;
  previous: string;
  results: Array<GameFullResponseType>;
}

export interface GameFullResponseType {
  name: string;
  backgroundImage: string;
  id: number;
  dateAdded: string;
  dateUpdated: string;
}

export interface GameRoleRepsonseType {
  gameRoleId: number;
  gameRole: string;
}

export interface LFGPostCreateResponseType extends APIBaseResponseType, APIMessageResponseType {
  id: number;
  dateAdded: string;
  description: string;
  durationInMinutes: number;
  gameId: number;
  gameName: string;
  gameModeId: number;
  gameMode: string;
  gameStyleId: number;
  gameStyle: string;
  title: string;
  userId: string;
  gameRoles: Array<GameRoleRepsonseType>;
}

export interface LFGPostFormOptionsResponseType
  extends LFGPostFormOptionsType,
    APIBaseResponseType,
    APIMessageResponseType {}
