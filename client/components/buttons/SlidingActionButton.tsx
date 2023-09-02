import { MouseEventHandler } from 'react';

export default function SlidingActionButton({
  text,
  onClick,
}: {
  text: string | JSX.Element;
  onClick: MouseEventHandler<HTMLDivElement>;
}) {
  return (
    <div role="button" className="button-slide-main !mt-10 justify-center" onClick={onClick}>
      <span>{text}</span>
    </div>
  );
}
