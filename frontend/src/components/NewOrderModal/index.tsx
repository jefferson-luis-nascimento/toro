import { FormEvent, useState } from 'react';
import Modal from 'react-modal';
import { MdClose } from 'react-icons/md';
import { toast } from 'react-toastify';

import { useHistory } from 'react-router-dom';
import { Container } from './styles';
import { Input } from '../Input';
import { Button } from '../Button';

import { api } from '../../services/api';
import { useAuth } from '../../hooks/useAuth';

import { numberMask } from '../../helper/numberMaskHelper';

interface NewOrderModalProps {
  isOpen: boolean;
  symbol?: string;
  onRequestClose: () => void;
}

export function NewOrderModal({
  isOpen,
  symbol,
  onRequestClose,
}: NewOrderModalProps) {
  const [amount, setAmount] = useState('');
  const history = useHistory();
  const { user } = useAuth();

  function handleCreateNewOrder(event: FormEvent) {
    event.preventDefault();

    api
      .post('/order', {
        cpf: user.cpf,
        symbol,
        amount,
      })
      .then(response => {
        toast.success('Compra realizar com sucesso');

        setAmount('');
        history.push('/dashboard');
        onRequestClose();
      })
      .catch(error => {
        let message = error?.response?.data?.message;

        if (!message) {
          message = error.message;
        }

        toast.error(message);
      });
  }

  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={onRequestClose}
      overlayClassName="react-modal-overlay"
      className="react-modal-content"
    >
      <button
        type="button"
        onClick={onRequestClose}
        className="react-modal-close"
        aria-label="X"
      >
        <MdClose color="red" size={28} />
      </button>

      <Container onSubmit={event => handleCreateNewOrder(event)}>
        <h2>Comprar Ação</h2>
        <Input
          type="text"
          name="symbol"
          label="Ação"
          placeholder="Ação"
          value={symbol}
          readOnly
        />

        <Input
          name="amount"
          label="Quantidade"
          type="number"
          placeholder="Quantidade"
          value={amount}
          onChange={event => setAmount(numberMask(event.target.value))}
        />

        <Button type="submit">Comprar</Button>
      </Container>
    </Modal>
  );
}
