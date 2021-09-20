import axios from 'axios';
import https from 'https';

export const api = axios.create({
  baseURL: 'http://localhost:5000/api/v1/',
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
});
