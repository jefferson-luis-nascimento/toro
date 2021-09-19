import { MdShoppingCart } from 'react-icons/md';

import { useEffect, useState } from 'react';
import { Container } from './styles';

import { api } from '../../services/api';

interface Trend {
  symbol: string;
  currentPrice: number;
}

interface TrendTableProps {
  onSetSymbol: (symbol: string) => void;
  onOpenNewOrderModal: () => void;
}

export function TrendTable({
  onSetSymbol,
  onOpenNewOrderModal,
}: TrendTableProps) {
  const [trends, setTrends] = useState<Trend[]>([]);

  useEffect(() => {
    async function loadTrends() {
      const response = await api.get<Trend[]>('/trends');
      setTrends(response.data);
    }

    loadTrends();
  }, []);

  function handleOpenNewOrderModal(currentSymbol: string) {
    onSetSymbol(currentSymbol);
    onOpenNewOrderModal();
  }

  return (
    <Container>
      <table>
        <thead>
          <tr>
            <th>Ativo</th>
            <th>Valor</th>
            <th> </th>
          </tr>
        </thead>
        <tbody>
          {trends &&
            trends.map(trend => (
              <tr key={trend.symbol}>
                <td>{trend.symbol}</td>
                <td>
                  {new Intl.NumberFormat('pt-BR', {
                    style: 'currency',
                    currency: 'BRL',
                  }).format(trend.currentPrice)}
                </td>
                <td className=".order">
                  <button
                    type="button"
                    onClick={() => handleOpenNewOrderModal(trend.symbol)}
                  >
                    <MdShoppingCart size={16} />
                  </button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
    </Container>
  );
}
