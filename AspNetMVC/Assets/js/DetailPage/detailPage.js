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
        if (document.querySelector(".wrap")) {
            document.querySelector(".wrap").remove();
        }
        if (getCookieName("user")==undefined) {
            toastr.warning("請先註冊或登入!!!");
            return;
        }
        let price = +document.querySelector(".section_product .content-footer .price").textContent.replace(",", "");
        let title = document.querySelector(".section_product .content-header h1").textContent;
        let url = document.querySelector(".product-pic img").src;
        let content = document.querySelector(".service-content").textContent;
        let info = document.querySelector(".place").textContent;
        let id = document.querySelector(".title").dataset.id;
        createFavoritesCard(price, title, url, content, info, id);

        favorites.push({
            price,
            title,
            url,
            content,
            info,
            id
        });
        localStorage.setItem("favorites", JSON.stringify(favorites));
        countFavoritesAmount(favorites.length);
        swipeDeleteEffect();
        checkoutBtnControl();
        toggleTip();
        toastr.success("成功加入至收藏!");
    })
}


window.addEventListener("load", () => {
    switchTabs();
    addFavorites();
})