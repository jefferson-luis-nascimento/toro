import { ButtonHTMLAttributes } from 'react';
import { Container } from './styles';

type ButtonProps = ButtonHTMLAttributes<HTMLInputElement>;

export function Button({ children }: ButtonProps) {
  return <Container>{children}</Container>;
}
