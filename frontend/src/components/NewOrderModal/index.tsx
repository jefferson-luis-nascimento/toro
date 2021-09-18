import { FormEvent, useState } from 'react';
import Modal from 'react-modal';

import { api } from '../../services/api';

import { Container } from './styles';

interface NewOrderModalProps {
  isOpen: boolean;
  onRequestClose: () => void;
}

export function NewOrderModal({ isOpen, onRequestClose }: NewOrderModalProps) {
  const [symbol, setSymbol] = useState('');
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
      />

      <Container onSubmit={event => handleCreateNewOrder(event)}>
        <h2>Comprar Ação</h2>
        <input
          type="text"
          placeholder="Ação"
          value={symbol}
          readOnly
          onChange={event => setSymbol(event.target.value)}
        />

        <input
          type="text"
          placeholder="CPF"
          value={cpf}
          onChange={event => setCPF(event.target.value)}
          readOnly
        />

        <input
          type="number"
          placeholder="Quantidade"
          value={amount}
          onChange={event => setAmount(event.target.value)}
        />

        <button type="submit">Comprar</button>
      </Container>
    </Modal>
  );
}
