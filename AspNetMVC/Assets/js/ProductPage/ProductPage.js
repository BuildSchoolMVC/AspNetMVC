////全域宣告區
//取勾選值
var sidemenubtn = document.getElementById("sidemenu-contorl");
var operatingareabtn = document.getElementById("operating-area-btn");
var sidemenu = document.getElementById("sidemenu");
var itemgroupgrid = document.getElementById("item-group-grid");
var itemgrouplist = document.getElementById("item-group-list");
var gridbtn = document.getElementById("grid-btn");
var listbtn = document.getElementById("list-btn");
var todefinebtn = document.getElementById("todefinebutton");
var topackagebtn = document.getElementById("topackagebutton")
var packageproduct = document.getElementById("packageproduct")
var definedproduct = document.getElementById("definedproduct")
var viewedarea = document.getElementById("viewed-area")
var viewedareabtn = document.getElementById("viewed-area-btn")
////載入區
window.onload = function () {
    setMenuContorl()
    viewModeSwitch()
    shopModeSwitch()
    setViewedContorl()
}

////操作區

//側邊欄關閉
function sideMenuHidden() {
    operatingareabtn.style.transform = "scale(-1)"
    sidemenu.classList.add("close")
    operatingareabtn.classList.add("close")
}
//側邊欄開啟
function sideMenuShow() {
    operatingareabtn.style.transform = "scale(1)"
    sidemenu.classList.remove("close");
    operatingareabtn.classList.remove("close")
}


//側邊欄監聽
function setMenuContorl() {
    if (window.innerWidth >= 768) {
        sideMenuShow()
    }
}
//監聽視窗尺寸
window.addEventListener("resize", function () {
    setViewedContorl()
    viewedAreaHidden()
    if (window.innerWidth >= 768) {
        sideMenuHidden()
        sideMenuShow()

    }
})
//監聽側邊欄按鈕
sidemenubtn.addEventListener("click", function () {
    if ($(sidemenu).hasClass("close")) {
        sideMenuShow()
    } else {
        sideMenuHidden()
    }
})




////商品區
//切換Grid/List

//用Grid顯示
function viewGrid() {
    itemgrouplist.classList.add("d-none")
    itemgroupgrid.classList.remove("d-none")
}
//用List顯示
function viewList() {
    itemgrouplist.classList.remove("d-none")
    itemgroupgrid.classList.add("d-none")
}

//Grid/List監聽
function viewModeSwitch() {
    gridbtn.addEventListener("click", function () {

        if (!$(itemgrouplist).hasClass("d-none")) {
            viewGrid()
        }

    })

    listbtn.addEventListener("click", function () {
        if (!$(itemgroupgrid).hasClass("d-none")) {
            viewList()
        }
    })

}




////轉換區+瀏覽區

function setViewedContorl() {
    if (window.innerWidth <= 768) {
        viewedareabtn.addEventListener("click", function () {
            if ($(viewedarea).hasClass("open")) {
                viewedAreaHidden()
            } else {
                viewedAreaShow()
            }
        })
    }
}

//瀏覽區監聽
// viewedareabtn.addEventListener("click", function () {
//     if ($(viewedarea).hasClass("open")) {
//         viewedAreaHidden()
//     } else {
//         viewedAreaShow()
//     }
// })

//瀏覽區打開
function viewedAreaShow() {
    viewedarea.classList.add("open");
}
//瀏覽區關閉
function viewedAreaHidden() {
    viewedarea.classList.remove("open");
}




//切換套裝/客製化
function shopInDefine() {
    packageproduct.classList.add("d-none")
    todefinebtn.classList.add("d-none")
    definedproduct.classList.remove("d-none")
    topackagebtn.classList.remove("d-none")
}
//用List顯示
function shopInPackage() {
    packageproduct.classList.remove("d-none")
    todefinebtn.classList.remove("d-none")
    definedproduct.classList.add("d-none")
    topackagebtn.classList.add("d-none")
}
//購物模式切換
function shopModeSwitch() {
    todefinebtn.addEventListener("click", function () {

        if (!$(packageproduct).hasClass("d-none")) {
            shopInDefine()
        }

    })

    topackagebtn.addEventListener("click", function () {
        if (!$(definedproduct).hasClass("d-none")) {
            shopInPackage()
        }
    })

}