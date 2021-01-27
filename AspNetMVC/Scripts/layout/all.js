const openHamburger = () => {
    document.querySelector(".hamburger").addEventListener("click", () => {
        document.querySelector(".side-menu").classList.add("show");
        document.querySelector(".fa-times").classList.add("show");
    })
}
const closeHamburger = () => {
    document.querySelector(".fa-times").addEventListener("click", function () {
        if (document.querySelector(".side-menu").classList.contains("show")) {
            document.querySelector(".side-menu").classList.remove("show");
            this.classList.remove("show");
        }
    })
}
const toggleAllService = () => {
    document.querySelector(".all-service").addEventListener("click", function () {
        if (document.querySelector("body").classList.contains("open")) {
            document.querySelector("body").classList.remove("open");
            document.querySelector(".section_collapse-zone").classList.remove("open");
        } else {
            document.querySelector("body").classList.add("open");
            document.querySelector(".section_collapse-zone").classList.add("open");
        }
        if (this.classList.contains("active")) this.classList.remove("active");
        else this.classList.add("active");

        //避免觸發關閉
        document.querySelector("#collapse").addEventListener("click", function (e) {
            e.stopPropagation();
        })

        if (document.querySelector(".section_collapse-zone").classList.contains("open")) {
            document.querySelector(".section_collapse-zone.open").addEventListener("click", function () {
                if (this.classList.contains("open")) {
                    this.classList.remove("open");
                    document.querySelector("body").classList.remove("open");
                    document.querySelector("#collapse").classList.remove("show");
                    document.querySelector(".all-service").classList.remove("active");
                }
            })
        }
    })
}
const toggleSideMenuAllService = () => {
    document.querySelector(".side-menu_body .all-service").addEventListener("click", function () {
        if (!document.querySelector(".side-menu_all-service").classList.contains("active")) {
            document.querySelector(".side-menu_all-service").classList.add("active");
        }
    })
    document.querySelector(".side-menu_all-service .all-service").addEventListener("click", function () {
        if (document.querySelector(".side-menu_all-service").classList.contains("active")) {
            document.querySelector(".side-menu_all-service").classList.remove("active");
        }

        document.querySelectorAll(".subItem").forEach(x => {
            if (x.classList.contains("open")) {
                x.classList.remove("open");
            }
        })
    })

}
const toggleSideMenuSubItem = (target, event) => {
    event.preventDefault();
    document.querySelectorAll(".subItem").forEach(x => {
        if (target != x) {
            if (x.classList.contains("open")) {
                x.classList.remove("open");
            }
        }
    })

    target.classList.toggle("open");
}
const toggleCart = () => {
    document.querySelector("#cart").addEventListener("click", function () {
        if (document.querySelector(".cart-side-menu").classList.contains("open")) {
            document.querySelector(".cart-side-menu").classList.remove("open")
        } else {
            document.querySelector(".cart-side-menu").classList.add("open")
        }
    })
    document.querySelector("#side-menu-cart").addEventListener("click", function () {
        if (document.querySelector(".cart-side-menu").classList.contains("open")) {
            document.querySelector(".cart-side-menu").classList.remove("open");
        } else {
            document.querySelector(".cart-side-menu").classList.add("open")
            document.querySelector(".side-menu").classList.remove("show");
        }
    })
    document.querySelector("#cart-close").addEventListener("click", function () {
        if (document.querySelector(".cart-side-menu").classList.contains("open")) {
            document.querySelector(".cart-side-menu").classList.remove("open")
        }
    })
}

const toggleContact = () => {
    document.querySelector(".contact-us button").addEventListener("click", function () {
        this.classList.add("active");
        document.querySelector(".contact-us-form").classList.add("active");
    })
    document.querySelector("#contact-close").addEventListener("click", () => {
        document.querySelector(".contact-us button").classList.remove("active");
        document.querySelector(".contact-us-form").classList.remove("active");
    })
}
const fakeLogin = () => {
    document.querySelector(".nav_btn-group #login-register").addEventListener("click", function () {
        createLogout();
        createMemberCenter();
        fakeLogout();
    })

    document.querySelector(".side-menu-nav .nav-item:nth-of-type(3) a").addEventListener("click", function () {
        createLogout();
        createMemberCenter();
        fakeLogout();
    })

}
const fakeLogout = () => {
    document.querySelector("#logout").addEventListener("click", function () {
        createLogIn();
        fakeLogin();
    })
    document.querySelector("#side-logout").addEventListener("click", function () {
        createLogIn();
        fakeLogin();
    })
}
const createMemberCenter = () => {
    if (document.querySelector("#login-register")) {
        document.querySelector("#login-register").remove();
    }
    if (document.querySelector("#side-login-register")) {
        document.querySelector("#side-login-register").remove();
    }

    let buttonMember = document.createElement("button");
    buttonMember.className = "btn border rounded-pill border-skyblue color-skyblue member-center";
    buttonMember.textContent = "會員中心";
    buttonMember.setAttribute("id", "member-center")
    document.querySelector(".nav_btn-group").prepend(buttonMember);


    let a = document.createElement("a");
    a.className = "nav-link color-gray member-center";
    a.setAttribute("href", "javascript:;");
    a.setAttribute("id", "side-member-center");

    let i = document.createElement("i");
    i.className = "fas fa-user ml-4 mr-2";

    a.append(i, "會員中心");

    document.querySelector(".side-menu-nav .nav-item:nth-of-type(3)").append(a);
}
const createLogout = () => {
    let li = document.createElement("li");
    li.classList.add("nav-item", "py-2");

    let a = document.createElement("a");
    a.className = "btn nav-link color-gray";
    a.setAttribute("href", "javascript:;");
    a.setAttribute("id", "side-logout");

    let i = document.createElement("i");
    i.className = "fas fa-sign-out-alt ml-4 mr-2";

    a.append(i, "登出");
    li.appendChild(a);
    document.querySelector(".side-menu-nav").appendChild(li);

    let button = document.createElement("button");
    button.classList.add("btn");
    button.innerText = "登出";
    button.setAttribute("id", "logout");
    document.querySelector(".nav_btn-group").prepend(button);

}
const createLogIn = () => {
    if (document.querySelector("#side-member-center")) {
        document.querySelector("#side-member-center").remove();
    }
    if (document.querySelector("#side-logout")) {
        document.querySelector(".side-menu-nav .nav-item:nth-of-type(5)").remove();
    }
    if (document.querySelector("#member-center")) {
        document.querySelector("#member-center").remove();
    }
    if (document.querySelector("#logout")) {
        document.querySelector("#logout").remove();
    }
    let button = document.createElement("button");
    button.className = "btn border rounded-pill border-skyblue color-skyblue login-register";
    button.setAttribute("id", "login-register")
    button.textContent = "註冊/登入";

    document.querySelector(".nav_btn-group").prepend(button);

    let a = document.createElement("a");
    a.className = "btn nav-link color-gray"
    a.setAttribute("href", "javascript:;");
    a.setAttribute("id", "side-login-register");

    let i = document.createElement("i");
    i.className = "fas fa-user ml-4 mr-2"

    a.append(i, "註冊/登入");
    document.querySelector(".side-menu-nav .nav-item:nth-of-type(3)").appendChild(a);
}

