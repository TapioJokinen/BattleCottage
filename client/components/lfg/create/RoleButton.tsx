import { MouseEventHandler, useState } from 'react';

interface RoleButtonProps {
  role: { text: string; icon: React.ComponentType; value: string };
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
    <div className="m-2 mx-3 block sm:inline-block">
      <div className="flex flex-col items-center">
        <span>{numOfSelected}</span>
        <div className="flex items-center">
          <div
            id={role.value}
            role="button"
            className="role-handle-button"
            onMouseDown={handleRemoveMouseDown}
            onMouseUp={handleRemoveMouseUp}
          >
            -
          </div>
          <div id={role.value} className="role-button">
            <span className="mr-2">{role.text}</span>
            <role.icon />
          </div>
          <div
            id={role.value}
            role="button"
            className="role-handle-button"
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
