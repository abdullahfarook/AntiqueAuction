import { ServerError } from "./models/error";

export class ParseUtil {
    public static error(error:any):ServerError|null{
        var response = error.response;
        if(!response) return null;
        try {
            const result =  JSON.parse(response,this.toCamelCase);
            return result
        } catch (error) {
            return null;
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