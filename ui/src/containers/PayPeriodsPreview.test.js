import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import PayPeriodsPreview from './PayPeriodsPreview';

describe('PayPeriodsPreview', () => {
  it('renders without crashing with no content', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const initialState = { employee: {}, dependents: [], payPeriods: [] };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><PayPeriodsPreview /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });

  it('renders without crashing with content', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const payPeriods = [
      { Number: 1, GrossPay: '123.45', Deductions: '23.45', NetPay: '100.00' },
      { Number: 2, GrossPay: '123.45', Deductions: '23.45', NetPay: '100.00' },
      { Number: 3, GrossPay: '123.45', Deductions: '23.45', NetPay: '100.00' },
    ];
    const initialState = { employee: {}, dependents: [], payPeriods: payPeriods };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><PayPeriodsPreview /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });
});
