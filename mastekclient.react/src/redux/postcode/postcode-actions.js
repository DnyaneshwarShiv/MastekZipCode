const PostCodeActions = {
    SET_AUTOCOMPLETE_POSTCODE: 'autocomplete_post_code',
    SEARCH_POST_CODE:'search_post_code',
    GET_AREA_DETAILS_BASED_ON_ZIP:'get_area_details_based_on_zip',
    SET_AREA_DETAILS_FOR_ZIP:'set_area_details',
    SET_ZIP_NOT_FOUND:'set_zip_not_found'
}

const SearchPostCode = (payload)=>({
    type: PostCodeActions.SEARCH_POST_CODE,
    payload
})
const initPostCodeAutomplete = (payload) => ({
    type: PostCodeActions.SET_AUTOCOMPLETE_POSTCODE,
    payload
})

const onPostCodeSelection = (payload)=>({
    type: PostCodeActions.GET_AREA_DETAILS_BASED_ON_ZIP,
    payload
})

const setAreaDetails = (payload)=>({
    type: PostCodeActions.SET_AREA_DETAILS_FOR_ZIP,
    payload
})
const setNoZipFound =(payload)=>({
    type: PostCodeActions.SET_ZIP_NOT_FOUND,
    payload
})

export {
    PostCodeActions,
    initPostCodeAutomplete,
    SearchPostCode,
    onPostCodeSelection,
    setAreaDetails,
    setNoZipFound
}