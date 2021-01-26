////全域宣告區
//取勾選值
var sidemenubtn = document.getElementById("sidemenu-contorl");
var operatingareabtn = document.getElementById("operating-area-btn");
var sidemenu = document.getElementById("sidemenu");
var itemgroupgrid = document.getElementById("item-group-grid");
var itemgrouplist = document.getElementById("item-group-list");
var gridbtn = document.getElementById("grid-btn");
var listbtn = document.getElementById("list-btn");

////載入區
window.onload = function () {
    sideMenuContorl()
    viewModalSwitch()
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
function sideMenuContorl() {
    if (window.innerWidth >= 768) {
        sideMenuShow()
    } else {

        sidemenubtn.addEventListener("click", function () {

            if ($(sidemenu).hasClass("close")) {
                sideMenuShow()
            } else {
                sideMenuHidden()
            }
        })
    }
}




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
function viewModalSwitch() {
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

//切換套裝/客製化