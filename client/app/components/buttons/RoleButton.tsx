import { MouseEventHandler, useState } from 'react';

interface RoleButtonProps {
  label: string;
  roleId: number;
  Icon: React.ComponentType;
  numOfSelected: number;
  handleAdd: MouseEventHandler<HTMLDivElement>;
  handleRemove: MouseEventHandler<HTMLDivElement>;
}

export default function RoleButton({
  label,
  roleId,
  Icon,
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
    <div className="role-button-main">
      <div className="role-button-container">
        <span>{numOfSelected}</span>
        <div className="role-button-controls">
          <div
            id={roleId.toString()}
            role="button"
            className="role-button-adjust-button"
            onMouseDown={handleRemoveMouseDown}
            onMouseUp={handleRemoveMouseUp}
          >
            -
          </div>
          <div id={roleId.toString()} className="role-button-name-and-icon">
            <span className="mr-2">{label}</span>
            <Icon />
          </div>
          <div
            id={roleId.toString()}
            role="button"
            className="role-button-adjust-button"
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
