import { combineReducers } from 'redux';
import EmployeeReducer from './employee';
import DependentsReducer from './dependents';
import PayPeriodsReducer from './pay-periods'

const rootReducer = combineReducers({
  employee: EmployeeReducer,
  dependents: DependentsReducer,
  payPeriods: PayPeriodsReducer,
});

export default rootReducer;
