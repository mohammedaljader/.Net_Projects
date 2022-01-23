import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-property-pane';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';

import * as strings from 'InschrijvenWebPartStrings';
import { Inschrijven } from './components/Inschrijven';
import { IInschrijvenProps } from './components/IInschrijvenProps';
import { sp } from "@pnp/sp";
import "@pnp/sp/webs";

export interface IInschrijvenWebPartProps {
  description: string;
  siteurl: string;
  listurl: string;
}

export default class InschrijvenWebPart extends BaseClientSideWebPart<IInschrijvenWebPartProps> {

  public onInit(): Promise<void> {

    return super.onInit().then(_ => {
  
      // other init code may be present
  
      sp.setup({
        spfxContext: this.context
      });
    });
  }

  public render(): void {
    const element: React.ReactElement<IInschrijvenProps> = React.createElement(
      Inschrijven,
      {
        description: this.properties.description,
        siteurl: this.properties.siteurl,
        listurl: this.properties.listurl
      }
    );

    ReactDom.render(element, this.domElement);
  }

  protected onDispose(): void {
    ReactDom.unmountComponentAtNode(this.domElement);
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: strings.PropertyPaneDescription
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('description', {
                  label: strings.DescriptionFieldLabel
                }),
                PropertyPaneTextField('siteurl', {
                  label: 'siteurl'
                }),
                PropertyPaneTextField('listurl', {
                  label: 'listurl'
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
