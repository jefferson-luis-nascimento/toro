import { darken } from 'polished';
import { NavLink } from 'react-router-dom';
import styled from 'styled-components';

export const Container = styled.header`
  background: var(--blue);
`;

export const Content = styled.div`
  max-width: 1220px;
  margin: 0 auto;
  height: 72px;

  padding: 2rem 1rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;

export const LeftContent = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;

  svg {
    height: 40px;
    width: 115px;
  }

  > div {
    margin-left: 80px;
    width: 300px;
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
`;

export const LinkElem = styled(NavLink)`
  text-decoration: none;

  color: var(--white);
  padding-bottom: 5px;

  transition: color 0.2s;

  &.active {
    color: var(--light-gray);
    font-weight: bold;
    border-bottom-width: 3px;
    border-bottom-style: solid;
    border-color: var(--white);
  }

  &:hover {
    color: ${darken(0.2, '#d7d7d7')};
  }
`;

export const RightContent = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;

  > span {
    font-size: 0.875rem;
    color: var(--white);
    margin-right: 20px;
    padding-bottom: 5px;
  }

  button {
    font-size: 0.875rem;
    color: var(--white);
    background: var(--blue);
    border: 2px solid var(--white);
    padding: 1.1rem 1rem;
    border-radius: 0.25rem;
    height: 2rem;
    margin-right: 10px;

    display: flex;
    align-items: center;
    justify-content: center;

    transition: filter 0.2s;

    &:hover {
      background: var(--white);
      color: var(--blue);
    }
  }
`;
