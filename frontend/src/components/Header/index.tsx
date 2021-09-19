import { Link, NavLink } from 'react-router-dom';

import logo from '../../assets/logo-toro.svg';
import {
  Container,
  Content,
  RightContent,
  LinkElem,
  LeftContent,
} from './styles';

import { useAuth } from '../../hooks/useAuth';

export function Header() {
  const { signed, user, signOut } = useAuth();

  function handleSignOut() {
    signOut();
  }

  return (
    <Container>
      <Content>
        <LeftContent>
          <Link id="logo" to="/dashboard">
            <img src={logo} alt="Toro" />
          </Link>

          <div>
            <LinkElem to="/dashboard">Dashboard</LinkElem>

            <LinkElem to="/order">Comprar Ações</LinkElem>
          </div>
        </LeftContent>

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
