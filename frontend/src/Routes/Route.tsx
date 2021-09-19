import React from 'react';
import {
  Route as ReactDomRoute,
  RouteProps as ReactDomRouteProps,
  Redirect,
} from 'react-router-dom';

import { useAuth } from '../hooks/useAuth';
import { DefaultLayout } from '../pages/_layouts/DefaultLayout';

interface RouteProps extends ReactDomRouteProps {
  isPrivate?: boolean;
  component: React.ComponentType<any>;
}

export function Route({
  isPrivate = false,
  component: Component,
  ...rest
}: RouteProps) {
  const { user } = useAuth();

  const Layout = DefaultLayout;

  return (
    <ReactDomRoute
      {...rest}
      render={({ location }) => {
        return isPrivate === !!user ? (
          <Layout>
            <Component {...rest} />
          </Layout>
        ) : (
          <Redirect
            to={{
              pathname: isPrivate ? '/' : '/dashboard',
              state: { from: location },
            }}
          />
        );
      }}
    />
  );
}
