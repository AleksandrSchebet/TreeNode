import axios, { AxiosResponse } from "axios";
const useRequest = () => {
    axios.interceptors.response.use(
        (response) => {
            return response;
        },
        (error) => {
            if (error?.response && error?.response.status === 500) {
                console.log(error.response)
                return error.response?.data?.message ?? error.response;
            } 
            return error?.response ?? error;
        }
    );

    async function fetch<T>(url: string, postData?: object, params?: object): Promise<T> {
        let response: AxiosResponse<T>;
            axios.defaults.headers.post['Content-Type'] = 'application/json;charset=utf-8';
            axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';
            if (postData) {
                response = await axios.post<T>(url, postData, params);
            }
            else {
                response = await axios.get<T>(url);
            }
            return response.data;
    }
    return { fetch }
}
export default useRequest;
