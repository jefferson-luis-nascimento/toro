import styled from 'styled-components';

export const Container = styled.div`
  margin-top: 1rem;

  table {
    width: 50%;
    border-spacing: 0.5rem;
    margin: 0 auto;
  }
  th {
    color: var(--blue);
    font-size: 1.5rem;
    font-weight: 400;
    padding: 1rem;
    text-align: center;
    line-height: 1.5rem;
  }

  td {
    border: 0;
    text-align: center;
    background: var(--light-gray);
    color: var(--text-body);
    border-radius: 0.25rem;

    &:first-child {
      color: var(--text-title);
    }

    > .order {
      max-width: 30px;
    }

    button {
      width: 100%;
      border: none;
      background: none;
      display: flex;
      justify-content: center;
      align-items: center;
      color: var(--blue);
      transition: background 0.2s;
      height: 3rem;

      &:hover {
        background: var(--blue);
        color: var(--white);
      }
    }
  }
`;
