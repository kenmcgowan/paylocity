import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import EmployeeEditor from './EmployeeEditor';

describe('EmployeeEditor', () => {
  it('renders without crashing', () => {
    const middlewares = [];
    const mockStore = configureMockStore(middlewares);
    const initialState = { employee: {}, dependents: [], payPeriods: [] };
    const store = mockStore(initialState);

    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><EmployeeEditor /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });
});
