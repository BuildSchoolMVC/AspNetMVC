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
    h3.classList.add("card-title", "package", "py-1", "text-truncate","pl-2");
    h3.textContent = data.Title;

    let ul = document.createElement("ul");
    ul.className = "pl-3 mb-0";

    let li1 = document.createElement("li");
    li1.className = "card-text";

    let li2 = document.createElement("li");
    li2.className = "card-text";

    let p3 = document.createElement("p");
    p3.className = "card-text mt-1";

    li1.textContent = combinedString;

    li2.textContent = data.ServiceItem.split("+").join("、");

    ul.append(li1, li2);

    let small = document.createElement("small");
    small.className = " text-truncate";
    small.textContent = "超值優惠服務!我們服務，你可放心";

    p3.append(small);

    let checkout = document.createElement("a");
    checkout.setAttribute("href", `/Checkout/?id=${datas.FavoriteId}`);
    checkout.className = "btns checkout";
    checkout.textContent = "結帳去";

    let detail = document.createElement("a");
    detail.setAttribute("href", `/Detail/Index/${data.PackageProductId}`);
    detail.className = "btns detail";
    detail.textContent = "查看";

    let groups = document.createElement("div");
    groups.className = "grouping";
    groups.append(checkout, detail);

    cardBody.append(h3, ul, p3, groups);
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
    let data1 = datas.Data[0];
    let card = document.createElement("div");
    card.className = "favorites-product-item mb-3 mx-2";
    card.setAttribute("data-id", datas.FavoriteId);

    let row = document.createElement("div");
    row.className = "row no-gutters w-100 h-auto";

    let col4 = document.createElement("div");
    col4.classList.add("col-4", "position-relative", "h-100");

    let col8 = document.createElement("div");
    col8.classList.add("col-8", "h-100");

    let cardBody = document.createElement("div");
    cardBody.className = "card-body py-2 px-3 d-flex flex-column";

    let h3 = document.createElement("h3");
    h3.classList.add("card-title", "py-1", "text-truncate", "pl-2");

    let tip = document.createElement("span");
    tip.classList.add("tip");
    tip.textContent = " (僅顯示此組合前兩筆)";
    h3.append(data1.Title);

    let checkout = document.createElement("a");
    checkout.setAttribute("href", `/Checkout/?id=${datas.FavoriteId}`);
    checkout.className = "btns checkout";
    checkout.textContent = "結帳去";

    let detail = document.createElement("a");
    detail.setAttribute("href", `/MemberCenter`);
    detail.className = "btns detail";
    detail.textContent = "查看";

    let groups = document.createElement("div");
    groups.className = "grouping";
    groups.append(checkout, detail);

    if (datas.Data.length < 2) {
        let roomType1 = roomTypeSwitch(+data1.RoomType);
        let squrefeet1 = squarefeetSwitch(+data1.Squarefeet);
        let combinedString1 = `${roomType1} - ${squrefeet1}`

        let img1 = document.createElement("img");
        img1.src = `https://i.imgur.com/${data1.PhotoUrl}`;
        img1.classList = `w-100 h-100`;

        col4.append(img1);

        let ul = document.createElement("ul");
        ul.className = "pl-3 mb-0";

        let li1 = document.createElement("li");
        li1.className = "card-text";
        li1.textContent = combinedString1;

        let li2 = document.createElement("li2");
        li2.className = "card-text";
        li2.textContent = data1.ServiceItem.split(",").join("、");

        ul.append(li1, li2);

        cardBody.append(h3, tip, ul, groups);
    } else {
        let data2 = datas.Data[1];
        let roomType1 = roomTypeSwitch(+data1.RoomType);
        let squrefeet1 = squarefeetSwitch(+data1.Squarefeet);
        let combinedString1 = `${roomType1} - ${squrefeet1}`

        let roomType2 = roomTypeSwitch(+data2.RoomType);
        let squrefeet2 = squarefeetSwitch(+data2.Squarefeet);
        let combinedString2 = `${roomType2} - ${squrefeet2}`

        let img1 = document.createElement("img");
        img1.src = `https://i.imgur.com/${data1.PhotoUrl}`;
        img1.classList = `w-100 h-100 img1`;

        let img2 = document.createElement("img");
        img2.src = `https://i.imgur.com/${data2.PhotoUrl}`;
        img2.classList = `w-100 h-100 img2`;

        col4.append(img1, img2);

        let div = document.createElement("div");
        div.className = "d-flex justify-content-evenly py-2";

        let ul1 = document.createElement("ul");
        ul1.className = "pl-0 pr-2 mb-0 pr-md-3 border-right w-50";
        let ul2 = document.createElement("ul");
        ul2.className = "pl-2 w-50";

        let li1 = document.createElement("li");
        li1.className = "card-text text-truncate";
        li1.textContent = combinedString1;

        let li2 = document.createElement("li");
        li2.className = "card-text text-truncate";
        li2.textContent = data1.ServiceItem.split(",").join("、");

        ul1.append(li1, li2);

        let li3 = document.createElement("li");
        li3.className = "card-text text-truncate";
        li3.textContent = combinedString2;

        let li4 = document.createElement("li");
        li4.className = "card-text text-truncate";
        li4.textContent = data2.ServiceItem.split(",").join("、");

        ul2.append(li3, li4);
        div.append(ul1, ul2);

        cardBody.append(h3, tip, div, groups);
    }
   
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
        if (x.value.length == 0 || x.tagName.toLowerCase() === "select" && x.value === "-1") {
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
    let url = "/ProductPage/SearchForFavorite";
    await fetch(url)
        .then(res =>  res.json())
        .then(result => {
            favorites = result;
            countFavoritesAmount(favorites.length);
            showFavorites();
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
       value == 3 ? "16坪以上" : "";

const roomTypeSwitch = value =>
    value == 0 ? "廚房" :
    value == 1 ? "客廳" :
    value == 2 ? "臥室" :
    value == 3 ? "浴廁" :
    value == 4 ? "陽台" : "";

function googleSigninInit() {
    gapi.load('auth2', function () {
        gapi.auth2.init({
            client_id: GoolgeApp_Cient_Id
        })
    })
}

function googleLogin(target) {
    let auth2 = gapi.auth2.getAuthInstance();
    let url = "/Account/GoogleLogin"

    auth2.signIn().then(function (GoogleUser) {
        let AuthResponse = GoogleUser.getAuthResponse(true);
        let id_token = AuthResponse.id_token;
        $.ajax({
            url: url,
            method: "post",
            data: { token: id_token, type: target.dataset.type },
            success: function (result) {
                if (result.status == 1 && result.response == "第三方登入") {
                    setTimeout(() => {
                        window.location.replace(`${window.location.origin}/Home/Index`);
                    }, 1500)
                }
                else if (result.status == 1) {
                    setTimeout(() => {
                        localStorage.setItem("social", result.response)
                        window.location.replace(`${window.location.origin}/Account/SocialRegister`);
                    }, 1500)
                } else if (result.status == 0) {
                    setTimeout(() => {
                        toastr.warning(`${result.response}`);
                        document.querySelectorAll("button").forEach(x => {
                            x.removeAttribute("disabled");
                            x.classList.remove("disabled");
                        })
                        target.querySelector(".spinner-border-wrap").classList.add("opacity");
                    }, 1500)
                }
            }
        });
    },
        function (error) {
            toastr.error("Google登入失敗")
            document.querySelectorAll("button").forEach(x => {
                x.removeAttribute("disabled");
                x.classList.remove("disabled");
            })
            document.querySelectorAll(".spinner-border-wrap").forEach(x => {
                if (!x.classList.contains("opacity")) x.classList.add("opacity");
            })
        });
}

function facebookLogin(response, target) {
    FB.login(function () {
        getProfile(target)
    }, { scope: 'email' });
}
function checkLoginState(target) {
    FB.getLoginStatus(function (response) {
        facebookLogin(response, target);
    });
}
function getProfile(target) {
    FB.api('/me', "GET", { fields: 'name,email,id,picture' }, function (response) {
        fetchData(response, target)
    })
}
function fetchData(response, target) {
    let url = "/Account/FacebookLogin"
    let data = {
        email: response.email,
        socialPlatform: "Facebook",
        imgUrl: response.picture.data.url,
        type: target.dataset.type
    }
    fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(result => {
            if (result.status == 1 && result.response == "第三方登入") {
                setTimeout(() => {
                    window.location.replace(`${window.location.origin}/Home/Index`);
                }, 1500)
            }
            else if (result.status == 1) {
                setTimeout(() => {
                    localStorage.setItem("social", result.response)
                    window.location.replace(`${window.location.origin}/Account/SocialRegister`);
                }, 1500)
            } else if (result.status == 0) {
                setTimeout(() => {
                    toastr.warning(`${result.response}`);
                    document.querySelectorAll("button").forEach(x => {
                        x.removeAttribute("disabled");
                        x.classList.remove("disabled");
                    })
                    target.querySelector(".spinner-border-wrap").classList.add("opacity");
                }, 1500)
            }
        })
        .catch(err => console.log(err))
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

    if (!document.cookie.includes("user")) {
        favoritesStatus("請先註冊/登入!");
        createLoginRegister();
        countFavoritesAmount(0);
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
        clearWarn(x);
        if (x.value.length == 0 || x.tagName.toLowerCase() === "select" && x.value === "-1") {
            x.classList.add("input-warn");
        }
    })
});
