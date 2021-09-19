import React, { ReactNode } from 'react';

import { AuthProvider } from './useAuth';

interface AppProviderProps {
  children: ReactNode;
}

export const AppProvider: React.FC = ({ children }) => (
  <AuthProvider>{children}</AuthProvider>
);
