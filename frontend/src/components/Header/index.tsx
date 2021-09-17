import React from 'react';
import logo from '../../assets/toro-logo.png';
import { Container, Content } from './styles';

interface HeaderProps {
  onOpenNewTransactionModal: () => void;
}

export function Header({
  onOpenNewTransactionModal,
}: HeaderProps): React.ReactElement<HeaderProps> {
  return (
    <Container>
      <Content>
        <img src={logo} alt="Toro" />
        <button type="button" onClick={onOpenNewTransactionModal}>
          Nova Transação
        </button>
      </Content>
    </Container>
  );
}
