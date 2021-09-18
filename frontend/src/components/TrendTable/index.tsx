import { MdShoppingCart } from 'react-icons/md';

import { useTrends } from '../../hooks/useTrends';
import { Container } from './styles';

interface TrendTableProps {
  onOpenNewOrderModal: () => void;
}

export function TrendTable({ onOpenNewOrderModal }: TrendTableProps) {
  const { trends } = useTrends();

  return (
    <Container>
      <table>
        <thead>
          <tr>
            <th>Ativo</th>
            <th>Valor</th>
            <th>Comprar</th>
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
                  <button type="button" onClick={() => onOpenNewOrderModal}>
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
