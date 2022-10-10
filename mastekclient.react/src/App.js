import logo from './logo.svg';
import './App.css';
import { Route, BrowserRouter as Router,Routes } from 'react-router-dom';
import WelcomePage from './component/welcome-page';
console.log(process.env.REACT_APP_API_BASEURL)
function App() {
  return (
    <div className="App">
      <Router>
        <div className="app-container">
          
            <div className='middle-section'>
                <Routes>
                      <Route path="/" element={<WelcomePage/>} />
                  
                </Routes>
            </div>
        </div>
      </Router>
    </div>
  );
}

export default App;
