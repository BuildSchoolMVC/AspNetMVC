﻿////全域宣告區
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
var searchproductbtn = document.getElementById("searchproduct-btn");
var createcardbtn = document.getElementById("createcard-btn")
var userdefinedbox = document.getElementById("userdefinedbox")
var userdefinedarray = [];
var roomtypearray = ["廚房", "客廳", "臥室", "浴廁", "陽台"]
var roompicarray = ["kitchen", "livingroom", "bedroom", "bathroom", "balcony"]
var squarefeetarray = ["5坪以下", "6-10坪", "11-15坪", "16坪以上"]
var serviceitemsarray = ["清潔", "收納", "除蟲"]
var hourprice = 500;
var basehour = 1;
var unit = 0.5;
var hour = 0;
var totalprice = 0;
var pointoutarea = document.getElementById("pointout-area")
var GUID;
var searchbyroombtn = document.getElementById("searchbyroom-btn");
var searchbysquarebtn = document.getElementById("searchbysquare-btn");


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
    viewedAreaHidden()
    setViewedContorl()
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
        viewedareabtn.onclick = function () {
            if ($(viewedarea).hasClass("open")) {
                viewedAreaHidden()
            } else {
                viewedAreaShow()
            }
        }
    } else {
        // viewedareabtn.removeEventListener("click");
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

createcardbtn.onclick = function () {
    createCard();
    checkCartIsEmpty()
}


////切換套裝/客製化
//用客製化

function shopInDefine() {
    packageproduct.classList.add("d-none")
    todefinebtn.classList.add("d-none")
    definedproduct.classList.remove("d-none")
    topackagebtn.classList.remove("d-none")
    searchproductbtn.classList.add("d-none")
    createcardbtn.classList.remove("d-none")
}
//用套裝顯示
function shopInPackage() {
    packageproduct.classList.remove("d-none")
    todefinebtn.classList.remove("d-none")
    definedproduct.classList.add("d-none")
    topackagebtn.classList.add("d-none")
    searchproductbtn.classList.remove("d-none")
    createcardbtn.classList.add("d-none")

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

////抓取radiobutton值產生卡片

//抓取radiobutton值

function countHour(Roomtype, Squarefeet) {

    if (Roomtype <= 2) {
        hour = basehour
    } else {
        hour = basehour / 2
    }
    hour += Squarefeet * unit
    return hour;

}

//計價
function countPrice() {
    let countprice = document.getElementById("countprice")
    let pricearray = Array.from(document.querySelectorAll(".itemprice")).map(x => x.innerHTML);
    if (pricearray == 0) {
        countprice.innerText = "NT:$元";
    }
    else {
        let totalprice = pricearray.map(x => parseInt(x.replace("$:", ""))).reduce(function (accumulator, currentValue) {
            return accumulator + currentValue
        })
        countprice.innerText = `NT:$ ${totalprice}元`;
    }
}

//創造物件
function createObject(itemroomtypevalue, itemsquarefeetvalue, GUIDvalue, itemserviceitem) {
    var item = {
        roomtype: parseInt(itemroomtypevalue), squarefeet: parseInt(itemsquarefeetvalue), serviceitem: itemserviceitem, GUID: GUIDvalue
    }
    userdefinedarray.push(item)
}

//創造GUID 
function createGUID() {

    GUID = Math.floor((1 + Math.random()) * 1000000).toString().substring(1);
    return GUID
}

//將物件從陣列中移除
function removeItemFromArray(itemsGUID) {
    var itemsindex = userdefinedarray.findIndex(x => x.GUID == itemsGUID)
    userdefinedarray.splice(itemsindex, 1)
}


//產生卡片
function createCard() {

    let roomtypeorginal = document.querySelector('input[name="roomtype"]:checked').value
    let roomtypevalue = roomtypearray[roomtypeorginal];
    let squarefeetorginal = document.querySelector('input[name="squarefeet"]:checked').value;
    let squarefeetvalue = squarefeetarray[squarefeetorginal];
    let items = document.getElementsByName("serviceitem")
    let serviceitemorginal = new Array();
    let serviceschinese = new Array();
    for (var i = 0; i < items.length; i++) {
        if (items[i].checked) {
            serviceitemorginal.push(items[i].value)
        }
    }
    let serviceitemvalue = serviceitemorginal.forEach(x => {
        serviceschinese.push(serviceitemsarray[x])
    });
    let card = document.getElementById("userDefinedCard")
    cloneContent = card.content.cloneNode(true);
    cloneContent.getElementById("temple-title").innerHTML = `${roomtypevalue}清潔<span class="itemprice">$:${countHour(parseInt(document.querySelector('input[name="roomtype"]:checked').value), parseInt(document.querySelector('input[name="squarefeet"]:checked').value)) * hourprice}</span>`;
    cloneContent.getElementById("temple-squarefeet").innerHTML = `坪數大小 : ${squarefeetvalue}`;
    cloneContent.getElementById("temple-img").src = `../../Assets/images/${roompicarray[document.querySelector('input[name="roomtype"]:checked').value]}.png`
    cloneContent.getElementById("temple-serviceitem").innerHTML = `服務內容 : ${serviceschinese}`;
    cloneContent.getElementById("temple-hour").innerHTML = `花費時間 : ${countHour(parseInt(document.querySelector('input[name="roomtype"]:checked').value), parseInt(document.querySelector('input[name="squarefeet"]:checked').value))}小時`
    createGUID()
    let tempguid = GUID
    createObject(roomtypeorginal, squarefeetorginal, tempguid, serviceitemorginal)
    cloneContent.getElementById("temple-deletebtn").onclick = function () {
        $(this).parent().parent().remove()
        countPrice()
        checkCartIsEmpty()
        console.log(GUID)
        removeItemFromArray(tempguid)
    }
    userdefinedbox.append(cloneContent);
    countPrice()
}

//顯示及隱藏購物車為空的頁面
function checkCartIsEmpty() {

    if (!userdefinedbox.childElementCount == 0) {
        pointoutarea.classList.add("d-none")
        userdefinedbox.classList.remove("d-none")
    }
    else {
        pointoutarea.classList.remove("d-none")
        userdefinedbox.classList.add("d-none")
    }
}


//過濾卡片
function fliterCardByRoomType() {
    showAllCard()
    let cardarray = document.getElementsByName("card");
    cardarray.forEach(x => {
        let roomtypevalue = document.querySelector('input[name="roomtype"]:checked').value
        let temproom1 = x.getAttribute("data-roomtypevalue")
        let temproom2 = x.getAttribute("data-roomtypevalue2")
        let temproom3 = x.getAttribute("data-roomtypevalue3")
        if (x.hasAttribute("data-roomtypevalue3")) {
            if (roomtypevalue != temproom1 && roomtypevalue != temproom2 && roomtypevalue != temproom3) {
                x.classList.add("d-none");
            }
            else {
                if (roomtypevalue != temproom1 && roomtypevalue != temproom2) {
                    x.classList.add("d-none");
                }
            }

        }
    })
    cleanSelected()
}

function showAllCard() {
    let cardarray = document.getElementsByName("card");
    cardarray.forEach(x => {
        if ($(x).hasClass("d-none")) {
            x.classList.remove("d-none")
        }

    })

}

function cleanSelected() {
    let roomtyperadiobtn = document.querySelectorAll('input[name="roomtype"]');
    for (var i = 0; i < roomtyperadiobtn.length; i++) {
        roomtyperadiobtn[i].checked = false;
    }
    let squarefeetradiobtn = document.querySelectorAll('input[name="squarefeet"]');
    for (var i = 0; i < squarefeetradiobtn.length; i++) {
        squarefeetradiobtn[i].checked = false;
    }
    let serviceitemcheckbox = document.querySelectorAll('input[name="serviceitem"]');
    for (var i = 0; i < serviceitemcheckbox.length; i++) {
        serviceitemcheckbox[i].checked = false;
    }
}

function fliterCardBySquareFeet() {
    showAllCard()
    let cardarray = document.getElementsByName("card");
    cardarray.forEach(x => {
        let squarefeetvalue = document.querySelector('input[name="squarefeet"]:checked').value
        let tempsquare1 = x.getAttribute("data-SquareValue")
        let tempsquare2 = x.getAttribute("data-SquareValue2")
        let tempsquare3 = x.getAttribute("data-SquareValue3")
        if (x.hasAttribute("data-SquareValue3")) {
            if (squarefeetvalue != tempsquare1 && squarefeetvalue != tempsquare2 && squarefeetvalue != tempsquare3) {
                x.classList.add("d-none");
            }
            else {
                if (squarefeetvalue != tempsquare1 && squarefeetvalue != tempsquare2) {
                    x.classList.add("d-none");
                }
            }

        }
    })
    cleanSelected()
}

function fliterCardByServiceItem() {
    showAllCard()
    let cardarray = document.getElementsByName("card");
    cardarray.forEach(x => {
        let items = document.getElementsByName("serviceitem")
        let serviceitemorginal = new Array();
        let serviceschinese = new Array();
        for (var i = 0; i < items.length; i++) {
            if (items[i].checked) {
                serviceitemorginal.push(items[i].value)
            }
        }
        let serviceitemvalue = serviceitemorginal.forEach(x => {
            serviceschinese.push(serviceitemsarray[x])
        });
        let temp = x.getAttribute("data-Serviceitem")
        if (temp.Contains()) {

            x.classList.add("d-none");
        }
    })
  
cleanSelected()
}




