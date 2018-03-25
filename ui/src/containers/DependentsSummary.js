import React, { Component } from 'react';
import { connect } from 'react-redux'
import NumberFormat from 'react-number-format';
import {
  Table,
  TableBody,
  TableRow,
  TableRowColumn
} from 'material-ui/Table';
import { tableColumnLeft, tableColumnCentered } from '../styles';

class DependentsSummary extends Component {
  render() {
    return (
      <div>
        <Table selectable={false} multiSelectable={false}>
          <TableBody displayRowCheckbox={false} stripedRows={false}>
          {
            this.props.dependents.map((dependent) => {
              return (
                <TableRow key={dependent.id.toString()}>
                  <TableRowColumn style={tableColumnLeft}>{dependent.firstName} {dependent.lastName}</TableRowColumn>
                  <TableRowColumn style={tableColumnCentered}>Dependent</TableRowColumn>
                  <TableRowColumn style={tableColumnCentered}></TableRowColumn>
                  <TableRowColumn style={tableColumnCentered}><NumberFormat value={dependent.annualBenefitsCost} displayType="text" thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} prefix={'$'} /></TableRowColumn>
                  <TableRowColumn style={tableColumnLeft} colSpan="2">{dependent.notes}</TableRowColumn>
                  <TableRowColumn style={tableColumnCentered}></TableRowColumn>
                </TableRow>
              );
            })
          }
          </TableBody>
        </Table>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    dependents: state.dependents
  };
}

export default connect(mapStateToProps)(DependentsSummary);
