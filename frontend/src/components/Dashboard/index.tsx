import { Container, Title } from './styles';
import { TrendTable } from '../TrendTable';

interface TrendTableProps {
  onSetSymbol: (symbol: string) => void;
  onOpenNewOrderModal: () => void;
}

export function Dashboard({
  onSetSymbol,
  onOpenNewOrderModal,
}: TrendTableProps) {
  return (
    <Container>
      <Title>Lista de 5 ações negociadas nos últimos 7 dias</Title>
      <TrendTable
        onSetSymbol={onSetSymbol}
        onOpenNewOrderModal={onOpenNewOrderModal}
      />
    </Container>
  );
}
