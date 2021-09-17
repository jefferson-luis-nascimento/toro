import { useState } from 'react';
import Modal from 'react-modal';
import { Dashboard } from './components/Dashboard';
import { Header } from './components/Header';
import { NewTransactionModal } from './components/NewTransactionModal';
import { GlobalStyle } from './styles/global';
import { TransactionsProvider } from './hooks/useTransactions';

Modal.setAppElement('#root');

export function App() {
  const [isNewTransactioModalOpen, setIsNewTransactioModalOpen] =
    useState(false);

  const handleOpenNewTransactionModal = () => {
    setIsNewTransactioModalOpen(true);
  };

  const handleCloseNewTransactioModal = () => {
    setIsNewTransactioModalOpen(false);
  };

  return (
    <TransactionsProvider>
      <Header onOpenNewTransactionModal={handleOpenNewTransactionModal} />
      <Dashboard />

      <NewTransactionModal
        isOpen={isNewTransactioModalOpen}
        onRequestClose={handleCloseNewTransactioModal}
      />
      <GlobalStyle />
    </TransactionsProvider>
  );
}
