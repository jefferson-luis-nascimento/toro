import { MdShoppingCart } from 'react-icons/md';

import { useTrends } from '../../hooks/useTrends';
import { Container } from './styles';

interface TrendTableProps {
  onSetSymbol: (symbol: string) => void;
  onOpenNewOrderModal: () => void;
}

export function TrendTable({
  onSetSymbol,
  onOpenNewOrderModal,
}: TrendTableProps) {
  const { trends } = useTrends();

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
