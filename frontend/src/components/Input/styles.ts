import styled from 'styled-components';

export const Container = styled.div`
  label {
    display: block;
    color: var(--blue);
    font-size: 1rem;
    padding-bottom: 10px;
  }

  input {
    width: 100%;
    padding: 1.25rem;
    border-radius: 0.25rem;
    height: 2rem;

    border: 1px solid #d7d7d7;
    background: #e7e9ee;

    font-weight: 400;
    font-size: 1rem;
    margin-bottom: 1rem;

    &::placeholder {
      color: var(--text-title);
    }
  }
`;
