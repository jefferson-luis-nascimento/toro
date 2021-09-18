import { InputHTMLAttributes } from 'react';
import { Container } from './styles';

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
  name: string;
}

export function Input({ label, name, ...rest }: InputProps) {
  return (
    <Container>
      {label && <label htmlFor={name}>{label}</label>}
      <input {...rest} />
    </Container>
  );
}
