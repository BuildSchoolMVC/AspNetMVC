let star = 0;
let commentArea = document.querySelector(".comment-area textarea");
let commentBtn = document.querySelector("#commentBtn");
let commentData = {};

const switchTabs = () => {
    document.querySelectorAll(".product-tabs .nav-link").forEach((x, index) => {
        x.addEventListener("click", () => {
            //切換tabs
            document.querySelectorAll(".product-tabs .nav-link").forEach(y => {
                if (y.classList.contains("active")) {
                    y.classList.remove("active");
                }
            })

            x.classList.add("active");
            //切換頁面
            document.querySelectorAll(".product-tab-item").forEach(z => {
                if (z.classList.contains("active")) {
                    z.classList.remove("active");
                }
            })
            document.querySelectorAll(".product-tab-item")[index].classList.add("active");
        })
    })
}
const addFavorites = () => {
    document.querySelector(".content-footer .add-favorites").addEventListener("click", function () {
        if (getCookieName("user") == undefined) {
            toastr.warning("請先註冊或登入!!!");
            return;
        }
        let titles = Array.from(document.querySelectorAll(".favorites-product-item h3")).map(x => x.textContent)
        let addItems = document.querySelector(".section_product h1").textContent;
        if (titles.includes(addItems)) {
            toastr.warning("已經加入至收藏!!!")
            return;
        }

        let url = "/ProductPage/CreateFavoriteData";
        let packageproducid = document.querySelector(".title").dataset.id;
        let data = { PackageProductId: packageproducid };

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
                if (result.response) {
                    toastr.success("成功加入至收藏!");
                    getFavorites();
                }
            })
            .catch(err => console.log(err))
    })
}



