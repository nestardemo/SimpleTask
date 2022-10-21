import { Guid } from "guid-typescript";

export interface User {
    userId?: Guid;
    login: string;
    provinceId: string;
    password : string;
}
