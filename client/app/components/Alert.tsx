'use client';

import { useEffect } from 'react';

import useAlert from '@/app/hooks/useAlert';

export default function Alert() {
  const alert = useAlert();

  useEffect(() => {
    if (alert.open) {
      setTimeout(() => {
        alert.closeAlert();
      }, 4000);
    }
  }, [alert]);

  function getAlertColor() {
    switch (alert.status) {
      case 'success':
        return 'bg-[var(--success)]';
      case 'warning':
        return 'bg-[var(--warning)]';
      case 'error':
        return 'bg-[var(--error)]';
      case 'info':
        return 'bg-[var(--info)]';

      default:
        return 'bg-[var(--critical)]';
    }
  }

  return (
    <>
      {alert.open && (
        <div
          className={`${getAlertColor()} fixed left-0 right-0 top-0 z-50 mx-3 ml-auto mr-auto mt-5 w-max break-words rounded p-5 text-center`}
        >
          <div className="font-roboto text-[0.9rem]">
            <span>{alert.message}</span>
          </div>
        </div>
      )}
    </>
  );
}
