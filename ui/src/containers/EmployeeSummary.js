import React, { Component } from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';
import NumberFormat from 'react-number-format';
import {
  Table,
  TableBody,
  TableHeader,
  TableHeaderColumn,
  TableRow,
  TableRowColumn
} from 'material-ui/Table';
import FlatButton from 'material-ui/FlatButton';
import RaisedButton from 'material-ui/RaisedButton';
import EmployeeEditor from './EmployeeEditor';
import DependentEditor from './DependentEditor';
import DependentsSummary from './DependentsSummary';
import PayPeriodsPreview from './PayPeriodsPreview';
import { fetchPayPeriodsPreview } from '../actions/pay-periods';
import { horizontallyCenteredColumnContainer, tableColumnLeft, tableColumnCentered } from '../styles';

class EmployeeSummary extends Component {
  constructor(props) {
    super(props);

    this.state = {
      employeeDialogVisible: false,
      dependentDialogVisible: false,
      payPeriodsPreviewVisible: false,
    };

    this.handleRegisterEmployeeClick = this.handleRegisterEmployeeClick.bind(this);
    this.handleAddDependentClick = this.handleAddDependentClick.bind(this);
    this.handleEmployeeDialogClosed = this.handleEmployeeDialogClosed.bind(this);
    this.handleDependentDialogClosed = this.handleDependentDialogClosed.bind(this);
    this.handlePreviewPayPeriodsClick = this.handlePreviewPayPeriodsClick.bind(this);
  }

  handleRegisterEmployeeClick() {
    this.setState(Object.assign({}, this.state, {
      employeeDialogVisible: true,
      payPeriodsPreviewVisible: false,
    }));
  }

  handleAddDependentClick() {
    this.setState(Object.assign({}, this.state, {
      dependentDialogVisible: true,
      payPeriodsPreviewVisible: false,
    }));
  }

  handleEmployeeDialogClosed() {
    this.setState(Object.assign({}, this.state, {
      employeeDialogVisible: false,
    }));
  }

  handleDependentDialogClosed() {
    this.setState(Object.assign({}, this.state, {
      dependentDialogVisible: false,
    }));
  }

  handlePreviewPayPeriodsClick(event) {
    this.props.fetchPayPeriodsPreview(this.props.employee.id);
    this.setState(Object.assign({}, this.state, {
      payPeriodsPreviewVisible: true,
    }));
  }

  render() {
    return (
      <div>
        <EmployeeEditor visible={this.state.employeeDialogVisible} actionComplete={this.handleEmployeeDialogClosed} />
        <DependentEditor visible={this.state.dependentDialogVisible} actionComplete={this.handleDependentDialogClosed} />
        {
          (this.props.employee.id == null) ? (
            <RaisedButton secondary={true} label="Register Employee" onClick={this.handleRegisterEmployeeClick}/>
          ) : (
            <div>
              <Table selectable={false} multiSelectable={false}>
                <TableHeader displaySelectAll={false} adjustForCheckbox={false}>
                  <TableRow>
                    <TableHeaderColumn style={tableColumnLeft}>Name</TableHeaderColumn>
                    <TableHeaderColumn style={tableColumnCentered}>Registrant Type</TableHeaderColumn>
                    <TableHeaderColumn style={tableColumnCentered}>Annual Salary</TableHeaderColumn>
                    <TableHeaderColumn style={tableColumnCentered}>Annual Benefits Cost</TableHeaderColumn>
                    <TableHeaderColumn style={tableColumnLeft} colSpan="2">Notes</TableHeaderColumn>
                    <TableHeaderColumn style={tableColumnCentered} />
                  </TableRow>
                </TableHeader>
                <TableBody displayRowCheckbox={false} stripedRows={true}>
                  <TableRow>
                    <TableRowColumn style={tableColumnLeft}>{this.props.employee.firstName} {this.props.employee.lastName}</TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}>Employee</TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}><NumberFormat value={this.props.employee.annualSalary} displayType="text" thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} prefix={"$"} /></TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}><NumberFormat value={this.props.employee.annualBenefitsCost} displayType="text" thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} prefix={"$"} /></TableRowColumn>
                    <TableRowColumn style={tableColumnLeft} colSpan="2">{this.props.employee.notes}</TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}><FlatButton label="Add Dependent" onClick={this.handleAddDependentClick} secondary={true} labelStyle={ { fontSize: "85%" } } /></TableRowColumn>
                  </TableRow>
                </TableBody>
              </Table>

              <DependentsSummary />

              <div style={horizontallyCenteredColumnContainer}>
              {
                (this.state.payPeriodsPreviewVisible) ? (
                  <PayPeriodsPreview />
                ) : (
                  <RaisedButton label="Preview Pay Periods" primary={true} onClick={this.handlePreviewPayPeriodsClick} style={ { marginTop: "20px" } } />
                )
              }
              </div>
            </div>
          )
        }
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    employee: state.employee
  };
}

function mapDispatchToProps(dispatch) {
  return bindActionCreators(
    {
      fetchPayPeriodsPreview: fetchPayPeriodsPreview,
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EmployeeSummary);
