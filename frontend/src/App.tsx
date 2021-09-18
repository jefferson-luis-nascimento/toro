import { useState } from 'react';
import Modal from 'react-modal';
import { ToastContainer } from 'react-toastify';

import { Dashboard } from './components/Dashboard';
import { Header } from './components/Header';
import { LoginModal } from './components/LoginModal';
import { NewOrderModal } from './components/NewOrderModal';
import { GlobalStyle } from './styles/global';
import { TrendsProvider } from './hooks/useTrends';
import 'react-toastify/dist/ReactToastify.css';

Modal.setAppElement('#root');

export function App() {
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
  const [isNewOrderModalOpen, setIsNewOrderModalOpen] = useState(false);
  const [symbol, setSymbol] = useState('');

  const handleOpenNewOrderModal = () => {
    setIsNewOrderModalOpen(true);
  };

  const handleCloseNewOrderModal = () => {
    setIsNewOrderModalOpen(false);
  };
  const handleOpenLoginModal = () => {
    setIsLoginModalOpen(true);
  };

  const handleCloseLoginModal = () => {
    setIsLoginModalOpen(false);
  };

  return (
    <TrendsProvider>
      <Header onOpenLoginModal={handleOpenLoginModal} />

      <Dashboard
        onSetSymbol={setSymbol}
        onOpenNewOrderModal={handleOpenNewOrderModal}
      />

      <NewOrderModal
        isOpen={isNewOrderModalOpen}
        symbol={symbol}
        onRequestClose={handleCloseNewOrderModal}
      />

      <LoginModal
        isOpen={isLoginModalOpen}
        onRequestClose={handleCloseLoginModal}
      />
      <GlobalStyle />

      <ToastContainer autoClose={2000} />
    </TrendsProvider>
  );
}
