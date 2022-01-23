import * as React from 'react';
import styles from './HelloWorld.module.scss';
import { IHelloWorldProps } from './IHelloWorldProps';
import { IHelloWorldState } from './IHelloWorldState';
import { escape } from '@microsoft/sp-lodash-subset';

import { TextField, PrimaryButton, DetailsList, IColumn } from 'office-ui-fabric-react';

import { IItem } from "@pnp/sp/items";

import { SharePointService} from '../services/sharepointService';

export default class HelloWorld extends React.Component<IHelloWorldProps, IHelloWorldState> {

  // #region React methods
  constructor(props: IHelloWorldProps) {
    super(props);

    this.state = {
      textValue: "",
      numberValue: null,
      items: []
    };
  }

  public componentDidMount(): void {
    // using React Hooks, componentDidMount is not available anymore

    // check the developer console for sharepoint items / interface to list the items from sharepoint to be implemented
    this.getSharepointItems();
  }

  public componentDidUpdate(prevProps: IHelloWorldProps, prevState: IHelloWorldState): void {
    // every update to state, this component will be updated and re-rendered
    console.debug('componentDidUpdate', this.state);
  }

  public render(): React.ReactElement<IHelloWorldProps> {
    let listItemsTitle: JSX.Element = this.getDetailsList();

    return (
      <div className={ styles.helloWorld }>
        <div className={ styles.container }>
          <div className={ styles.row }>
            <div className={ styles.column }>
              <span className={ styles.title }>Welcome to SharePoint!</span>
              <p className={ styles.subTitle }>Customize SharePoint experiences using Web Parts.</p>

              <p className={ styles.description }>{escape(this.props.description)}</p>
              <a href="https://aka.ms/spfx" className={ styles.button }>
                <span className={ styles.label }>Learn more</span>
              </a>

              <p>
              <TextField label ="Voer titel in" onChange={(event: any, value:string) => this.updateTextValue(value)} />
              <TextField label ="Voer hoeveelheid in" onChange={(event: any, number:string) => this.updateNumberValue(number)} />
              <PrimaryButton onClick={(Event) =>this.AddListItem()}
              text = 'Voeg Toe' />

              <div>{this.state.textValue}</div>

              <h1>List items</h1>
              <div>{listItemsTitle}</div>
              </p>


            </div>
          </div>
        </div>
      </div>
    );
  }
  // #endregion React methods

  private updateTextValue(value: string /*numbervalue */): void {
    this.setState({
      textValue: value
    });
  }
  //nieuwe method om 2e kolomn te updaten
  private updateNumberValue(value: string): void {
    var temp : number= parseInt(value);
    this.setState({
      numberValue: temp
    });
  }

  private async AddListItem(): Promise<void>{
    let _item = await SharePointService.AddListItem(this.props.siteurl, this.props.listurl, this.state.textValue, this.state.numberValue);
    this.getSharepointItems();
  }

  private async getSharepointItems(): Promise<void> {
    let _items: IItem[] = await SharePointService.getListItems(this.props.siteurl, this.props.listurl);

    this.setState({
      items: _items
    });
  }

  //Inhoud list
  private getDetailsList(): JSX.Element {

    let _itemsTitle: any[] = this.state.items.map(item => {
      let obj: any = {
        'title': item["Title"],
        'hoeveelheid' : item["Hoeveelheid"]
      };

      return obj;
    });

    /*let _itemsHoeveelheid: any[] = this.state.items.map(item => {
      let obj: any = {
        'hoeveelheid': item["Hoeveelheid"],
      };

      return obj;
    });*/

    console.debug('itemsTitle', _itemsTitle /*'itemsHoeveelheid', _itemsHoeveelheid*/);

    const _columns: IColumn[] = [
      {
        key: 'column1' ,
        name: 'Titel',
        fieldName: 'title',
        minWidth: 100,
        isSorted: true,
        isSortedDescending: false,
      },
      {
        key:  'column2',
        name: 'Hoeveelheid',
        fieldName: 'hoeveelheid',
        minWidth: 100,
      }

    ];

    let _detailsList: JSX.Element = <DetailsList
      items={_itemsTitle}
      columns={_columns}
      isHeaderVisible={true}
    />;

    return _detailsList;
  }

//#endregion customfunctions

}
