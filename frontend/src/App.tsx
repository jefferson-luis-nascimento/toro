import { useState } from 'react';
import Modal from 'react-modal';
import { ToastContainer } from 'react-toastify';
import { BrowserRouter as Router } from 'react-router-dom';

import { GlobalStyle } from './styles/global';
import { AppProvider } from './hooks';
import { Routes } from './Routes';

import 'react-toastify/dist/ReactToastify.css';

Modal.setAppElement('#root');

export function App() {
  return (
    <Router>
      <AppProvider>
        <Routes />

        <GlobalStyle />

        <ToastContainer autoClose={2000} />
      </AppProvider>
    </Router>
  );
}
