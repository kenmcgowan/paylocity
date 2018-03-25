import payPeriodsReducer from './pay-periods'
import { FETCH_PAY_PERIODS_PREVIEW } from '../actions/pay-periods'

describe('Pay Periods Reducer', () => {
  it ('should return an empty array for the initial state', () => {
    const suppliedState = undefined;
    const action = { type: '@@redux/INIT' };
    const expectedState = [];

    var result = payPeriodsReducer(suppliedState, action);

    expect(result).toEqual(expectedState);
  });

  it ('should return the correct list of pay periods', () => {
    const initialState = [];
    const expectedPayPeriods = {
      payPeriods: [
        { Number: 1 },
        { Number: 2 },
        { Number: 3 },
      ]
    };

    const action = {
      type: FETCH_PAY_PERIODS_PREVIEW,
      payload: {
        status: 200,
        data: expectedPayPeriods
      }
    };

    var result = payPeriodsReducer(initialState, action);

    expect(result).toEqual(expectedPayPeriods.payPeriods);
  });


  it ('should not modify the existing state when the result is not successful', () => {
    const initialState = [];
    const payPeriods = { payPeriods: [] };
    const action = {
      type: FETCH_PAY_PERIODS_PREVIEW,
      payload: {
        status: 0, // Doesn't matter so long as it's not 200
        data: payPeriods
      }
    };

    var result = payPeriodsReducer(initialState, action);

    expect(result).toEqual(initialState);
  });
});
