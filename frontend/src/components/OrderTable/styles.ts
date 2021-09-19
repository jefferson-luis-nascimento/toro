import styled from 'styled-components';

export const Container = styled.div`
  margin-top: 1rem;

  table {
    width: 70%;
    border-spacing: 0.5rem;
    margin: 0 auto;
  }
  th {
    color: var(--blue);
    font-size: 1.25rem;
    font-weight: bold;
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
    padding: 1rem;

    &:first-child {
      color: var(--text-title);
    }
  }
`;
