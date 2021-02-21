
let favorites = []
toastr.options = {
    "closeButton": true,
    "positionClass": "toast-top-center",
    "showDuration": "1500",
    "hideDuration": "1000",
    "timeOut": "2000",
    "extendedTimeOut": "2000",
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
                item.remove();
                deleteFavorite(item);
                
            })


            item.querySelector(".cancel").addEventListener("click", () => {
                item.classList.remove("remove");
            })
        })
    })
}
async function deleteFavorite(target){
    let id = target.dataset.id;
    let url = "/ProductPage/DeleteFavorite";
    let data = { favoriteId: id };
    await fetch(url, {
        method: "Post",
        body: JSON.stringify(data),
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(result => {
            if (result.response == "success") {
                toastr.success("已刪除該收藏")
                getFavorites();
            }
        })
        .catch(err => {
            console.log(err)
            toastr.error("發生錯誤")
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
const favoriteSelectEffect = (target) => {
    if (target.checked) {
        target.parentNode.parentNode.parentNode.parentNode.classList.add("selected");
        document.querySelector(".checkout").href += `?id=${target.dataset.id}`
    } else {
        target.parentNode.parentNode.parentNode.parentNode.classList.remove("selected");
        document.querySelector(".checkout").href = document.querySelector(".checkout").href.split("?")[0]
    }
}

const createFavoritesCard = (datas) => {
    if (datas.IsPackage) {
        createPackageCard(datas)
    } else {
        createUserDefinedCard(datas)
    }
}

const createPackageCard = (datas) => {
    let data = datas.Data[0];
    let roomTypes = data.RoomType.split(",").filter(x => x != "-");
    let squrefeets = data.Squarefeet.split(",").filter(x => x != "-");
    let combinedString = roomTypes.map((x, y) => {
        return `${roomTypes[y]} - ${ squrefeets[y]}`
    }).join("、")
    let card = document.createElement("div");

    card.className = "favorites-product-item mb-3 mx-2";
    card.setAttribute("data-price", data.Price);
    card.setAttribute("data-id", datas.FavoriteId);

    let row = document.createElement("div");
    row.className = "row no-gutters w-100";

    let col4 = document.createElement("div");
    col4.classList.add("col-4");
    let col8 = document.createElement("div");
    col8.classList.add("col-8");

    let img = document.createElement("img");
    img.src = `https://i.imgur.com/${data.PhotoUrl}.jpg`;
    img.classList = "w-100 h-100";

    col4.append(img);

    let cardBody = document.createElement("div");
    cardBody.className = "card-body py-2 px-3 d-flex flex-column";

    let h3 = document.createElement("h3");
    h3.classList.add("card-title");
    h3.textContent = data.Title;

    let p1 = document.createElement("p");
    p1.className = "card-text";

    let p2 = document.createElement("p");
    p2.className = "card-text";

    let p3 = document.createElement("p");
    p3.className = "card-text mt-1";

    p1.textContent = combinedString;

    p2.textContent = data.ServiceItem.split("+").join("、");

    let small = document.createElement("small");
    small.classList.add("color-muted");
    small.textContent = "超值優惠服務!我們服務，你可放心";

    p3.append(small);

    let a = document.createElement("a");
    a.setAttribute("href", `/Detail/Index/${data.PackageProductId}`);
    a.className = "btns detail";
    a.textContent = "詳情";

    let checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.className = "checkbox";
    checkbox.setAttribute("data-id", datas.FavoriteId);

    cardBody.append(h3, p1, p2, p3, a, checkbox);
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

const createUserDefinedCard = (datas) => {
    let data1 = datas.Data[0], data2 = datas.Data[1];
    let roomType1 = roomTypeSwitch(+data1.RoomType);
    let squrefeet1 = squarefeetSwitch(+data1.Squarefeet);
    let combinedString1 = `${roomType1} - ${squrefeet1}`

    let roomType2 = roomTypeSwitch(+data2.RoomType);
    let squrefeet2 = squarefeetSwitch(+data2.Squarefeet);
    let combinedString2 = `${roomType2} - ${squrefeet2}`
   

    let card = document.createElement("div");
    card.className = "favorites-product-item mb-3 mx-2";
    card.setAttribute("data-id", datas.FavoriteId);

    let row = document.createElement("div");
    row.className = "row no-gutters w-100";

    let col4 = document.createElement("div");
    col4.classList.add("col-4","position-relative","h-100");
    let col8 = document.createElement("div");
    col8.classList.add("col-8", "h-100");

    let img1 = document.createElement("img");
    img1.src = `https://i.imgur.com/${data1.PhotoUrl}`;
    img1.classList = `w-100 h-100 img1`;

    let img2 = document.createElement("img");
    img2.src = `https://i.imgur.com/${data2.PhotoUrl}`;
    img2.classList = `w-100 h-100 img2`;

    col4.append(img1,img2);

    let cardBody = document.createElement("div");
    cardBody.className = "card-body py-2 px-3 d-flex flex-column";

    let h3 = document.createElement("h3");
    h3.classList.add("card-title");
    let tip = document.createElement("span");
    tip.classList.add("tip");
    tip.textContent = " (僅顯示此組合前兩筆)";
    h3.append(data1.Title);

    let p1 = document.createElement("p");
    p1.className = "card-text";
    p1.textContent = combinedString1;

    let p2 = document.createElement("p");
    p2.className = "card-text";
    p2.textContent = data1.ServiceItem.split(",").join("、");

    let hr = document.createElement("hr");  
        

    let p3 = document.createElement("p");
    p3.className = "card-text";
    p3.textContent = combinedString2;

    let p4 = document.createElement("p");
    p4.className = "card-text";
    p4.textContent = data2.ServiceItem.split(",").join("、");

    let a = document.createElement("a");
    a.setAttribute("href", `/MemberCenter#v-pills-favorites`);
    a.className = "btns detail";
    a.textContent = "查看收藏詳情";

    let checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.className = "checkbox";
    checkbox.setAttribute("data-id", datas.FavoriteId);

    cardBody.append(h3, tip, p1, p2, hr, p3, p4, a, checkbox);
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
    if (favorites.length == 0) {
        favoritesStatus("你目前的收藏是空的")
    } else {
        favorites.forEach(x => {
            createFavoritesCard(x);
        })
    }

    swipeDeleteEffect();

    document.querySelectorAll("input[type='checkbox']").forEach(x => {
        x.addEventListener("click", function () {
            favoriteSelectEffect(x);
        })
    })

}
const favoritesStatus = (words) => {
    document.querySelector(".section_favorites-side-menu .favorites-body").innerHTML = "";
    let div = document.createElement("div");
    let word = document.createElement("h4");
    let pic;
    word.textContent = words;
    word.classList.add("center");
    if (words === "你目前的收藏是空的") {
        pic = document.createElement("img");
        pic.src = "/Assets/images/empty.png";
    } else {
        pic = document.createElement("i");
        pic.className = "far fa-user color-primary";
    }

    div.classList.add("wrap");
    div.append(pic, word);
    document.querySelector(".section_favorites-side-menu .favorites-body").appendChild(div);
}
const checkoutBtnControl = () => {
    if (!getCookieValue || document.querySelectorAll(".favorites-product-item").length == 0) {
        document.querySelector(".favorites-footer .checkout").classList.add("disabled");
        document.querySelector(".favorites-footer .checkout").removeAttribute("href");
    } else {
        document.querySelector(".favorites-footer .checkout").classList.remove("disabled");
        document.querySelector(".favorites-footer .checkout").setAttribute("href", "/Checkout");
    }
}
const toggleTip = () => {
    if (!getCookieValue || document.querySelectorAll(".favorites-product-item").length == 0) {
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
const clearWarn = function (x) {
    x.classList.remove("input-warn");
};
const customerForm = () => {
    let validate = null;
    document.querySelectorAll(".contact-us .question").forEach(x => {
        clearWarn(x);
        if (x.value.length == 0) {
            x.classList.add("input-warn");
            validate = "fail";
        } else if (x.tagName.toLowerCase() === "select" && x.value === "-1") {
            x.classList.add("input-warn");
            validate = "fail";
        }
    });
    if (validate == "fail") {
        return;
    } else {
        document.querySelector(".finish-view .box").classList.remove("hide");

        const data = {};
        data.Name = $("#contact_name").val();
        data.Email = $("#contact_email").val();
        data.Phone = $("#contact_phone").val();
        data.Category = $("#contact_category").val() * 1;
        data.Content = $("#contact_content").val();

        $.ajax({
            url: "/Home/CustomerServiceCreate",
            method: "POST",
            data: data,
            success: function (result) {
                if (result.response === "success") {
                    setTimeout(() => {
                        document.querySelector(".finish-view .box").classList.add("hide");
                        document.querySelector(".finish-view .finished").classList.remove("hide");

                        $("#contact_name").val("");
                        $("#contact_email").val("");
                        $("#contact_phone").val("");
                        $("#contact_category").val("-1");
                        $("#contact_content").val("");
                    }, 1000)
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
}
const judgeCharacter = (str, judge) => {
    let result;
    switch (judge) {
        case "capital":
            result = str.match(/^.*[A-Z]+.*$/);
            break;
        case "lowercase":
            result = str.match(/^.*[a-z]+.*$/);
            break;
        case "english":
            result = str.match(/^.*[a-zA-Z]+.*$/);
            break;
        case "number":
            result = str.match(/^.*[0-9]+.*$/);
            break;
        case "other":
            result = str.match(/^.*[^0-9A-Za-z]+.*$/);
            break;
    }
    return result == null ? false : true;
}


async function getFavorites(){
    url = "/ProductPage/SearchForFavorite";
    await fetch(url)
        .then(res =>  res.json())
        .then(result => {
            favorites = result;
            countFavoritesAmount(favorites.length);
            showFavorites();
            checkoutBtnControl();
            toggleTip();
        })
        .catch(err => console.log(err))
}

function getCookieName(name) {
    let cookieObj = {};
    let cookieArr = document.cookie.split(";");

    for (let i = 0, j = cookieArr.length; i < j; i++) {
        let cookie = cookieArr[i].trim().split("=");
        cookieObj[cookie[0]] = cookie[1];
    }

    return cookieObj[`${name}`]
}

//得到加密過的帳號名稱
function getCookieValue() {
    return document.cookie.split(";")[0].split("=")[2]
}

const squarefeetSwitch = value =>
       value == 0 ? "5坪以下" :
       value == 1 ? "6-10坪" :
       value == 2 ? "11-15坪" :
       value == 3 ? "16坪以上" : "-";

const roomTypeSwitch = value =>
    value == 0 ? "廚房" :
    value == 1 ? "客廳" :
    value == 2 ? "臥室" :
    value == 3 ? "浴廁" :
    value == 4 ? "陽台" : "-";





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

    if (!document.cookie.includes("user")) {
        favoritesStatus("請先註冊/登入!");
        createLoginRegister();
        countFavoritesAmount(0);
        checkoutBtnControl();
        toggleTip();
    } else {
        getFavorites();
       
    }

    document.querySelectorAll(".subItem").forEach(x => {
        x.addEventListener("click", function (e) {
            toggleSideMenuSubItem(x, e);
        })
    })
    document.querySelector(".finish-view .box").classList.add("hide");
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

document.querySelector(".finish-view .finishBtn").addEventListener("click", function (e) {
    e.preventDefault();
    document.querySelector(".finish-view .finished").classList.add("hide");
})

document.querySelectorAll(".contact-us .question").forEach(x => {
    x.addEventListener("change", function () {
        console.log(x)
        clearWarn(x);
        if (x.value.length == 0) {
            x.classList.add("input-warn");
        } else if (x.tagName.toLowerCase() === "select" && x.value === "-1") {
            x.classList.add("input-warn");
        }
    })
});

document.querySelector(".checkout").addEventListener("click", function (e) {
    if (this.classList.contains("disabled")) return;
    if (Array.from(document.querySelectorAll("input[type='checkbox']")).every(x => x.checked == false)) {
        toastr.warning("必須要選一項，才能前往結帳");
        e.preventDefault();
        return;
    }
})

