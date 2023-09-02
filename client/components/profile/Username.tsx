export default async function UserName({ username }: { username: string }) {
  return (
    <div className="hidden font-opensans text-lg xl:flex">
      <span>{username}</span>
    </div>
  );
}
