import { ERROR_RAISED, ERROR_ACKNOWLEDGED } from '../actions/error'

export default function(state = { acknowledged: true }, action) {
  switch (action.type) {
    case ERROR_RAISED:
      return { acknowledged: false };

    case ERROR_ACKNOWLEDGED:
      return { acknowledged: true };

    default:
      return state;
  }
}
