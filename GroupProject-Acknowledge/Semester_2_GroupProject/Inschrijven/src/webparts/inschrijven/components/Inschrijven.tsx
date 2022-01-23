import * as React from 'react';
//import './Style.css';
import styles from './Inschrijven.module.scss';
import { IInschrijvenProps } from './IInschrijvenProps';
import { IInschrijvenState } from './IInschrijvenState';
import { InschijfWeek } from './Inschrijfweek';
import { SharePointService} from '../services/sharepointService';



export class Inschrijven extends React.Component<IInschrijvenProps, IInschrijvenState>{

  constructor(props: IInschrijvenProps) {
    super(props);

    this.state = {
    };
  }

  public render(): React.ReactElement<IInschrijvenProps> {
    return (
    <>
    <InschijfWeek opslaanInschrijving = {this.AddListItem.bind(this)}/>
    </>
    );
  }

  private async AddListItem(body): Promise<void>{
    let _item = await SharePointService.AddListItem(this.props.siteurl, this.props.listurl, body);
  }
}
