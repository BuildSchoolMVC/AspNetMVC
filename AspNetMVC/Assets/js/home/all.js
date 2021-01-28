const initCarousel = () => {
    $(".owl-carousel").owlCarousel({
        items: 1,
        loop: true
    });
}

const newsFadeAmimation = () => {
    let newsCards = document.querySelectorAll(".section_news .card");
    newsCards.forEach(item => item.classList.remove("faded-bottom", "faded-left"));
    let observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (window.innerWidth > 768) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("faded-bottom");
                } else {
                    entry.target.classList.remove("faded-bottom");
                }
            } else {
                if (entry.isIntersecting) {
                    entry.target.classList.add("faded-left");
                } else {
                    entry.target.classList.remove("faded-left");
                }
            }
        })
    })

    newsCards.forEach(item => observer.observe(item));
}
const imageFadeAmimation = () => {
    let imageCards = document.querySelectorAll(".section_image .card");
    imageCards.forEach(item => item.classList.remove("faded-bottom", "faded-left"));
    let observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (window.innerWidth > 1200) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("faded-bottom");
                } else {
                    entry.target.classList.remove("faded-bottom");
                }
            } else {
                if (entry.isIntersecting) {
                    entry.target.classList.add("faded-left");
                } else {
                    entry.target.classList.remove("faded-left");
                }
            }
        })
    })

    imageCards.forEach(item => {
        observer.observe(item)
    })
}

const counterEffect = () => {
    const counters = document.querySelectorAll(".section_statistic .num");

    let observer = new IntersectionObserver(entries => {
        entries.forEach(item => {
            if (item.isIntersecting) {
                let c = 0;
                item.target.innerText = `0${item.target.dataset.unit}`;

                const updateCounter = () => {
                    const value = +item.target.getAttribute("data-value");
                    const count = c;
                    const increment = value / 300;
                    if (count <= value) {
                        let num = Math.ceil(count + increment);
                        c = num;

                        item.target.innerText = `${notation(c)} ${item.target.dataset.unit}`;
                        setTimeout(updateCounter, 1);
                    } else if (count > value) {
                        item.target.innerText = `${notation(value)} ${item.target.dataset.unit}`;
                    }

                };

                updateCounter();
                observer.unobserve(item.target);
            }
        });
    });
    counters.forEach(item => observer.observe(item));

    const notation = (value) => {
        let length = value.toString().length
        return length <= 3 ? value :
            length == 4 ? `${value.toString().substring(0, 1)},${value.toString().substring(1, 4)}` :
            length == 5 ? `${value.toString().substring(0, 2)},${value.toString().substring(2, 5)}` :
            length == 6 ? `${value.toString().substring(0, 3)},${value.toString().substring(3, 6)}` :
            length == 7 ? `${value.toString().substring(0, 1)},${value.toString().substring(1, 4)},${value.toString().substring(4, 7)}` :
            length == 8 ? `${value.toString().substring(0, 2)},${value.toString().substring(2, 5)},${value.toString().substring(5, 8)}` : `${value.toString().substring(0, 3)},${value.toString().substring(3, 6)},${value.toString().substring(6, 9)}`
    }
}
const toggleServiceTab = (target, event) => {
    event.preventDefault();
    document.querySelectorAll(".tabs-item").forEach(x => {
        x.classList.remove("active");
    })
    target.classList.add("active");

    document.querySelectorAll("div[class*='tab-item_']").forEach(x => {
        x.classList.remove("active");
    })
    document.querySelector(`.tab-item_${target.dataset.id}`).classList.add("active");
}



window.addEventListener("load", () => {
    initCarousel();
    counterEffect();

    newsFadeAmimation();
    imageFadeAmimation();

    document.querySelectorAll(".tabs-item").forEach(x => {
        x.addEventListener("click", function (e) {
            toggleServiceTab(x, e);
        })
    })

})

window.addEventListener("resize", () => {
    newsFadeAmimation();
    imageFadeAmimation();
})