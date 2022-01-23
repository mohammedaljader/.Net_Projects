import { IItem } from "@pnp/sp/items";

export interface IHelloWorldState {
  textValue: string;
  numberValue: number;
  items: IItem[];
}
