import { MouseEventHandler, useState } from 'react';

interface RoleButtonProps {
  role: { label: string; icon: React.ComponentType; value: string };
  numOfSelected: number;
  handleAdd: MouseEventHandler<HTMLDivElement>;
  handleRemove: MouseEventHandler<HTMLDivElement>;
}

export default function RoleButton({
  role,
  numOfSelected,
  handleAdd,
  handleRemove,
}: RoleButtonProps) {
  const [addTimeoutId, setAddTimeoutId] = useState<number | NodeJS.Timeout | undefined>(undefined);
  const [addIntervalId, setAddIntervalId] = useState<number | NodeJS.Timeout | undefined>(
    undefined,
  );
  const [removeTimeoutId, setRemoveTimeoutId] = useState<number | NodeJS.Timeout | undefined>(
    undefined,
  );
  const [removeIntervalId, setRemoveIntervalId] = useState<number | NodeJS.Timeout | undefined>(
    undefined,
  );

  const handleAddMouseDown = (event: React.MouseEvent<HTMLDivElement>) => {
    handleAdd(event);
    const timeout = setTimeout(() => {
      const interval = setInterval(() => {
        handleAdd(event);
      }, 50);
      setAddIntervalId(interval);
    }, 300);
    setAddTimeoutId(timeout);
  };

  const handleRemoveMouseDown = (event: React.MouseEvent<HTMLDivElement>) => {
    handleRemove(event);
    const timeout = setTimeout(() => {
      const interval = setInterval(() => {
        handleRemove(event);
      }, 50);
      setRemoveIntervalId(interval);
    }, 300);
    setRemoveTimeoutId(timeout);
  };

  const handleAddMouseUp = () => {
    clearTimeout(addTimeoutId);
    clearInterval(addIntervalId);
  };

  const handleRemoveMouseUp = () => {
    clearTimeout(removeTimeoutId);
    clearInterval(removeIntervalId);
  };

  return (
    <div className="block px-5 sm:inline-block">
      <div className="flex flex-col items-center">
        <span>{numOfSelected}</span>
        <div className="flex items-center">
          <div
            id={role.value}
            role="button"
            className="w-[24px] rounded bg-[var(--palette-dark-grey)] shadow shadow-[var(--palette-shadow)] selection:bg-transparent active:translate-y-1"
            onMouseDown={handleRemoveMouseDown}
            onMouseUp={handleRemoveMouseUp}
          >
            -
          </div>
          <div
            id={role.value}
            className="font-sourcecodepro m-2 flex min-w-[200px] items-center justify-between rounded bg-[var(--palette-dark-grey)] px-5 py-1 text-xl shadow shadow-[var(--palette-shadow)] sm:min-w-[230px]"
          >
            <span className="mr-2">{role.label}</span>
            <role.icon />
          </div>
          <div
            id={role.value}
            role="button"
            className="w-[24px] rounded bg-[var(--palette-dark-grey)] shadow shadow-[var(--palette-shadow)] selection:bg-transparent active:translate-y-1"
            onMouseDown={handleAddMouseDown}
            onMouseUp={handleAddMouseUp}
          >
            +
          </div>
        </div>
      </div>
    </div>
  );
}
