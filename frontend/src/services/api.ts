import axios from 'axios';

export const api = axios.create({
  baseURL: 'http://localhost:15383/api/v1/',
});
