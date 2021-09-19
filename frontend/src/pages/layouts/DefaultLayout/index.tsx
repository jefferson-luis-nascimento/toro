import { HTMLAttributes } from 'react';
import { Header } from '../../../components/Header';
import { Wrapper } from './styles';

type DefaultLayoutProps = HTMLAttributes<HTMLElement>;

export function DefaultLayout({ children }: DefaultLayoutProps) {
  return (
    <Wrapper>
      <Header />
      {children}
    </Wrapper>
  );
}
