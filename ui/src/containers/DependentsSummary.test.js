import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import DependentsSummary from './DependentsSummary';

describe('DependentsSummary', () => {
  it('renders without crashing with no content', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const initialState = { employee: {}, dependents: [], payPeriods: [] };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><DependentsSummary /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });

  it('renders without crashing with content', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const dependents = [
      { employeeId: 1, id: 1, firstName: 'FN1', lastName: 'LN1', annualBenefitsCost: '123.45', notes: 'notes1' },
      { employeeId: 1, id: 2, firstName: 'FN2', lastName: 'LN2', annualBenefitsCost: '123.45', notes: 'notes2' },
      { employeeId: 1, id: 3, firstName: 'FN3', lastName: 'LN3', annualBenefitsCost: '123.45', notes: 'notes3' },
    ];
    const initialState = { employee: {}, dependents: dependents, payPeriods: [] };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><DependentsSummary /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });
});
