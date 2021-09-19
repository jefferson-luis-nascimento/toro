import { useState } from 'react';
import Modal from 'react-modal';
import { ToastContainer } from 'react-toastify';
import { BrowserRouter as Router } from 'react-router-dom';

import { Header } from './components/Header';
import { NewOrderModal } from './components/NewOrderModal';
import { GlobalStyle } from './styles/global';
import 'react-toastify/dist/ReactToastify.css';
import { AppProvider } from './hooks';
import { Routes } from './Routes';

Modal.setAppElement('#root');

export function App() {
  const [isNewOrderModalOpen, setIsNewOrderModalOpen] = useState(false);
  const [symbol, setSymbol] = useState('');

  const handleOpenNewOrderModal = () => {
    setIsNewOrderModalOpen(true);
  };

  const handleCloseNewOrderModal = () => {
    setIsNewOrderModalOpen(false);
  };

  return (
    <Router>
      <AppProvider>
        <Routes />
        <Header />

        <NewOrderModal
          isOpen={isNewOrderModalOpen}
          symbol={symbol}
          onRequestClose={handleCloseNewOrderModal}
        />

        <GlobalStyle />

        <ToastContainer autoClose={2000} />
      </AppProvider>
    </Router>
  );
}
