export interface ServerError {
    code?:number | undefined;
    description?:string | undefined;
    message?:string | undefined;
    traceId?:string | undefined;
}