import React from 'react';
import logo from '../../assets/logo-toro.svg';
import { Container, Content } from './styles';

interface HeaderProps {
  onOpenLoginModal: () => void;
}

export function Header({
  onOpenLoginModal,
}: HeaderProps): React.ReactElement<HeaderProps> {
  return (
    <Container>
      <Content>
        <img src={logo} alt="Toro" />
        <button type="button" onClick={onOpenLoginModal}>
          New Login
        </button>
      </Content>
    </Container>
  );
}
