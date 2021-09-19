import { useEffect, useState } from 'react';

import { Container, Card, SideCard, Type, Value } from './styles';
import { OrderTable } from '../../components/OrderTable';

import { useAuth } from '../../hooks/useAuth';
import { api } from '../../services/api';

interface Trend {
  id: string;
  symbol: string;
  amount: number;
  currentPrice: number;
  total: number;
  orderDate: string;
}

interface UserPosition {
  id: string;
  name: string;
  cpf: string;
  positions: Trend[];
  checkingAccountAmount: number;
  consolidated: number;
}

export function Dashboard() {
  const { user } = useAuth();
  const [userPosition, setUserPosition] = useState<UserPosition>();

  useEffect(() => {
    async function loadUserPosition() {
      const response = await api.get(`/userposition/${user.id}`);
      setUserPosition(response.data);
    }

    loadUserPosition();
  }, [user.id]);

  return (
    <Container>
      <Card>
        <SideCard>
          <Type>Saldo C/C</Type>
          <Value>
            {new Intl.NumberFormat('pt-BR', {
              style: 'currency',
              currency: 'BRL',
            }).format(userPosition?.checkingAccountAmount || 0)}
          </Value>
        </SideCard>
        <SideCard>
          <Type>Saldo Consolidado</Type>
          <Value>
            {new Intl.NumberFormat('pt-BR', {
              style: 'currency',
              currency: 'BRL',
            }).format(userPosition?.consolidated || 0)}
          </Value>
        </SideCard>
      </Card>
      <OrderTable trends={userPosition?.positions} />
    </Container>
  );
}
