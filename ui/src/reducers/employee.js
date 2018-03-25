import { REGISTER_EMPLOYEE } from '../actions/employee'

export default function(state = {}, action) {
  switch (action.type) {
    case REGISTER_EMPLOYEE:
      return (action.payload.status === 201) ? action.payload.data : state;

    default:
      return state;
  }
}
