import mockAxios from 'jest-mock-axios';
import { fetchPayPeriodsPreview, FETCH_PAY_PERIODS_PREVIEW } from './pay-periods'

describe('Pay period Actions', () => {
  afterEach(() => {
    mockAxios.reset();
  });

  it ('should create an action with a promise to call the pay periods API correctly', () => {
    const expectedEmployeeId = 1;

    var action = fetchPayPeriodsPreview(expectedEmployeeId);

    expect(action).not.toBeNull();
    expect(action.type).toEqual(FETCH_PAY_PERIODS_PREVIEW);
    expect(action.payload).toBeDefined();
    expect(mockAxios.get).toHaveBeenCalledWith(
      `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees/${expectedEmployeeId}/payperiods`);
  });
});
