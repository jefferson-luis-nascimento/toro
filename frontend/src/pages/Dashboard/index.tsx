import { useState } from 'react';

import { Container, Title } from './styles';
import { TrendTable } from '../../components/TrendTable';
import { NewOrderModal } from '../../components/NewOrderModal';

export function Dashboard() {
  const [isNewOrderModalOpen, setIsNewOrderModalOpen] = useState(false);
  const [symbol, setSymbol] = useState('');

  const handleOpenNewOrderModal = () => {
    setIsNewOrderModalOpen(true);
  };

  const handleCloseNewOrderModal = () => {
    setIsNewOrderModalOpen(false);
  };
  return (
    <Container>
      <Title>Lista de 5 ações negociadas nos últimos 7 dias</Title>
      <TrendTable
        onSetSymbol={setSymbol}
        onOpenNewOrderModal={handleOpenNewOrderModal}
      />

      <NewOrderModal
        isOpen={isNewOrderModalOpen}
        symbol={symbol}
        onRequestClose={handleCloseNewOrderModal}
      />
    </Container>
  );
}
