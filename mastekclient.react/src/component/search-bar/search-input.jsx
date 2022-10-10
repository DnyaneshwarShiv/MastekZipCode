import { useState } from "react";
import { Typeahead } from "react-bootstrap-typeahead";
import { connect } from "react-redux";
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import { onPostCodeSelection, SearchPostCode, setAreaDetails } from "../../redux/postcode/postcode-actions";
import './search-input.scss'
import { Table } from "react-bootstrap";

const SearchBar = ({suggestionsList,searchPostCode,onPostCodeSelection,postCodeAreaDetails,setAreaDetails})=>{
  const [zipError, setZipError] = useState('')
  const searchPostCodesDetails=(zip)=>{
    let regEx=/^[0-9a-zA-Z]+$/;
    if(zip.match(regEx))
     {
      searchPostCode(zip)
      setZipError('')
     }
     else{
        setZipError('Zip code must be alphanumberic')
        setAreaDetails([])
     }
  }
    return(<>
    <div className="search">
        <input type='text'
          className="searchBox"
          onChange={(e)=>{searchPostCodesDetails(e.target.value)}}
          label="Search"
          placeholder="Seach ZipCode"
        />
        {zipError?<div className="error">{zipError}</div>:''}
        <ul className="suggestions-list">
          {suggestionsList?.map((suggestion, index) => {
            return (
              <li
                key={index}
                id={'suggestion-'+index}
                className='suggestion'
                onClick={() => {onPostCodeSelection(suggestion)}}
              >
               {suggestion} 
              </li>
            );
          })}
        </ul>
      </div>
      {postCodeAreaDetails?.length>0?<div className="zipCodeAreaDetails">
      <Table>
        <tr>
          <th>Country</th>
          <th>Region</th>
          <th>Admin District</th>
          <th>Parlimentary Constituency</th>
          <th>Area</th>
        </tr>
      <tbody>
       { postCodeAreaDetails?.map((postCode)=>{
        console.log(JSON.stringify(postCode)); 
        return(      <tr>
          <td>{postCode?.country}</td>
          <td>{postCode?.region}</td>
          <td>{postCode?.adminDistrict}</td>
          <td>{postCode?.parlimentaryConstituency}</td>
          <td>{postCode?.area}</td>
        </tr>)})}
       
      </tbody>
    </Table>
      </div>:<></>}
        </>
    )
}
const mapStateToProps = (state, props) => {
    let {PostCodeReducer} = state;
    if(PostCodeReducer){
        return {
            suggestionsList: PostCodeReducer?.zipCodeList,
            postCodeAreaDetails: PostCodeReducer?.postCodeAreaDetails
        }
    }
}
const mapDispatchToProps = (dispatch) => {
    return {
        searchPostCode: (payload) => dispatch(SearchPostCode(payload)),
        onPostCodeSelection: (payload)=> dispatch(onPostCodeSelection(payload)),
        setAreaDetails : (payload)=>dispatch(setAreaDetails(payload))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchBar);