import React, {Component} from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import PersonEditor from '../components/PersonEditor';
import { registerEmployee } from '../actions/employee';

class EmployeeEditor extends Component {
  static defaultProps = {
    visible: false,
    actionComplete: () => {}
  };

  constructor(props) {
    super(props);

    this.handleCancel = this.handleCancel.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleCancel() {
    this.props.actionComplete();
  }

  handleSubmit(person) {
    this.props.registerEmployee({
      firstName: person.firstName,
      lastName: person.lastName
    });
    this.props.actionComplete();
  }

  render() {
    return (
      <PersonEditor
        visible={this.props.visible}
        title="Register Employee"
        onCancel={this.handleCancel}
        onSubmit={this.handleSubmit}
      />
    );
  }
}

function mapDispatchToProps(dispatch) {
  return bindActionCreators({ registerEmployee: registerEmployee }, dispatch)
}

export default connect(null, mapDispatchToProps)(EmployeeEditor);
