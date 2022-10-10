
import { call, put, select, takeLatest } from "redux-saga/effects";
import { APIEndPointConfig } from "../../api-endpoint-cnfig";
import { getRequest, interpolateString } from "../../utils/saga-utils";
import { initPostCodeAutomplete, PostCodeActions, setAreaDetails } from "./postcode-actions";

function* fetchPostCode({payload}){
    let searchtext = payload;
    let getPostCodeUrl = interpolateString(process.env.REACT_APP_API_BASEURL + APIEndPointConfig.GET_POST_CODE_FOR_AUTO_COMPLETION, //use when API is ready 
        {
            code:searchtext
        })
        let {response,error} = yield call(getRequest, getPostCodeUrl);
        if(!error){
            if(response!==''){
                yield put(initPostCodeAutomplete(response));
            }
            else{
                
            }
        }
}
function* getAreaDetails({payload}){
    let searchtext = payload;
    let getPostCodeUrl = interpolateString(process.env.REACT_APP_API_BASEURL + APIEndPointConfig.GET_AREA_DETAILS_BASED_ON_ZIP, //use when API is ready 
        {
            selectedCode:searchtext
        })
        let {response,error} = yield call(getRequest, getPostCodeUrl);
        if(!error){
            yield put(initPostCodeAutomplete([]));
            yield put(setAreaDetails(response));
        }
}

export function* autoCompletionSaga() {
    yield takeLatest(PostCodeActions.SEARCH_POST_CODE, fetchPostCode);
 }
 
 export function* getAreaDetailsWatcher() {
    yield takeLatest(PostCodeActions.GET_AREA_DETAILS_BASED_ON_ZIP, getAreaDetails);
 }
 