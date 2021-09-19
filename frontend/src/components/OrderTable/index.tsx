import { Container } from './styles';

interface Trend {
  id: string;
  symbol: string;
  amount: number;
  currentPrice: number;
  total: number;
  orderDate: string;
}

interface OrderTableProps {
  trends: Trend[] | undefined;
}

export function OrderTable({ trends }: OrderTableProps) {
  return (
    <Container>
      <table>
        <thead>
          <tr>
            <th>Ativo</th>
            <th>Valor</th>
            <th>Quantidade</th>
            <th>Total</th>
            <th>Data Compra</th>
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
                <td>{trend.amount}</td>
                <td>
                  {new Intl.NumberFormat('pt-BR', {
                    style: 'currency',
                    currency: 'BRL',
                  }).format(trend.total)}
                </td>
                <td>
                  {new Intl.DateTimeFormat('pt-BR', {
                    day: 'numeric',
                    month: 'numeric',
                    year: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric',
                    second: 'numeric',
                  }).format(new Date(trend.orderDate))}
                </td>
              </tr>
            ))}
        </tbody>
      </table>
    </Container>
  );
}
