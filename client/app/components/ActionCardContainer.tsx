'use client';

import SquaredCard from '@/app/components/cards/SquaredCard';
import { useRouter } from 'next/navigation';

export default function ActionCardContainer() {
  const router = useRouter();

  return (
    <div className="action-card-container-main">
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
