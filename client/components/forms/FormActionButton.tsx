import { MouseEventHandler } from 'react';

export default function FormActionButton({
  text,
  onClick,
}: {
  text: string;
  onClick: MouseEventHandler<HTMLDivElement>;
}) {
  return (
    <div className="button-slide-main !mt-10 justify-center" onClick={onClick}>
      <button>{text}</button>
    </div>
  );
}
