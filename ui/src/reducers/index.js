import { combineReducers } from 'redux';
import EmployeeReducer from './employee';
import DependentsReducer from './dependents';
import PayPeriodsReducer from './pay-periods'
import ErrorReducer from './error';

const rootReducer = combineReducers({
  employee: EmployeeReducer,
  dependents: DependentsReducer,
  payPeriods: PayPeriodsReducer,
  error: ErrorReducer
});

export default rootReducer;
