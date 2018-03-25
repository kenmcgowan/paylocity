import { raiseError, acknowledgeError, ERROR_RAISED, ERROR_ACKNOWLEDGED } from './error'

describe('Employee Actions', () => {
  it('should return the correct action when raising an error', () => {
    const action = raiseError();
    expect(action).not.toBeNull();
    expect(action.type).toEqual(ERROR_RAISED);
  });

  it('should return the correct action when acknowledging an error', () => {
    const action = acknowledgeError();
    expect(action).not.toBeNull();
    expect(action.type).toEqual(ERROR_ACKNOWLEDGED);
  });
});
