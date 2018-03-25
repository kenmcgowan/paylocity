import React, { Component } from 'react';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import Drawer from 'material-ui/Drawer';
import MenuItem from 'material-ui/MenuItem';
import EmployeeSummary from './containers/EmployeeSummary';
import './App.css';
import {fade} from 'material-ui/utils/colorManipulator';
import {
  blueGrey500,
  blueGrey700,
  grey400,
  orangeA200,
  grey100,
  grey500,
  darkBlack,
  white,
  grey300,
  fullBlack
} from 'material-ui/styles/colors'

const muiTheme = getMuiTheme({
  palette: {
    primary1Color: blueGrey500,
    primary2Color: blueGrey700,
    primary3Color: grey400,
    accent1Color: orangeA200,
    accent2Color: grey100,
    accent3Color: grey500,
    textColor: darkBlack,
    alternateTextColor: white,
    canvasColor: white,
    borderColor: grey300,
    disabledColor: fade(darkBlack, 0.3),
    pickerHeaderColor: blueGrey500,
    clockCircleColor: fade(darkBlack, 0.07),
    shadowColor: fullBlack
  }
});

export default class App extends Component {
  constructor() {
    super();

    this.state = { drawerOpen: false };

    this.handleAppBarLeftIconClick = this.handleAppBarLeftIconClick.bind(this);
  }

  handleAppBarLeftIconClick() {
    this.setState( {
      drawerOpen: !this.state.drawerOpen
    });
  }

  render() {
    return (
      <MuiThemeProvider muiTheme={muiTheme}>
        <div style={ { display: "flex", flexDirection: "column", justifyContent: "flex-start", alignItems: "center" } }>
          <AppBar title="Paylocity Deductions Calculator" onLeftIconButtonClick={this.handleAppBarLeftIconClick} />
          <Drawer docked={false} width={200} open={this.state.drawerOpen} onRequestChange={(drawerOpen) => this.setState({ drawerOpen })}>
            <MenuItem onClick={() => window.location.reload()}>Reset App</MenuItem>
          </Drawer>
            <div style={ { marginTop: "60px" } }>
              <EmployeeSummary />
            </div>
        </div>
      </MuiThemeProvider>
    );
  }
}
