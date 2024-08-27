import axios from "axios";

let accessToken = "";
const apiUrl = "https://localhost:7259/api/";
let tokensRefreshFailed:() => void;

export function setTokensRefreshFailedCallBack(callback: () => void){
  tokensRefreshFailed = callback;
}

const instance = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
    //"Access-Control-Allow-Origin": "https://localhost:8084",
  },
});

instance.interceptors.request.use(
  (config) => {
    if (accessToken !== "") {
      config.headers["Authorization"] = accessToken;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

let tryRefresh = false;
instance.interceptors.response.use(
  (res) => {
    return res;
  },
  async (err) => {
    if(tokensRefreshFailed == undefined){
      return Promise.reject("set up refresh failed callback via [setTokensRefreshFailedCallBack] function");
    }

    const originalConfig = err.config;

    if (err.response) {
      // Access Token was expired
      if (err.response.status === 401 && !tryRefresh) {
        tryRefresh = true;
          var refreshResult = await new RequestUtils().sendRequest("auth/refresh-tokens", "POST");

          if(refreshResult != null && refreshResult != undefined){
            accessToken = refreshResult.accessToken;

            instance.defaults.headers.common["Authorization"] = accessToken;
            
            tryRefresh = false;            
            
            return instance(originalConfig);
          }
      }
      else if(err.response.status === 401){
        tokensRefreshFailed();
      }
      else if (err.response.status === 403 && err.response.data) {
        return Promise.reject(err.response.data);
      }      
    }
    return Promise.reject(err);
  }
);

class RequestUtils{
setAccessToken(token: string){
  accessToken = token;
}
clearAccessToken(){
  accessToken = "";
}

async sendRequest(url: string, method: string = "GET", args: object = {}) {
  url = apiUrl + url;

  if (method === "GET") {
    return await instance
      .get(url)
      .then((response) => {
        if (response.data.Result !== undefined && response.data.Result !== 0)
          throw response.data.Message;
        return response.data;
      })
      .catch((ex) => {
        console.log(ex);
        throw ex;
      });
  } else if (method === "DELETE") {
    return await instance
      .delete(url, { data: args })
      .then((response) => {
        if (response.data.Result !== undefined && response.data.Result !== 0)
          throw response.data.Message;
        return response.data;
      })
      .catch((ex) => {
        console.log(ex);
        throw ex;
      });
  }else if (method === "PUT") {
    return await instance
      .put(url, args)
    .then((response) => {
      if (response.data.Result !== undefined && response.data.Result !== 0)
        throw response.data.Message;
      return response.data;
    })
    .catch((ex) => {
      console.log(ex);
      throw ex;
    });
  }

  return await instance
    .post(url, args)
    .then((response) => {
      if (response.data.Result !== undefined && response.data.Result !== 0)
        throw response.data.Message;
      return response.data;
    })
    .catch((ex) => {
      console.log(ex);
      throw ex;
    });
}
}
export default new RequestUtils();