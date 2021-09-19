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
`;

export const RightContent = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;

  > span {
    font-size: 0.875rem;
    color: var(--white);
    margin-right: 20px;
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