const loadingAnimation = () => {
    setTimeout(() => {
        document.querySelector(".section_loading").classList.add("inactive");
    }, 2000)
}

const imgLazyLoad = () => {
    let imgs = document.querySelectorAll(".lazyload");
    let observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.src = entry.target.dataset.src;
                entry.target.removeAttribute("data-src");
                observer.unobserve(entry.target);
            }
        })
    })
    imgs.forEach(item => observer.observe(item));
}
const hoverEffect = () => {
    const imgSrc = document.querySelectorAll(".section_collapse-zone a");
    imgSrc.forEach(item => {
        let img = document.createElement("img");
        img.setAttribute("style", "opacity:0;position:absolute; z-index:-1;top:0;left:0;");
        img.src = item.dataset.imgsrc;
        document.querySelector("body").append(img);

        item.addEventListener("mouseenter", function () {
            setTimeout(() => {
                document.querySelector(".section_collapse-zone img").src = item.dataset.imgsrc;
            }, 300);
        })
    })
}
const notation = (value) => {
    let length = value.toString().length
    return length <= 3 ? value :
        length == 4 ? `${value.toString().substring(0, 1)},${value.toString().substring(1, 4)}` :
        length == 5 ? `${value.toString().substring(0, 2)},${value.toString().substring(2, 5)}` :
        length == 6 ? `${value.toString().substring(0, 3)},${value.toString().substring(3, 6)}` :
        length == 7 ? `${value.toString().substring(0, 1)},${value.toString().substring(1, 4)},${value.toString().substring(4, 7)}` :
        length == 8 ? `${value.toString().substring(0, 2)},${value.toString().substring(2, 5)},${value.toString().substring(5, 8)}` : `${value.toString().substring(0, 3)},${value.toString().substring(3, 6)},${value.toString().substring(6, 9)}`
}
const swipeDeleteEffect = () => {
    let cartProducts = document.querySelectorAll(".cart-product-item");

    cartProducts.forEach(item => {
        let hammer = new Hammer(item);
        hammer.on("swipeleft", function () {
            item.classList.add("remove");

            item.querySelector(".confirm").addEventListener("click", () => {
                item.classList.add("delete");
                setTimeout(() => {
                    item.remove();
                    countCartAmount();
                    countCartPrice();
                }, 500);
            })


            item.querySelector(".cancel").addEventListener("click", () => {
                item.classList.remove("remove");
            })
        })
    })
}
const countCartAmount = () => {
    let count = document.querySelectorAll(".cart-product-item").length;

    document.querySelectorAll(".count").
    forEach(x => x.innerText = count);

    if (count == 0) document.querySelector(".cart-body").style.overflowY = "auto";
    else document.querySelector(".cart-body").style.overflowY = "scroll";
}
const countCartPrice = () => {
    let products = document.querySelectorAll(".cart-product-item");

    if (products.length > 0) {
        let total = Array.from(products).map(x => parseInt(x.dataset.price)).reduce((x, y) => x + y);
        document.querySelector(".cart-footer h2").innerText = `小計 : ${notation(total)} 元`;
    } else {
        document.querySelector(".cart-footer h2").innerText = `小計 : 0 元`;
    }
}



window.addEventListener("load", () => {
    loadingAnimation();
    openHamburger();
    closeHamburger();
    toggleAllService();
    toggleSideMenuAllService();
    toggleCart();
    toggleContact();
    fakeLogin();
    imgLazyLoad();
    hoverEffect();
    swipeDeleteEffect();
    countCartAmount();
    countCartPrice();

    document.querySelectorAll(".subItem").forEach(x => {
        x.addEventListener("click", function (e) {
            toggleSideMenuSubItem(x, e);
        })
    })
})

window.addEventListener("resize", () => {
    if (window.innerWidth > 1024 && document.querySelector(".side-menu").classList.contains("show")) {
        document.querySelector(".side-menu").classList.remove("show");
    } else {
        document.querySelector(".cart-side-menu").classList.remove("open");
    }
})