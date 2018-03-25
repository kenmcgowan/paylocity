import React, {Component} from 'react';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import TextField from 'material-ui/TextField';
import { inputContainer } from '../styles'

export default class PersonEditor extends Component {
  static defaultProps = {
    visible: false,
    title: '',
    onCancel: () => {},
    onSubmit: () => {}
  };

  constructor(props) {
    super(props);

    this.state = {
      firstName: '',
      lastName: ''
    };
  }

handleFirstNameChange = (event, newValue) => {
  this.setState(Object.assign({}, this.state, { firstName: newValue }));
}

handleLastNameChange = (event, newValue) => {
  this.setState(Object.assign({}, this.state, { lastName: newValue }));
}

handleCancel = (event) => {
  this.props.onCancel();
  this.resetFieldState();
};

handleSubmit = (event) => {
  this.props.onSubmit({
    firstName: this.state.firstName,
    lastName: this.state.lastName
  });
  this.resetFieldState();
}

resetFieldState() {
  this.setState(Object.assign({}, this.state, {
    firstName: '',
    lastName: ''
  }));
}

  render() {
    const dialogActions = [
      <FlatButton label="Cancel" primary={true} onClick={this.handleCancel} />,
      <FlatButton label="Submit" primary={true} onClick={this.handleSubmit} />
    ];

    return (
      <div style={ { width: "350px" } }>
        <Dialog
          contentStyle={ { maxWidth: "400px" } }
          title={this.props.title}
          actions={dialogActions}
          modal={false}
          open={this.props.visible}
          onRequestClose={this.handleCancel}
        >
          <div style={inputContainer}>
            <TextField floatingLabelText="First Name" value={this.state.firstName} fullWidth={true} onChange={this.handleFirstNameChange} />
            <TextField floatingLabelText="Last Name" value={this.state.lastName} fullWidth={true} onChange={this.handleLastNameChange} />
          </div>
        </Dialog>
      </div>
    );
  }
}
