import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import ErrorNotifier from './ErrorNotifier';

describe('ErrorNotifier', () => {
  it('renders without crashing when there is no error', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const initialState = { employee: {}, dependents: [], payPeriods: [], error: { acknowledged: true } };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><ErrorNotifier /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });

  it('renders without crashing when there is an error', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const initialState = { employee: {}, dependents: [], payPeriods: [], error: { acknowledged: false } };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><ErrorNotifier /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });
});
