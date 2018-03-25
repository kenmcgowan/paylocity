import axios from 'axios';

export const FETCH_PAY_PERIODS_PREVIEW = 'FETCH_PAY_PERIODS_PREVIEW';

export function fetchPayPeriodsPreview(employeeId) {
  var url = `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees/${employeeId}/payperiods`
  var request = axios.get(url);

  return {
    type: FETCH_PAY_PERIODS_PREVIEW,
    payload: request
  }
}
