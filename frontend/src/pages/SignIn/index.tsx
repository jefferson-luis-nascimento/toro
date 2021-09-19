import { FormEvent, useState } from 'react';
import Modal from 'react-modal';
import { MdClose } from 'react-icons/md';

import { useAuth } from '../../hooks/useAuth';
import { Input } from '../../components/Input';
import { Button } from '../../components/Button';

import { Container, Form } from './styles';
import { cpfMask } from '../../helper/cpfMaskHelper';

export function SignIn() {
  const [cpf, setCPF] = useState('');
  const { signIn } = useAuth();

  async function handleCreateNewOrder(event: FormEvent) {
    event.preventDefault();

    await signIn({ cpfSignIn: cpf });
  }

  return (
    <Container>
      <h2>Acesse sua conta Toro.</h2>

      <Form onSubmit={event => handleCreateNewOrder(event)}>
        <Input
          name="cpf"
          label="Informe o CPF"
          type="text"
          placeholder="CPF"
          value={cpf}
          onChange={event => setCPF(cpfMask(event.target.value))}
        />

        <Button type="submit">Entrar</Button>
      </Form>
    </Container>
  );
}
