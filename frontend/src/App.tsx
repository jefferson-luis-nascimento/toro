import { useState } from 'react';
import Modal from 'react-modal';
import { ToastContainer } from 'react-toastify';
import { BrowserRouter as Router } from 'react-router-dom';

import { Header } from './components/Header';

import { GlobalStyle } from './styles/global';
import 'react-toastify/dist/ReactToastify.css';
import { AppProvider } from './hooks';
import { Routes } from './Routes';

Modal.setAppElement('#root');

export function App() {
  return (
    <Router>
      <AppProvider>
        <Routes />
        <Header />

        <GlobalStyle />

        <ToastContainer autoClose={2000} />
      </AppProvider>
    </Router>
  );
}
