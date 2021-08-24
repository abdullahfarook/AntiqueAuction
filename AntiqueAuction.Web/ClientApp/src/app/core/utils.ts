import { ServerError } from "./models/error";

export class ParseUtil {
    private static DefaultError:ServerError = {
      code:500,
      description:"Something went wrong",
    };
    public static error(error:any):ServerError{
        try {
            const result =  JSON.parse(error?.response,this.toCamelCase);
            return result
        } catch (error) {
          return this.DefaultError; 
        }
        
    }
    static toCamelCase(key:any, value:any) {
        if (value && typeof value === 'object'){
          for (var k in value) {
            if (/^[A-Z]/.test(k) && Object.hasOwnProperty.call(value, k)) {
              value[k.charAt(0).toLowerCase() + k.substring(1)] = value[k];
              delete value[k];
            }
          }
        }
        return value;
      }
}