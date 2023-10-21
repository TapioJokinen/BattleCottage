'use client';

import SquaredCard from '@/components/cards/SquaredCard';
import { useRouter } from 'next/navigation';
import { useState } from 'react';

export default function ActionCardContainer() {
  const router = useRouter();

  return (
    <div className="flex w-full items-center justify-center p-5">
      <SquaredCard
        type="button"
        title="Create LFG Post"
        textSecondary='Create a "Looking For Group" post to find other players to play with.'
        isActive={true}
        onClick={() => router.push('/lfg/create')}
      />
    </div>
  );
}
