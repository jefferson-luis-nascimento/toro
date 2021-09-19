import React from 'react';
import { Switch } from 'react-router-dom';

import { SignIn } from '../pages/SignIn';
import { SignUp } from '../pages/SignUp';

import { Dashboard } from '../pages/Dashboard';
import { Order } from '../pages/Order';

import { Route } from './Route';

export function Routes() {
  return (
    <Switch>
      <Route path="/" exact component={SignIn} />
      <Route path="/signup" exact component={SignUp} />

      <Route path="/dashboard" exact component={Dashboard} isPrivate />
      <Route path="/order" exact component={Order} isPrivate />
    </Switch>
  );
}
