import React from 'react';
import logo from '../../assets/logo-toro.svg';
import { Container, Content, RightContent } from './styles';

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
        <RightContent>
          <button type="button" onClick={onOpenLoginModal}>
            <span>Entrar</span>
          </button>
          <button type="button" onClick={onOpenLoginModal}>
            <span>Cadastre-se</span>
          </button>
        </RightContent>
      </Content>
    </Container>
  );
}
