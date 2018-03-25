import errorReducer from './error'
import { ERROR_RAISED, ERROR_ACKNOWLEDGED } from '../actions/error'

describe('Error Reducer', () => {
  it('should return acknowledged for the initial state', () => {
    const suppliedState = undefined;
    const action = { type: '@@redux/INIT' };
    const expectedState = { acknowledged: true };

    var result = errorReducer(suppliedState, action);

    expect(result).toEqual(expectedState);
  });

  it('should set the acknowledged state to false when an error is raised', () => {
    const initialState = { acknowleded: true };
    const action = { type: ERROR_RAISED };
    const expectedState = { acknowledged: false };

    var result = errorReducer(initialState, action);

    expect(result).toEqual(expectedState);
  });

  it('should set the acknowledged state to true when an error is acknowledged', () => {
    const initialState = { acknowleded: false };
    const action = { type: ERROR_ACKNOWLEDGED };
    const expectedState = { acknowledged: true };

    var result = errorReducer(initialState, action);

    expect(result).toEqual(expectedState);
  });
});
