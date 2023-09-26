import Link from 'next/link';

export default function FormLinkButton({ text, link }: { text: string; link: string }) {
  return (
    <div className="secondary-link">
      <Link href={link} className="underline">
        {text}
      </Link>
    </div>
  );
}
