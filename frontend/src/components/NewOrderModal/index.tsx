import { FormEvent, useState } from 'react';
import Modal from 'react-modal';
import { MdClose } from 'react-icons/md';

import { api } from '../../services/api';

import { cpfMask } from '../../helper/cpfMaskHelper';
import { numberMask } from '../../helper/numberMaskHelper';

import { Container } from './styles';

import { Input } from '../Input';

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
  const [cpf, setCPF] = useState('');
  const [amount, setAmount] = useState('');

  async function handleCreateNewOrder(event: FormEvent) {
    event.preventDefault();

    const result = await api.post('/order', {
      cpf,
      symbol,
      amount,
    });

    console.log(result);

    setAmount('');
    setCPF('');

    onRequestClose();
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
          name="cpf"
          label="CPF"
          type="text"
          placeholder="CPF"
          value={cpf}
          onChange={event => setCPF(cpfMask(event.target.value))}
        />

        <Input
          name="amount"
          label="Quantidade"
          type="number"
          placeholder="Quantidade"
          value={amount}
          min="0"
          max="9999999"
          step="1"
          onChange={event => setAmount(numberMask(event.target.value))}
        />

        <button type="submit">Comprar</button>
      </Container>
    </Modal>
  );
}
