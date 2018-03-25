import { REGISTER_DEPENDENT } from '../actions/dependents'

export default function(state = [], action) {
  switch (action.type) {
    case REGISTER_DEPENDENT:
      return (action.payload.status === 201) ? state.concat(action.payload.data) : state;

    default:
      return state;
  }
}
