'use client';

import SquaredCard from '@/components/cards/SquaredCard';
import { useRouter } from 'next/navigation';

export default function ActionCardContainer() {
  const router = useRouter();

  function handleCreateLFGPost() {
    router.push('/lfg/create');
  }

  return (
    <div className="flex w-full items-center justify-center p-5">
      <SquaredCard
        type="button"
        loading={false}
        title="Create LFG Post"
        textSecondary='Create a "Looking For Group" post to find other players to play with.'
        isActive={true}
        onClick={handleCreateLFGPost}
      />
    </div>
  );
}
