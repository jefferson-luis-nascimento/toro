import logo from '../../assets/logo-toro.svg';
import { Container, Content, RightContent } from './styles';

import { useAuth } from '../../hooks/useAuth';

export function Header() {
  const { signed, user, signOut } = useAuth();

  function handleSignOut() {
    signOut();
  }

  return (
    <Container>
      <Content>
        <img src={logo} alt="Toro" />
        <RightContent>
          {signed ? (
            <>
              <span>{user.name}</span>
              <button type="button" onClick={handleSignOut}>
                <span>Sair</span>
              </button>
            </>
          ) : (
            <button type="button">
              <span>Cadastre-se</span>
            </button>
          )}
        </RightContent>
      </Content>
    </Container>
  );
}
