import axios from 'axios';

export const REGISTER_EMPLOYEE = 'REGISTER_EMPLOYEE';

export function registerEmployee(person) {
  var url = `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees`;
  var request = axios.post(url, person);

  return {
    type: REGISTER_EMPLOYEE,
    payload: request
  }
}
