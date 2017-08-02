import React, { Component } from "react";
import "./NewsEvent.css";

export default class NewsEvent extends Component {

    static defaultProps = {
        name: "hello"
    };

    render() {
        return (
            <div className="NewsEvent">{this.props.name}</div>
        );
    }
}