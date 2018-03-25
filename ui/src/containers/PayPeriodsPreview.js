import React, { Component } from 'react';
import { connect } from 'react-redux'
import NumberFormat from 'react-number-format';
import {
  Table,
  TableBody,
  TableHeader,
  TableHeaderColumn,
  TableRow,
  TableRowColumn
} from 'material-ui/Table';
import Paper from 'material-ui/Paper';
import { tableColumnCentered, reportContainer } from '../styles';

class PayPeriodsPreview extends Component {
  render() {
    return (
      <Paper zDepth={3} style={ reportContainer }>
        <Table selectable={false} multiSelectable={false}>
          <TableHeader displaySelectAll={false} adjustForCheckbox={false}>
            <TableRow>
              <TableHeaderColumn colSpan={4} style={tableColumnCentered}>Pay Period Preview</TableHeaderColumn>
            </TableRow>
            <TableRow>
              <TableHeaderColumn style={tableColumnCentered}>Pay Period</TableHeaderColumn>
              <TableHeaderColumn style={tableColumnCentered}>Gross Pay</TableHeaderColumn>
              <TableHeaderColumn style={tableColumnCentered}>Deductions</TableHeaderColumn>
              <TableHeaderColumn style={tableColumnCentered}>Net Pay</TableHeaderColumn>
            </TableRow>
          </TableHeader>
          <TableBody displayRowCheckbox={false} stripedRows={true}>
            {
              this.props.payPeriods.map((payPeriod) => {
                return (
                  <TableRow key={payPeriod.number}>
                    <TableRowColumn style={tableColumnCentered}>{payPeriod.number}</TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}><NumberFormat value={payPeriod.grossPay} displayType="text" thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} prefix={"$"} /></TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}><NumberFormat value={payPeriod.deductions} displayType="text" thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} prefix={"$"} /></TableRowColumn>
                    <TableRowColumn style={tableColumnCentered}><NumberFormat value={payPeriod.netPay} displayType="text" thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} prefix={"$"} /></TableRowColumn>
                  </TableRow>
                );
              })
            }
          </TableBody>
        </Table>
      </Paper>
    )
  }
}

function mapStateToProps(state) {
  return Object.assign({}, {
    payPeriods: state.payPeriods
  });
}

export default connect(mapStateToProps)(PayPeriodsPreview);
