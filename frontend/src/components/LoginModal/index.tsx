import { FormEvent, useState } from 'react';
import Modal from 'react-modal';
import { MdClose } from 'react-icons/md';

import { api } from '../../services/api';

import { Container } from './styles';

interface NewOrderModalProps {
  isOpen: boolean;
  onRequestClose: () => void;
}

export function LoginModal({ isOpen, onRequestClose }: NewOrderModalProps) {
  const [cpf, setCPF] = useState('');

  async function handleCreateNewOrder(event: FormEvent) {
    event.preventDefault();

    const result = await api.post('/session', {
      cpf,
    });

    console.log(result);

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
        <h2>Acesse sua conta Toro.</h2>

        <input
          type="text"
          placeholder="CPF"
          value={cpf}
          onChange={event => setCPF(event.target.value)}
          readOnly
        />

        <button type="submit">Entrar</button>
      </Container>
    </Modal>
  );
}
