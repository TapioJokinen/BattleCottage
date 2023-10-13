import Image from 'next/image';
import Link from 'next/link';

function AvatarIcon({ src, size }: { src: string; size: number }) {
  return (
    <Link href="/profile" className="rounded-full border-2 border-[var(--palette-dark-grey)]">
      <Image
        src={src}
        width={size}
        height={size}
        alt="avatar"
        priority={true}
        className="rounded-full align-middle"
      />
    </Link>
  );
}

export default function Avatar() {
  return (
    <>
      <div className="no-select hidden cursor-pointer justify-center p-10 pb-5 xl:flex">
        <AvatarIcon src={`${process.env.NEXT_PUBLIC_IMAGES_URL}/img_avatar.png`} size={100} />
      </div>
      <div className="no-select flex cursor-pointer justify-center p-2 xl:hidden">
        <AvatarIcon src={`${process.env.NEXT_PUBLIC_IMAGES_URL}/img_avatar.png`} size={50} />
      </div>
    </>
  );
}
