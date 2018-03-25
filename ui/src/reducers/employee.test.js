import employeeReducer from './employee'
import { REGISTER_EMPLOYEE } from '../actions/employee'

describe('Employee Reducer', () => {
  it ('should return an empty object for the initial state', () => {
    const suppliedState = undefined;
    const action = { type: '@@redux/INIT' };
    const expectedState = {};

    var result = employeeReducer(suppliedState, action);

    expect(result).toEqual(expectedState);
  });

  it ('should return an employee that has been successfully registered', () => {
    const initialState = {};
    const newEmployee = { id: 1, firstName: 'fn', lastName: 'ln' };
    const action = {
      type: REGISTER_EMPLOYEE,
      payload: {
        status: 201,
        data: newEmployee
      }
    };

    var result = employeeReducer(initialState, action);

    expect(result).toEqual(newEmployee);
  });

  it ('should not modify the existing state when the result is not successful', () => {
    const initialState = { };
    const newEmployee = { id: 1, firstName: 'NEW FN', lastName: 'NEW LN' };
    const action = {
      type: REGISTER_EMPLOYEE,
      payload: {
        status: 0, // Doesn't matter so long as it's not 201
        data: newEmployee
      }
    };

    var result = employeeReducer(initialState, action);

    expect(result).toEqual(initialState);
  });
});
