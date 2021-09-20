import {
  createContext,
  ReactNode,
  useCallback,
  useContext,
  useState,
} from 'react';
import { toast } from 'react-toastify';

import { api } from '../services/api';

interface User {
  id: string;
  name: string;
  cpf: string;
}

interface AuthState {
  user: User;
  token: string;
}

interface SignInCredentials {
  cpfSignIn: string;
}

interface AuthContextData {
  user: User;
  signed?: boolean;
  signIn(creadentials: SignInCredentials): void;
  signOut(): void;
}

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

interface UserProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: UserProviderProps) {
  const [data, setData] = useState<AuthState>(() => {
    const user = localStorage.getItem('@Toro:user');
    const token = localStorage.getItem('@Toro:token');

    if (user && token) {
      return { user: JSON.parse(user), token };
    }

    return {} as AuthState;
  });

  const signIn = useCallback(({ cpfSignIn }) => {
    const cleanCpf = cpfSignIn.replace(/\D/g, '');

    api
      .post('/session', {
        cpf: cleanCpf,
      })
      .then(response => {
        const { id, cpf, name, token } = response.data;

        api.defaults.headers.Authorization = `Bearer ${token}`;

        const user = { id, cpf, name };

        localStorage.setItem('@Toro:user', JSON.stringify(user));
        localStorage.setItem('@Toro:token', token);

        setData({ user, token });
      })
      .catch(error => {
        let message = error?.response?.data?.message;

        if (!message) {
          message = error.message;
        }

        toast.error(message);
      });
  }, []);

  const signOut = useCallback(() => {
    localStorage.removeItem('@Toro:user');
    localStorage.removeItem('@Toro:token');

    setData({} as AuthState);
  }, []);

  return (
    <AuthContext.Provider
      value={{ user: data.user, signed: !!data.user, signIn, signOut }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);

  return context;
}
