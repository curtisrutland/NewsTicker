class NewsTicker {
    constructor(selector, seconds) {
        this.$el = $(selector);
        this.el = this.$el.get(0);
        this.animationLength = seconds * 1000;
        this.setPaddings();
        this.runAnimation();
    }

    getScrollWidth() {
        return this.el.scrollWidth - this.el.clientWidth;
    }

    setPaddings() {
        $(":first-child", this.$el).attr("style", `padding-left: ${window.screen.availWidth}px`);
        $(":last-child", this.$el).attr("style", `padding-right: ${window.screen.availWidth}px`);
    }

    runAnimation() {
        this.$el.scrollLeft(0);
        this.$el.animate({
            scrollLeft: this.getScrollWidth()
        }, this.animationLength, "linear", () => {
            this.runAnimation();
        });
    }
}


$(() => {
    let t = new NewsTicker("#ticker-1", 30);
});