import { MouseEventHandler, ReactNode } from 'react';

interface RoleButtonProps {
  role: { text: string; icon: ReactNode; value: string };
  onClick: MouseEventHandler<HTMLDivElement>;
}

export default function RoleButton({ role, onClick }: RoleButtonProps) {
  return (
    <div className="m-2 mx-3 block sm:inline-block">
      <div className="flex flex-col items-center">
        <span>0</span>
        <div className="flex items-center">
          <div
            role="button"
            className="w-[24px] rounded bg-[var(--palette-dark-grey)] shadow shadow-[var(--palette-shadow)]"
          >
            -
          </div>

          <div id={role.value} className="role-button">
            <span className="mr-2">{role.text}</span>
            {role.icon}
          </div>
          <div
            role="button"
            className="w-[24px] rounded bg-[var(--palette-dark-grey)] shadow shadow-[var(--palette-shadow)]"
          >
            +
          </div>
        </div>
      </div>
    </div>
  );
}
