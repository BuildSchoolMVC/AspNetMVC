﻿let favorites = JSON.parse(localStorage.getItem("favorites")) || []
let isLogin = JSON.parse(localStorage.getItem("login")) || false;
toastr.options = {
    "closeButton": true,
    "positionClass": "toast-top-center",
    "showDuration": "800",
    "hideDuration": "1000",
    "timeOut": "1000",
    "extendedTimeOut": "500",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

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
const toggleFavorites = () => {
    document.querySelector("#favorites").addEventListener("click", function () {
        if (document.querySelector(".favorites-side-menu").classList.contains("open")) {
            document.querySelector(".favorites-side-menu").classList.remove("open")
        } else {
            document.querySelector(".favorites-side-menu").classList.add("open")
        }
    })
    document.querySelector("#favorites-close").addEventListener("click", function () {
        if (document.querySelector(".favorites-side-menu").classList.contains("open")) {
            document.querySelector(".favorites-side-menu").classList.remove("open")
        }
    })
}
const openBottomFavorites = () => {
    document.querySelectorAll(".nav-bottom-item")[1].addEventListener("click", function () {
        document.querySelector(".section_favorites-side-menu").classList.add("open");
    })
}
const openBottomCustomerService = () => {
    document.querySelectorAll(".nav-bottom-item")[3].addEventListener("click", function () {
        document.querySelector(".contact-us-form").classList.toggle("active");
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
const loadingAnimation = () => {
    setTimeout(() => {
        document.querySelector(".section_loading").classList.add("inactive");
    }, 100)
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
        img.setAttribute("style", "opacity:0;position:absolute; z-index:-1;top:0;left:0;width:100%");
        img.src = item.dataset.imgsrc;

        document.querySelector(".section_footer .logo").append(img);

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
    let favoritesProducts = document.querySelectorAll(".favorites-product-item");

    favoritesProducts.forEach(item => {
        let hammer = new Hammer(item);
        hammer.on("swipeleft", function () {
            item.classList.add("remove");

            item.querySelector(".confirm").addEventListener("click", () => {
                item.classList.add("delete");
                setTimeout(() => {
                    item.remove();
                    let index = favorites.findIndex(x => x.id == item.dataset.id);
                    if (index != -1) {
                        favorites.splice(index, 1);
                        localStorage.setItem("favorites", JSON.stringify(favorites));
                        countFavoritesAmount(favorites.length);
                        checkoutBtnControl();
                        toggleTip();
                        if (favorites.length == 0) favoritesStatus("你目前的收藏是空的");
                        toastr.info("成功刪除!!");
                    }
                }, 500);
            })


            item.querySelector(".cancel").addEventListener("click", () => {
                item.classList.remove("remove");
            })
        })
    })
}
const countFavoritesAmount = (count) => {
    if (document.querySelector(".nav-bottom-item .red-dot")) {
        document.querySelector(".nav-bottom-item .red-dot").remove();
    }
    let span = document.createElement("span");
    span.className = "count red-dot";
    document.querySelector(".nav-bottom-item .fa-heart").prepend(span);
    document.querySelectorAll(".count").
    forEach(x => x.innerText = count);

    if (count == 0) {
        document.querySelector(".favorites-body").style.overflowY = "auto";
    } else document.querySelector(".favorites-body").style.overflowY = "scroll";
}

const createFavoritesCard = (price, title, items, id) => {
    let card = document.createElement("div");
    card.className = "favorites-product-item mb-3";
    card.setAttribute("data-price", price);
    card.setAttribute("data-id", id);

    let row = document.createElement("div");
    row.className = "row no-gutters w-100";

    let col4 = document.createElement("div");
    col4.classList.add("col-4");
    let col8 = document.createElement("div");
    col8.classList.add("col-8");

    let img = document.createElement("img");
    img.src = `https://picsum.photos/300/400/?random=${id}`;
    img.classList = "w-100 h-100";

    col4.append(img);

    let cardBody = document.createElement("div");
    cardBody.className = "card-body py-4 px-3 d-flex flex-column";

    let h3 = document.createElement("h3");
    h3.classList.add("card-title");
    h3.textContent = title;

    let ul = document.createElement("ul");
    ul.classList.add("list-unstyled");

    items.forEach(x => {
        let li = document.createElement("li");
        li.textContent = x;
        ul.append(li);
    })
    let p = document.createElement("p");
    p.className = "card-text pt-3";

    let small = document.createElement("small");
    small.classList.add("color-muted");
    small.textContent = "超值優惠服務!我們服務，你可放心";

    p.append(small);

    let a = document.createElement("a");
    a.setAttribute("href", "javascript:;");
    a.className = "btns detail";
    a.textContent = "詳情";

    cardBody.append(h3, ul, p, a);
    col8.append(cardBody);

    let btnGroup = document.createElement("div");
    btnGroup.classList.add("btns-group");

    let btnConfirm = document.createElement("button");
    btnConfirm.className = "btns confirm";
    btnConfirm.textContent = "確認刪除";

    let btnCancel = document.createElement("button");
    btnCancel.className = "btns cancel";
    btnCancel.textContent = "取消";
    btnGroup.append(btnConfirm, btnCancel);
    row.append(col4, col8, btnGroup);
    card.appendChild(row);

    document.querySelector(".section_favorites-side-menu .favorites-body").appendChild(card);
}
const showFavorites = () => {
    document.querySelector(".section_favorites-side-menu .favorites-body").innerHTML = "";
    favorites.forEach(x => {
        createFavoritesCard(x.price, x.title, x.items, x.id);
    })
    swipeDeleteEffect();
}
const favoritesStatus = (words) => {
    document.querySelector(".section_favorites-side-menu .favorites-body").innerHTML = "";
    let div = document.createElement("div");
    let word = document.createElement("h4");
    word.textContent = words;
    word.classList.add("center");
    div.classList.add("wrap");
    div.append(word);
    document.querySelector(".section_favorites-side-menu .favorites-body").appendChild(div);
}
const checkoutBtnControl = () => {
    if (isLogin == false || favorites.length == 0) {
        document.querySelector(".favorites-footer .checkout").setAttribute("disabled", true);
    } else {
        document.querySelector(".favorites-footer .checkout").removeAttribute("disabled");

    }
}
const toggleTip = () => {
    if (isLogin == false || favorites.length == 0) {
        document.querySelector(".tip").classList.add("hide");
    } else {
        document.querySelector(".tip").classList.remove("hide");
    }
}
const createLoginRegister = () => {
    let a = document.createElement("a");
    a.className = "btns login-register";
    a.setAttribute("href", "/Account/Login");
    a.setAttribute("id", "registerLink");
    a.textContent = "註冊 / 登入";
    document.querySelector(".section_favorites-side-menu .favorites-body .wrap").appendChild(a);
}
const customerForm = () => {
    const data = {};
    data.Name = $("#contact_name").val();
    data.Email = $("#contact_email").val();
    data.Phone = $("#contact_phone").val();
    data.Category = $("#contact_category").val() * 1;
    data.Content = $("#contact_content").val();
    const token = $('input[name="__RequestVerificationToken"]').val();
    console.log(data)

    $.ajax({
        url: "/Home/CustomerServiceCreate",
        method: "POST",
        data: {
            data: data,
            __RequestVerificationToken: token,
            returnUrl: "Home/Index"
        },
        success: function (result) {
            console.log(result);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

window.addEventListener("load", () => {
    loadingAnimation();
    openHamburger();
    closeHamburger();
    toggleAllService();
    toggleSideMenuAllService();
    toggleFavorites();
    toggleContact();
    openBottomFavorites();
    openBottomCustomerService();
    imgLazyLoad();
    hoverEffect();
    toggleTip();

    if (isLogin == false) {
        favoritesStatus("請先註冊/登入!");
        createLoginRegister();
        countFavoritesAmount(0);
        checkoutBtnControl();
        toggleTip();
    } else if (isLogin == true) {
        console.log(favorites)
        if (favorites.length == 0) {
            checkoutBtnControl();
            countFavoritesAmount(0);
            favoritesStatus("你目前的收藏是空的");
        } else {
            showFavorites();
            countFavoritesAmount(favorites.length);
        }
    }

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
        document.querySelector(".favorites-side-menu").classList.remove("open");
    }

    if (window.innerWidth < 1024) {
        document.querySelector(".section_collapse-zone").classList.remove("open");
        document.querySelector("#collapse").classList.remove("show");
        document.querySelector(".all-service").classList.remove("active");
        document.querySelector("body").classList.remove("open");
    }
    if (window.innerWidth > 1024 && document.querySelector(".contact-us-form").classList.contains("active")) {
        document.querySelector(".contact-us-form").classList.remove("active");
        document.querySelector(".contact-us button").classList.remove("active");
    }
})
document.querySelector(".contact-us input[type='submit']").addEventListener("click", function (e) {
    e.preventDefault();
    customerForm();
})