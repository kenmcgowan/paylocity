import axios from 'axios';
import { raiseError } from './error';

export const FETCH_PAY_PERIODS_PREVIEW = 'FETCH_PAY_PERIODS_PREVIEW';

export function fetchPayPeriodsPreview(employeeId) {
  var url = `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees/${employeeId}/payperiods`
  var request = axios.get(url);

  return (dispatch) => {
    request.catch((error) => {
      dispatch(raiseError());
    });

    dispatch({
      type: FETCH_PAY_PERIODS_PREVIEW,
      payload: request
    });
  }
}
