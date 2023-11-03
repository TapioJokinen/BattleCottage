import { useContext } from 'react';

import { AlertContext } from '@/app/context/AlertProvider';

const useAlert = () => useContext(AlertContext);

export default useAlert;
