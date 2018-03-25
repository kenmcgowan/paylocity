import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import PersonEditor from './PersonEditor';

const middlewares = [];
const mockStore = configureMockStore(middlewares);
const initialState = {};
const store = mockStore(initialState);

describe('PersonEditor', () => {
  it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<Provider store={store}><MuiThemeProvider><PersonEditor /></MuiThemeProvider></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });

  it('is not visible by default', () => {
    expect(PersonEditor.defaultProps.visible).toEqual(false);
  });

  it('has no title by default', () => {
    expect(PersonEditor.defaultProps.title).toEqual("");
  });
});
