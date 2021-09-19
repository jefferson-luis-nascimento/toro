import { FormEvent, useState } from 'react';

import { useHistory } from 'react-router-dom';

import { toast } from 'react-toastify';
import { Container } from './styles';

import { Input } from '../../components/Input';
import { Button } from '../../components/Button';

import { api } from '../../services/api';

export function SignUp() {
  const [cpf, setCPF] = useState('');
  const [name, setName] = useState('');
  const history = useHistory();

  function handleSignUp(event: FormEvent) {
    event.preventDefault();

    api
      .post('/users', { cpf, name })
      .then(response => {
        toast.success('Cadastro realizado com sucesso');
        history.push('/');
      })
      .catch(error => {
        toast.error(error?.response?.data?.message);
      });
  }

  return (
    <Container onSubmit={event => handleSignUp(event)}>
      <h2>Crie sua conta na Toro e ganhe R$ 100,00 de saldo.</h2>

      <Input
        name="cpf"
        label="Informe o CPF"
        type="text"
        placeholder="CPF"
        value={cpf}
        onChange={event => setCPF(event.target.value)}
        readOnly
      />

      <Input
        name="name"
        label="Informe o Nome"
        type="text"
        placeholder="Nome"
        value={name}
        onChange={event => setName(event.target.value)}
        readOnly
      />

      <Button type="submit">Cadastrar</Button>
    </Container>
  );
}
