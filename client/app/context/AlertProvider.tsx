import { createContext, ReactNode, useCallback, useMemo, useState } from 'react';

export interface AlertContextType {
  open: boolean;
  message: string;
  status: 'success' | 'warning' | 'error' | 'info';
  raiseAlert: (message: AlertContextType['message'], status: AlertContextType['status']) => void;
  closeAlert: () => void;
}

export const AlertContext = createContext<AlertContextType>(null!);

export function AlertProvider({ children }: { children: ReactNode }) {
  const [open, setOpen] = useState<boolean>(false);
  const [message, setMessage] = useState<string>('');
  const [status, setStatus] = useState<AlertContextType['status']>('info');

  const raiseAlert = useCallback(
    (message: AlertContextType['message'], status: AlertContextType['status']) => {
      setOpen(true);
      setMessage(message);
      setStatus(status);
    },
    [],
  );

  const closeAlert = useCallback(() => {
    setOpen(false);
    setMessage('');
    setStatus('info');
  }, []);

  const value = useMemo(
    () => ({
      open,
      message,
      status,
      raiseAlert,
      closeAlert,
    }),
    [open, message, status, raiseAlert, closeAlert],
  );
  return <AlertContext.Provider value={value}>{children}</AlertContext.Provider>;
}
