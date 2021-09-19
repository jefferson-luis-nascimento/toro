import { Link, NavLink, useHistory } from 'react-router-dom';

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
  const history = useHistory();

  function handleSignOut() {
    signOut();
  }

  function handleSignUp() {
    history.push('/signup');
  }

  return (
    <Container>
      <Content>
        <LeftContent>
          <Link id="logo" to="/dashboard">
            <img src={logo} alt="Toro" />
          </Link>
          {signed && (
            <div>
              <LinkElem to="/dashboard">Dashboard</LinkElem>

              <LinkElem to="/order">Comprar Ações</LinkElem>
            </div>
          )}
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
            <button type="button" onClick={handleSignUp}>
              <span>Cadastre-se</span>
            </button>
          )}
        </RightContent>
      </Content>
    </Container>
  );
}
