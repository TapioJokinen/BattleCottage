import SignOutButton from '@/components/buttons/SignOutButton';
import Avatar from '@/components/profile/Avatar';
import UserName from '@/components/profile/Username';
import { getServerSession } from 'next-auth/next';
import { authOptions } from '@/app/api/auth/[...nextauth]/route';

export default async function Profile() {
  const session = await getServerSession(authOptions);
  return (
    <div className="flex flex-col items-center justify-center">
      <Avatar />
      <UserName username={session?.email || 'Unknown User'} />
      {session?.email && <SignOutButton />}
    </div>
  );
}
