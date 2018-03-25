import axios from 'axios';
import { raiseError } from './error';

export const REGISTER_DEPENDENT = 'REGISTER_DEPENDENT';

export function registerDependent(person) {
  var url = `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees/${person.employeeId}/dependents`;
  var request = axios.post(url, person);

  return (dispatch) => {
    request.catch((error) => {
      dispatch(raiseError());
    });

    dispatch({
      type: REGISTER_DEPENDENT,
      payload: request
    });
  }
}
