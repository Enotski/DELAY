export const apiUrl = "https://localhost:7259/api/";
import axios from "axios";

const instance = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
    //"Access-Control-Allow-Origin": "https://localhost:8084",
  },
});

export async function sendRequest(url: string, method: string = "GET", args: object = {}) {
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
