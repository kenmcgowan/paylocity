import mockAxios from 'jest-mock-axios';
import { registerEmployee, REGISTER_EMPLOYEE } from './employee'

describe('Employee Actions', () => {
  afterEach(() => {
    mockAxios.reset();
  });

  it ('should create an action with a promise to call the employee registration API correctly', () => {
    const person = { firstName: 'fn', lastName: 'ln' };

    var action = registerEmployee(person);

    expect(action).not.toBeNull();
    expect(action.type).toEqual(REGISTER_EMPLOYEE);
    expect(action.payload).toBeDefined();
    expect(mockAxios.post).toHaveBeenCalledWith(
      `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees`,
      person);
  });
});
