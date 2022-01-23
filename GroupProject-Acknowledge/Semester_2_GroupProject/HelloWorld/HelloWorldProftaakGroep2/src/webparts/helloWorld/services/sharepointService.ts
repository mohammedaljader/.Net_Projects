
import { sp } from "@pnp/sp";
import "@pnp/sp/webs";
import "@pnp/sp/lists";
import "@pnp/sp/items";
import { IItem, IItemAddResult } from "@pnp/sp/items";

export class SharePointService {
  public static async getListItems(siteurl: string, listurl:string): Promise<IItem[]> {

    let _url: string = `${siteurl}/${listurl}`;

    console.debug('url get list', _url);

    const _items: IItem[] = await sp.web.getList(_url).items
      .select('Title', 'Hoeveelheid')
      .orderBy('Title')
      .expand('TestPerson')
      .top(5000) //otherwise max. 100 items are returned
      .get();

      console.debug('items received');

    return _items;
  }

  public static async AddListItem(siteurl: string, listurl: string, title: string, hoeveelheid: number): Promise<IItemAddResult>{
    let _body : any = {
      Title: title,
      Hoeveelheid: hoeveelheid
    };
    let _url: string = `${siteurl}/${listurl}`;
    const _item : IItemAddResult = await sp.web.getList(_url).items
    .add(_body);
    return _item;
  }
}
