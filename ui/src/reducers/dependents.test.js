import dependentsReducer from './dependents'
import { REGISTER_DEPENDENT } from '../actions/dependents'

describe('Dependents Reducer', () => {
  it ('should return an empty array for the initial state', () => {
    const suppliedState = undefined;
    const action = { type: '@@redux/INIT' };
    const expectedState = [];

    var result = dependentsReducer(suppliedState, action);

    expect(result).toEqual(expectedState);
  });

  it ('should add a new dependent to the array when the first dependent is successfully added', () => {
    const initialState = [];
    const newDependent = { employeeId: 1, firstName: 'fn', lastName: 'ln' };
    const action = {
      type: REGISTER_DEPENDENT,
      payload: {
        status: 201,
        data: newDependent
      }
    };

    var result = dependentsReducer(initialState, action);

    expect(result).toEqual([ newDependent ]);
  });

  it ('should add a new dependent to an existing array of dependents', () => {
    const dependent1 = { employeeId: 1, firstName: 'FN1', lastName: 'LN1' };
    const dependent2 = { employeeId: 1, firstName: 'FN2', lastName: 'LN2' };
    const dependent3 = { employeeId: 1, firstName: 'FN3', lastName: 'LN3' };
    const initialState = [ dependent1, dependent2, dependent3 ];
    const newDependent = { employeeId: 1, firstName: 'NEW FN', lastName: 'NEW LN' };
    const action = {
      type: REGISTER_DEPENDENT,
      payload: {
        status: 201,
        data: newDependent
      }
    };

    var result = dependentsReducer(initialState, action);

    expect(result).toContain(dependent1);
    expect(result).toContain(dependent2);
    expect(result).toContain(dependent3);
    expect(result).toContain(newDependent);
    expect(result).toHaveLength(4);
  });

  it ('should not modify the existing state when the result is not successful', () => {
    const preexistingDependent = { employeeId: 1, firstName: 'FN1', lastName: 'LN1' };
    const initialState = [ preexistingDependent ];
    const newDependent = { employeeId: 1, firstName: 'NEW FN', lastName: 'NEW LN' };
    const action = {
      type: REGISTER_DEPENDENT,
      payload: {
        status: 0, // Doesn't matter so long as it's not 201
        data: newDependent
      }
    };

    var result = dependentsReducer(initialState, action);

    expect(result).toEqual(initialState);
  });
});
