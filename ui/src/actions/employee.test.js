import mockAxios from 'jest-mock-axios';
import { registerEmployee, REGISTER_EMPLOYEE } from './employee'

describe('Employee Actions', () => {
  afterEach(() => {
    mockAxios.reset();
  });

  it ('should create an action with a promise to call the employee registration API correctly', () => {
    const person = { firstName: 'fn', lastName: 'ln' };

    var mockDispatch = jest.fn();
    registerEmployee(person)(mockDispatch);

    expect(mockDispatch.mock.calls.length).toBe(1);

    var action = mockDispatch.mock.calls[0][0];
    expect(action).not.toBeNull();
    expect(action.type).toEqual(REGISTER_EMPLOYEE);
    expect(action.payload).toBeDefined();
    expect(mockAxios.post).toHaveBeenCalledWith(
      `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees`,
      person);
  });
});