const hoverStar = () => {
    let stars = document.querySelectorAll(".star-grouping i");
    stars.forEach((x, y) => {
        x.addEventListener("mouseover", function () {
            stars.forEach(z => {
                if (z.classList.contains("color-yellow")) z.classList.remove("color-yellow");
            })
            for (let i = 0; i <= y; i++) {
                stars[i].classList.add("color-yellow");
            }
        })
    })
}
const selectStar = () => {
    let stars = document.querySelectorAll(".star-grouping i");
    stars.forEach((x, y) => {
        x.addEventListener("click", function () {
            stars.forEach(z => {
                if (z.classList.contains("selected")) z.classList.remove("selected");
            })
            for (let i = 0; i <= y; i++) {
                stars[i].classList.add("selected");
            }
            star = document.querySelectorAll(".selected").length;
            document.querySelector(".starcount").textContent = star;
        })
    })
}
const commentForm = () => {
    if (commentArea) {
        commentArea.addEventListener("change", function () {
            if (commentArea.value.length > 0) {
                commentBtn.removeAttribute("disabled");
            } else {
                commentBtn.setAttribute("disabled", "disabled");
            }
        })
    }
    if (commentBtn) {
        commentBtn.addEventListener("click", function () {
            if (star == 0) {
                toastr.warning("請填選星數!");
                return;
            } else if (star > 5) {
                toastr.warning("👿別亂搞👿");
                return;
            }
            $("#commentBtn .spinner-border-wrap").removeClass("opacity");

            let value = commentArea.value;
            let url = "/Detail/AddComment";
            let data = JSON.stringify({
                "PackageProductId": +document.querySelector("h1").dataset.id,
                "StarCount": star,
                "Comment": value
            })


            fetch(url, {
                method: "POST",
                body: data,
                headers: {
                    'Content-Type': 'application/json'
                },
            })
                .then(res => res.json())
                .then(result => {
                    if (result.status) {
                        getLatestComment();
                        resetCommentInput();
                        $(".spinner-border-wrap").addClass("opacity");
                    }
                })
                .catch(err => console.log(err))
        })
    }

}
const getLatestComment = () => {
    let id = document.querySelector("h1").dataset.id;
    let url = `/Detail/GetLatestComment?packageProductId=${id}`;

    fetch(url)
        .then(res => res.json())
        .then(res => {
            commentData = res;
            refreshComment();
        })
        .catch(err => console.log(err));
}
const refreshComment = () => {
    let comment = document.querySelector(".comment");
    let commentItem = document.createElement("div");
    let row = document.createElement("div");
    let wrap1 = document.createElement("div");
    let wrap2 = document.createElement("div");
    let wrap3 = document.createElement("div");
    let img = document.createElement("img");
    let p1 = document.createElement("p");
    let p2 = document.createElement("p");
    let span1 = document.createElement("span");
    let span2 = document.createElement("span");
    let span3 = document.createElement("span");
    let i = document.createElement("i");
    let br = document.createElement("br");
    let imgNo = Math.floor(Math.random() * 6) + 1;

    let date = new Date(+commentData.CreateTime.replace("/Date(", "").replace(")/", ""));

    let dateString = `${date.getFullYear()}/${date.getMonth() + 1}/${date.getDate()} ${date.getHours() > 12 ? "下午" : "上午"} ${date.getHours().toString().padStart(2, "0")}:${date.getMinutes().toString().padStart(2, "0")}:${date.getSeconds().toString().padStart(2, "0")}`

    commentItem.className = "comment-item";
    row.className = "row";

    wrap1.className = "col-2 pr-0 pl-4 d-flex justify-content-center align-items-center";
    wrap2.className = "col-6 pl-4";
    wrap3.className = "col-4 pr-4 d-flex align-items-center justify-content-center";
    img.src = `/Assets/images/p${imgNo}.jpg`;
    img.alt = "人物";
    img.className = "user rounded-circle d-block";
    wrap1.appendChild(img);

    p1.className = "comment-user";
    span1.className = "user";
    span1.textContent = `${commentData.AccountName.charAt(0)}${"*".repeat(8)}${commentData.AccountName.charAt(commentData.AccountName.length - 1)}`;
    span2.textContent = dateString;

    p1.append(span1, " 用戶於", br, span2);
    p2.textContent = commentData.Content;
    wrap2.append(p1, p2);

    span3.className = "comment-delete";
    span3.setAttribute("data-id", `${commentData.CommentId}`);
    span3.setAttribute("onclick", "deleteComment(this)");
    i.className = "fas fa-trash-alt";
    span3.appendChild(i);

    for (let j = 1; j <= 5; j++) {
        let icon = document.createElement("i");
        icon.className = "fas fa-star pr-1";
        if (commentData.Star >= j) icon.classList.add("color-yellow")
        else icon.classList.add("color-gray");

        wrap3.appendChild(icon);
    }
    row.append(wrap1, wrap2, wrap3);
    commentItem.append(span3, row);

    if (!document.querySelector(".comment-item")) {
        document.querySelector(".no-comment p").remove();
    }
    comment.prepend(commentItem);

    let commentCount = document.querySelector(".commentCount").textContent;
    document.querySelector(".commentCount").textContent = parseInt(commentCount) + 1;
}
const resetCommentInput = () => {
    commentArea.value = "";
    star = 0
    document.querySelector(".starcount").textContent = star;
    document.querySelectorAll(".star-grouping i").forEach(x => {
        if (x.classList.contains("selected")) x.classList.remove("selected");
        if (x.classList.contains("color-yellow")) x.classList.remove("color-yellow");
    })
    commentBtn.setAttribute("disabled", "disabled");
}
const deleteComment = (target) => {
    let commentId = target.dataset.id;
    let url = "/Detail/DeleteComment";
    let data = { id: commentId };
    fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(res => {
            if (res.status == 1) {
                target.parentNode.remove();
                document.querySelector(".commentCount").textContent = document.querySelectorAll(".comment-item").length;
                if (document.querySelectorAll(".comment-item").length == 0) {                    
                    let p = document.createElement("p");
                    p.textContent = "目前沒有評論";

                    document.querySelector(".no-comment").appendChild(p);
                }
            } else {
                toastr.error("發生錯誤!");
            }
        })
        .catch(err => console.log(err));
}

window.addEventListener("load", () => {
    switchTabs();
    addFavorites();
    hoverStar();
    selectStar();
    commentForm();
    getPicUrl();
})


//抓到單一商品頁圖片的Url
function getPicUrl() {

    var temp = this.document.getElementsByClassName("product-pic mb-2")
    if (temp == null) {
        return;

    }
    else {
        var viewedsrc = $(".product-pic.mb-2").children()[0].src;
        savePicData(viewedsrc)

    }
}

//將資料存到localStorage
function savePicData(src) {
    let localData = JSON.parse(localStorage.getItem("key"))
    let tempURL = document.URL;
    if (localData == null) {
        let viewArray = [];
        let temp = {
            Id: src,
            Url: tempURL
        }
        viewArray.push(temp)
        localStorage.setItem("key", JSON.stringify(viewArray))
    }
    else {
        let viewArray = JSON.parse(localStorage.getItem("key"))
        viewArray.forEach(x => {
            if (viewArray.map(z => z.Id).includes(src)) {
                return;
            }
            else {
                if (viewArray.length < 5) {
                    let temp = {
                        Id: src,
                        Url: tempURL
                    }
                    viewArray.push(temp)
                }
                else {
                    let temp = {
                        Id: src,
                        Url: tempURL
                    }
                    viewArray.splice(0, 1)
                    viewArray.push(temp)
                }
                localStorage.setItem("key", JSON.stringify(viewArray))
            }
        }
        )
    }
}