import { useState } from 'react';

import { NewOrderModal } from '../NewOrderModal';

import { Container, Title } from './styles';
import { TrendTable } from '../TrendTable';

export function Dashboard() {
  const [isNewOrderModalOpen, setIsNewOrderModalOpen] = useState(false);

  const handleOpenNewOrderModal = () => {
    setIsNewOrderModalOpen(true);
  };

  const handleCloseNewOrderModal = () => {
    setIsNewOrderModalOpen(false);
  };

  return (
    <Container>
      <NewOrderModal
        isOpen={isNewOrderModalOpen}
        onRequestClose={handleCloseNewOrderModal}
      />

      <Title>Lista de 5 ações negociadas nos últimos 7 dias</Title>
      <TrendTable onOpenNewOrderModal={handleOpenNewOrderModal} />
    </Container>
  );
}
