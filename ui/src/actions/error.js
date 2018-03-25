export const ERROR_RAISED = 'ERROR_RAISED';
export function raiseError() {
  return {
    type: ERROR_RAISED
  };
}

export const ERROR_ACKNOWLEDGED = 'ERROR_ACKNOWLEDGED';
export function acknowledgeError() {
  return {
    type: ERROR_ACKNOWLEDGED
  };
}
