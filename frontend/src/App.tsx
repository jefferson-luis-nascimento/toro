import { useState } from 'react';
import Modal from 'react-modal';
import { Dashboard } from './components/Dashboard';
import { Header } from './components/Header';
import { LoginModal } from './components/LoginModal';
import { GlobalStyle } from './styles/global';
import { TrendsProvider } from './hooks/useTrends';

Modal.setAppElement('#root');

export function App() {
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);

  const handleOpenLoginModal = () => {
    setIsLoginModalOpen(true);
  };

  const handleCloseLoginModal = () => {
    setIsLoginModalOpen(false);
  };

  return (
    <TrendsProvider>
      <Header onOpenLoginModal={handleOpenLoginModal} />

      <Dashboard />

      <LoginModal
        isOpen={isLoginModalOpen}
        onRequestClose={handleCloseLoginModal}
      />
      <GlobalStyle />
    </TrendsProvider>
  );
}
