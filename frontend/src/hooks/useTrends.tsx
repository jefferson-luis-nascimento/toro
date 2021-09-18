import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from 'react';

import { api } from '../services/api';

interface Trend {
  symbol: string;
  currentPrice: number;
}

interface TrendProviderProps {
  children: ReactNode;
}

type TrendInput = Omit<Trend, 'id' | 'createdAt'>;

interface TrendContextData {
  trends: Trend[];
  createTrend: (trend: TrendInput) => Promise<void>;
}

const TrendsContext = createContext<TrendContextData>({} as TrendContextData);

export function TrendsProvider({ children }: TrendProviderProps) {
  const [trends, setTrends] = useState<Trend[]>([]);

  useEffect(() => {
    async function loadTrends() {
      const response = await api.get<Trend[]>('/trends');
      setTrends(response.data);
    }

    loadTrends();
  }, []);

  async function createTrend(trendInput: TrendInput) {
    const response = await api.post('/trends', {
      ...trendInput,
      createdAt: new Date(),
    });

    const { trend } = response.data;
    setTrends([...trends, trend]);
  }

  return (
    <TrendsContext.Provider value={{ trends, createTrend }}>
      {children}
    </TrendsContext.Provider>
  );
}

export function useTrends() {
  const context = useContext(TrendsContext);

  return context;
}
