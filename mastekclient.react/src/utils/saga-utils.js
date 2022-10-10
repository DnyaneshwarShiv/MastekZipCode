import axios from "axios"

const getReq = async (url, params) => {
        return await axios.get(url)
            .then((response) => response)
            .catch((error) => {  
                console.log(JSON.stringify(error))
                return { error: true, response: error.response, status: error.status } 
            } )
}
function* getRequest(url, data, keepLoadingEnable) {
    let effect = yield (getReq(url));
    let isError=false;
    if(effect){
        localStorage.removeItem('isExceptionOccured');
        if (effect?.response?.status >= 500) {
            isError=true;
        }
        if(effect && typeof(effect?.status)==='undefined'){
            effect.status= effect?.response?.status;
        }
        return yield { error: isError, response: effect?.data };
    }
}
const interpolateString = (template, data) => {
    const braceRegex = /{(.*?)}/g;
    return template && template.replace(braceRegex, (_, key) => {
        let result = data;
        key.split('.').forEach((property) => {
            result = result ? result[property] : '';
        });
        return String(result);
    });
};
export {
    getRequest,
    interpolateString
}