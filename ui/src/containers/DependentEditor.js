import React, {Component} from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { registerDependent } from '../actions/dependents';
import PersonEditor from '../components/PersonEditor';

class DependentEditor extends Component {
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
    this.props.registerDependent({
      employeeId: this.props.employeeId,
      firstName: person.firstName,
      lastName: person.lastName
    });
    this.props.actionComplete();
  }

  render() {
    return (
      <PersonEditor
        visible={this.props.visible}
        title="Register Dependent"
        onCancel={this.handleCancel}
        onSubmit={this.handleSubmit}
      />
    );
  }
}

function mapStateToProps(state, ownProps) {
  return Object.assign({}, {
    employeeId: state.employee.id
  }, ownProps);
}

function mapDispatchToProps(dispatch) {
  return bindActionCreators({ registerDependent: registerDependent }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(DependentEditor);
