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

  svg {
    height: 40px;
    width: 115px;
  }

  button {
    font-size: 0.875rem;
    color: var(--white);
    background: var(--blue);
    border: 2px solid var(--white);
    padding: 0 2rem;
    border-radius: 0.25rem;
    height: 2rem;

    transition: filter 0.2s;

    &:hover {
      background: var(--white);
      color: var(--blue);
    }
  }
`;
