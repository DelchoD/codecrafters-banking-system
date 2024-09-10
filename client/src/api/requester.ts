import { RequestOptions } from "../types/RequestOptions";


function requester(method: string, url: string, data: any): Promise<any> {

    let options: RequestOptions = {};

    if (method == 'POST' || method == 'PUT') {

        options.headers = {
            'content-type': 'application/json'
        };

        if (data) {
            options.body = JSON.stringify(data);
        }
    }

    return fetch(url, options)
        .then(res => res.json());
}


const get = requester.bind('GET');
const post = requester.bind('POST');
const put = requester.bind('PUT');
const del = requester.bind('DELETE');


export default {
    get,
    post,
    put,
    del
}