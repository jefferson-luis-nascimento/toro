import styled from 'styled-components';

export const Container = styled.main`
  max-width: 1220px;
  padding: 1.5rem 1rem;
  margin-left: 0 auto;
`;

export const Card = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin: 20px 100px;
`;

export const SideCard = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
`;

export const Type = styled.h3`
  color: var(--blue);
  display: block;
`;

export const Value = styled.h2`
  color: var(--light-blue);
  display: block;
`;
