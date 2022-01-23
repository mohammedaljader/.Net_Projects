import { sp } from "@pnp/sp";
import "@pnp/sp/webs";
import "@pnp/sp/lists";
import "@pnp/sp/items";
import { IItem, IItemAddResult } from "@pnp/sp/items";

export class SharePointService {
    public static async AddListItem(siteurl: string, listurl: string, body : any): Promise<IItemAddResult>{
    let _body : any = {
      Title: body.updateweek,
      Maandag: body.maandag,
      Dinsdag: body.dinsdag,
      Woensdag: body.woensdag,
      Donderdag: body.donderdag,
      Zaterdag: body.zaterdag,
      MMC: body.mmc,
      Afgemeld: body.afmelden,
      Opmerkingen: body.opmerking
    };
    let _url: string = `${siteurl}/${listurl}`;
    const _item : IItemAddResult = await sp.web.getList(_url).items
  
    .add(_body);
    return _item;
  }
}
