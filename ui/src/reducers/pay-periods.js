import { FETCH_PAY_PERIODS_PREVIEW } from '../actions/pay-periods'

export default function(state = [], action) {
  switch (action.type) {
    case FETCH_PAY_PERIODS_PREVIEW:
      return (action.payload.status === 200) ? action.payload.data.payPeriods : state;

    default:
      return state;
  }
}
