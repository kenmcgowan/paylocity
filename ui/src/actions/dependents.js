import axios from 'axios';

export const REGISTER_DEPENDENT = 'REGISTER_DEPENDENT';

export function registerDependent(person) {
  var url = `${process.env.REACT_APP_REGISTRATION_API_BASE_URL}/employees/${person.employeeId}/dependents`;
  var request = axios.post(url, person);

  return {
    type: REGISTER_DEPENDENT,
    payload: request
  }
}
