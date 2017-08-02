import React, { Component } from 'react';
import NewsEvent from "../NewsEvent/NewsEvent";
import $ from "jquery";
import "./Ticker.css";

export default class Ticker extends Component {
    static defaultProps = {
        speed: 20
    };

    constructor() {
        super();
        this.state = { loading: true };
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        this.setState({ loading: true });
        fetch("/mock-data.json")
            .then(r => r.json())
            .then(r => {
                this.setState({ news: r, loading: false });
                this.setPaddings();
                this.animate();
            });
    }

    setPaddings() {
        const style = `${window.innerWidth}px`;
        this.el.firstChild.style.paddingLeft = style;
        this.el.lastChild.style.paddingRight = style;
    }

    animate() {
        const scrollWidth = this.el.scrollWidth - this.el.clientWidth;
        const runtime = this.props.speed * scrollWidth;
        this.el.scrollLeft = 0;
        let $el = $(this.el);
        $el.animate({ scrollLeft: scrollWidth }, runtime, "linear", () => this.fetchData());
    }


    render() {
        return this.state.loading
            ? this.renderLoading()
            : this.renderItems();
    }

    renderLoading() {
        return (
            <div className="Loading">Loading...</div>
        );
    }

    renderItems() {
        let items = this.state.news.map((n, i) => <NewsEvent name={n} key={i} />);
        return (
            <div className="Ticker" ref={i => this.el = i}>
                {items}
            </div >
        );
    }
}

