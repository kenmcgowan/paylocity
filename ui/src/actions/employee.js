import axios from 'axios';
import { raiseError } from './error';

export const REGISTER_EMPLOYEE = 'REGISTER_EMPLOYEE';

export function registerEmployee(person) {
  var url = `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees`;
  var request = axios.post(url, person);

  return (dispatch) => {
    request.catch((error) => {
      dispatch(raiseError());
    });

    dispatch({
      type: REGISTER_EMPLOYEE,
      payload: request
    });
  }
}
