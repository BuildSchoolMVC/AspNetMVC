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
        if (isLogin == false) {
            toastr.warning("請先註冊或登入!!!");
            return;
        }
        let price = +document.querySelector(".section_product .content-footer .price").textContent.replace(",", "");
        let title = document.querySelector(".section_product .content-header h2").textContent;
        let id = document.querySelectorAll(".favorites-product-item").length == 0 ? 0 : Math.max(...Array.from(document.querySelectorAll(".favorites-product-item")).map(x => +x.dataset.id));
        createFavoritesCard(price, title, ["傢俱、櫥櫃外觀擦拭", "地面拖地", "地面除塵清潔", "櫃外擦拭"], id + 1);

        favorites.push({
            price,
            title,
            items: ["傢俱、櫥櫃外觀擦拭", "地面拖地", "地面除塵清潔", "櫃外擦拭"],
            id: id + 1
        });
        localStorage.setItem("favorites", JSON.stringify(favorites));
        countFavoritesAmount(favorites.length);
        swipeDeleteEffect();
        checkoutBtnControl();
        toggleTip();
        toastr.success("成功加入至購物車!");
    })
}


window.addEventListener("load", () => {
    switchTabs();
    addFavorites();
})