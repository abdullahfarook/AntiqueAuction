import {
  Observable,
  throwError as _observableThrow,
  of as _observableOf,
} from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BaseService {

  private apiUrl: string;
  private token: string | undefined;
  constructor() {
    this.apiUrl = environment.apiUrl;
  }
  setToken(token: string) {
    token = token;
  }
  getBaseUrl(arg: string = ''): string {
    return this.apiUrl;
  }
  transformOptions(options: { headers: HttpHeaders; }): Promise<any> {
    return new Promise((res, rej) => {
      // const token = this.token;
      // if (token) {
      //   let headers: HttpHeaders = options.headers;
      //   headers = headers.append('Authorization', 'Bearer ' + token);
      //   options.headers = headers;
      // }
      res(options);
    });
  }
  public toPaginatedUrl(url:string){
    if(!url.includes('?'))
      url +='?'
    return `${url}&wrapwith=total-count`
  }
}

export class SwaggerException extends Error {
  message: string;
  status: number;
  response: string;
  headers: { [key: string]: any };
  result: any;

  constructor(
    message: string,
    status: number,
    response: string,
    headers: { [key: string]: any },
    result: any
  ) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isSwaggerException = true;

  static isSwaggerException(obj: any): obj is SwaggerException {
    return obj.isSwaggerException === true;
  }
}
