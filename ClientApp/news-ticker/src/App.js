import React, { Component } from 'react';
import TopSection from "./components/TopSection/TopSection";
import BottomSection from "./components/BottomSection/BottomSection";
import './App.css';

class App extends Component {


  render() {
    return (
      <div className="App">
        <TopSection />
        <BottomSection />
      </div>
    );
  }
}

export default App;
