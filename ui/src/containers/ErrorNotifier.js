import React, { Component } from 'react'
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import Snackbar from 'material-ui/Snackbar';
import { acknowledgeError } from '../actions/error';

class ErrorNotifier extends Component {
  constructor(props) {
    super(props);

    this.handleRequestClose = this.handleRequestClose.bind(this);
  }

  handleRequestClose() {
    this.props.acknowledgeError();
  }

  render() {
    return (
      <Snackbar
        open={this.props.visible}
        message="Oh dear, that wasn't supposed to happen. Maybe check the logs? Please stand by…"
        autoHideDuration={5000}
        onRequestClose={this.handleRequestClose}
        bodyStyle={ { backgroundColor: "rgb(144,0,0)" } }
        />
    );
  }
}

function mapStateToProps(state) {
  return {
    visible: !state.error.acknowledged,
  };
}

function mapDispatchToProps(dispatch) {
  return bindActionCreators({
    acknowledgeError: acknowledgeError
  }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ErrorNotifier);
