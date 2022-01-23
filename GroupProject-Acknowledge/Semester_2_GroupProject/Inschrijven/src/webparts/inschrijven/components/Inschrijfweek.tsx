import * as React from 'react';
import {Component} from 'react';
import { TextField, PrimaryButton, DetailsList, IColumn, Checkbox, labelProperties } from 'office-ui-fabric-react';
import styles from './Inschrijven.module.scss';

export interface IInschrijfweekState{
  updateweek : string;
  maandag : boolean;
  dinsdag : boolean;
  woensdag : boolean;
  donderdag : boolean;
  zaterdag : boolean;
  mmc : boolean;
  afmelden : boolean;
  opmerking : string;
}

export interface IInschrijfweekProps{
  opslaanInschrijving : (body : any) => void;
}


export class InschijfWeek extends React.Component<IInschrijfweekProps, IInschrijfweekState> {

  constructor(props : IInschrijfweekProps){
    super(props);

    this.state = {
      updateweek : "",
      maandag : false,
      dinsdag : false,
      woensdag : false,
      donderdag : false,
      zaterdag : false,
      mmc : false,
      afmelden : false,
      opmerking : ""
    };
  }

  //krijgen textfield niet via onchange method dus aparte method gebruikt
  private updateTextValue(value: string): void {
    this.setState({
      opmerking: value
    });
  }

  private onchange = (waarde : any, veld : string) =>{

    let body : any = {};
    body[veld] = waarde;
    this.setState(body);

    if(body.afmelden === true){
      body.maandag = false;
      body.dinsdag = false;
      body.woensdag = false;
      body.donderdag = false;
      body.mmc = false;
      body.zaterdag = false;
    }
  }

  private submit =  (): void =>{
    console.log(this.state.updateweek + " " + this.state.maandag + " " + this.state.dinsdag + " " + this.state.woensdag + " " +this.state.donderdag + " " + this.state.zaterdag + " " + this.state.mmc + " " +  this.state.afmelden + " "+ this.state.opmerking );
    let body : any = {
      updateweek : this.state.updateweek,
      maandag : this.state.maandag,
      dinsdag : this.state.dinsdag,
      woensdag : this.state.woensdag,
      donderdag : this.state.donderdag,
      zaterdag : this.state.zaterdag,
      mmc : this.state.mmc,
      afmelden : this.state.afmelden,
      opmerking : this.state.opmerking
    };
    if(body.afmelden === true && body.opmerking === ""){
       console.debug("geen opmerking");
       //textfield onErrorMessage
    }
    else
    {
      this.props.opslaanInschrijving(body);
    } 
  }

  public render(){
    return(
      <div>
        <Checkbox label = "maandag" onChange = {(event, checked) => this.onchange(checked, 'maandag')}/>
        <Checkbox label = "dinsdag" onChange = {(event, checked) => this.onchange(checked, 'dinsdag')}/>
        <Checkbox label = "woensdag" onChange = {(event, checked) => this.onchange(checked, 'woensdag')}/>
        <Checkbox label = "donderdag" onChange = {(event, checked) => this.onchange(checked, 'donderdag')}/>
        <Checkbox label = "zaterdag" onChange = {(event, checked) => this.onchange(checked, 'zaterdag')}/>
        <Checkbox label = "mmc" onChange = {(event, checked) => this.onchange(checked, 'mmc')}/>
        <Checkbox label = "afmelden" onChange = {(event, checked) => this.onchange(checked, 'afmelden')}/>
        <TextField label ="opmerking" required={this.state.afmelden} onChange={(event: any, value:string) => this.updateTextValue(value)} />
        <PrimaryButton text = "submit" onClick={(Event) =>this.submit()}/>
      </div>
    );
  }
}