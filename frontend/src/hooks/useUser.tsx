import { createContext, ReactNode, useContext, useState } from 'react';

import { api } from '../services/api';

interface User {
  name: string;
  cpf: string;
}

interface UserProviderProps {
  children: ReactNode;
}

interface UserContextData {
  user: User;
  createUser: (user: User) => Promise<void>;
}

const UserContext = createContext<UserContextData>({} as UserContextData);

export function UserProvider({ children }: UserProviderProps) {
  const [user, setUser] = useState<User>({} as User);

  async function createUser(newUser: User) {
    const response = await api.post<User>('/user', newUser);

    setUser(response.data);
  }

  return (
    <UserContext.Provider value={{ user, createUser }}>
      {children}
    </UserContext.Provider>
  );
}

export function useUser() {
  const context = useContext(UserContext);

  return context;
}
