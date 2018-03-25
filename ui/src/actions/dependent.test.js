import mockAxios from 'jest-mock-axios';
import { registerDependent, REGISTER_DEPENDENT } from './dependents'

describe('Dependent Actions', () => {
  afterEach(() => {
    mockAxios.reset();
  });

  it ('should create an action with a promise to call the dependent registration API correctly', () => {
    const expectedEmployeeId = 1;
    const person = { employeeId: expectedEmployeeId, firstName: 'fn', lastName: 'ln' };

    var action = registerDependent(person);

    expect(action).not.toBeNull();
    expect(action.type).toEqual(REGISTER_DEPENDENT);
    expect(action.payload).toBeDefined();
    expect(mockAxios.post).toHaveBeenCalledWith(
      `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees/${expectedEmployeeId}/dependents`,
      person);
  });
});
