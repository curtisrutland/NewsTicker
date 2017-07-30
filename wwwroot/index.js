class NewsItem {
    constructor(item) {
        this.message = item.message;
        this.group = item.group;
        this.severity = item.severity;
        this.id = item.id;
    }

    get color() {
        switch(this.severity) {
            case 0: return "success";
            case 1: return "info";
            case 2: return "warning";
            case 3: return "critical";
        }
    }

    toElement() {
        return $(`<div class="item ${this.color}" data-id="${this.id}">${this.message}</div>`);
    }
}

class NewsTicker {
    constructor(selector, group, seconds) {
        this.$el = $(selector);
        this.el = this.$el.get(0);
        this.animationLength = seconds * 1000;
        this.group = group;
        this.fetchNews();
    }

    fetchNews() {
        this.setLoading();
        const url = `/api/news/group/${this.group}`;
        fetch(url).then(r => r.json()).then(r => this.setNews(r));
    }

    setLoading() {
        this.$el.empty();
        this.$el.append($("<div class='loading item'>Loading...</div>"));
    }

    setNews(news) {
        console.log(news);
        if(!news || !news.length) {
            news = [new NewsItem({
                message: "No news is good news",
                severity: 0,
                group: this.group,
                id: 0
            })];
        }
        this.$el.empty();
        news.map(item => new NewsItem(item)).forEach(item => this.$el.append(item.toElement()));
        this.setPaddings();
        this.runAnimation();
    }

    get scrollWidth() {
        return this.el.scrollWidth - this.el.clientWidth;
    }

    setPaddings() {
        $(":first-child", this.$el).css("padding-left", `${window.innerWidth}px`);
        $(":last-child", this.$el).css("padding-right", `${window.innerWidth}px`);
    }

    runAnimation() {
        //scroll at 20 pixels per ms
        let totalTime = 20 * this.scrollWidth;
        console.log(totalTime);
        this.$el.scrollLeft(0);
        this.$el.animate({
            scrollLeft: this.scrollWidth
        }, totalTime, "linear", () => {
            //this.runAnimation();
            this.fetchNews();
        });
    }
}


$(() => {
    let t = new NewsTicker("#ticker-1", 1, 30);
});