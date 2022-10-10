import { PostCodeActions } from "./postcode-actions";

const initialState = {
    zipCodeList:[],
    postCodeAreaDetails:[],
    zipNotFoundMessage:''
};

const PostCodeReducer = (state = initialState, action) => {
    let {type, payload} = action
    switch (type) {
        case PostCodeActions.SET_AUTOCOMPLETE_POSTCODE:
            return {...state, zipCodeList: payload}
        case PostCodeActions.SET_AREA_DETAILS_FOR_ZIP:
            return {...state, postCodeAreaDetails: payload}
        case PostCodeActions.SET_ZIP_NOT_FOUND:
            return {...state, zipNotFoundMessage: payload}
        default:
            return state
    }
}
export default PostCodeReducer