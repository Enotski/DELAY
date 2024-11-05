import axios from "axios";

let accessToken = "";
export const apiUrl = import.meta.env.VITE_API_URI;
let tokensRefreshSuccess:(response: any) => void;
let tokensRefreshFailed:() => void;

export function setTokensRefreshSuccessCallBack(callback: (response: any) => void){
  tokensRefreshSuccess = callback;
}
export function setTokensRefreshFailedCallBack(callback: () => void){
  tokensRefreshFailed = callback;
}

export function setAccessToken(token: string){
  accessToken = token;
}
export function clearAccessToken(){
  accessToken = "";
}
export function getAccessToken() : string{
  return accessToken;
}

export function parseJwt (token: string) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

const instance = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "https://localhost:7259",
  },
});

instance.interceptors.request.use(
  (config) => {
    config.withCredentials = true;

    if (accessToken !== "") {
      config.headers["Authorization"] = "Bearer " + accessToken;
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
            
            if(tokensRefreshSuccess !== undefined && tokensRefreshSuccess !== null)
            tokensRefreshSuccess(parseJwt(accessToken));

            return instance(originalConfig);
          }
      }
      else if(err.response.status === 401){
        if(tokensRefreshFailed !== undefined && tokensRefreshFailed !== null)
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

async sendRequest<T>(url: string, method: string = "GET", args?: T) {
  url = apiUrl + url;

  if (method === "GET") {
    return await instance
      .get(url, {
        params: args
      })
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
  }else if (method === "PATCH") {
    return await instance
      .patch(url, args)
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