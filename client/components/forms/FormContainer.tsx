import { ReactNode } from 'react';

export default function FormContainer({ children }: { children: ReactNode }) {
  return (
    <div className="form-container">
      <div className="form-input-container">{children}</div>
    </div>
  );
}
