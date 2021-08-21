export class PageResult<T> {
    data: T[];
    pageInfo!: PageInfo;
    totalCount!: number;
    error: any | undefined;
  
    constructor(response?: any) {
      if  (response)  {
        if (response.result) {
          this.data = response.result;
          this.pageInfo = new PageInfo(response.pageInfo);
          this.totalCount = response['total-count'];
        } else {
          this.data = response;
        }
      }  else {
        this.data = [];
        this.pageInfo = new PageInfo();
      }
    }
  }
export class PageInfo {
    pageIndex: string | undefined;
    pageSize: string | undefined;
    constructor(info?: any) {
      if  (info)  {
        this.pageIndex = info.pageIndex;
        this.pageSize = info.pageSize;
        }
    }
  }
  