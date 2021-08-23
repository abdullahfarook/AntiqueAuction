export class PageResult<T> {
    data: T[];
    pageInfo!: PageInfo;
    totalCount!: number;
    error: any | undefined;
  
    constructor(response?: any,page:PageInfo = new PageInfo({pageIndex:1,pageSize:10})) {
      if  (response)  {
        if (response.result) {
          this.data = response.result;
          this.pageInfo = new PageInfo(page);
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
    pageIndex!: number;
    pageSize!: number;
    constructor(info?: any) {
      if  (info)  {
        this.pageIndex = info.pageIndex;
        this.pageSize = info.pageSize;
        }
    }
  }
  