////全域宣告區

//import { ajax } from "jquery";

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
var addfavoritebtn = document.getElementById("addfavorite-btn");
var definenamebtn = document.getElementById("definename-btn");
var allcartbtn = document.getElementsByName("cartbtn");



////載入區
window.onload = function () {
    setMenuContorl()
    viewModeSwitch()
    shopModeSwitch()
    setViewedContorl()
    showModule()
    createPackageObj()
    getPackageProductId()
    createViewedPic()
    addToCart()

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
        countprice.innerText = `${totalprice}元`;
    }
}

//創造物件
function createObject(itemroomtypevalue, itemsquarefeetvalue, GUIDvalue, itemserviceitem) {
    var item = {
        RoomType: parseInt(itemroomtypevalue), Squarefeet: parseInt(itemsquarefeetvalue), ServiceItem: itemserviceitem.toString(), GUID: GUIDvalue
    }
    userdefinedarray.push(item)
}

//創造GUID 
function createGUID() {

    GUID = Math.floor((1 + Math.random()) * 1000000000).toString().substring(1);
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
    cloneContent.getElementById("temple-serviceitem").innerHTML = `服務內容 : ${serviceschinese.toString()}`;
    cloneContent.getElementById("temple-hour").innerHTML = `花費時間 : ${countHour(parseInt(document.querySelector('input[name="roomtype"]:checked').value), parseInt(document.querySelector('input[name="squarefeet"]:checked').value))}小時`
    createGUID()
    let tempguid = GUID
    createObject(roomtypeorginal, squarefeetorginal, tempguid, serviceschinese)
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
    toastr.success("根據空間類型搜尋結果...")
    cleanSelected()
}

//顯示出所有檔案
function showAllCard() {
    let cardarray = document.getElementsByName("card");
    cardarray.forEach(x => {
        if ($(x).hasClass("d-none")) {
            x.classList.remove("d-none")
        }

    })

}
//清空勾選欄
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
//以空間大小過濾商品
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
    toastr.success("根據空間大小搜尋結果...")
    cleanSelected()
}
//以服務項目過濾商品
function fliterCardByServiceItem() {
    showAllCard()
    let cardarray = document.getElementsByName("card");
    cardarray.forEach(x => {
        let items = document.getElementsByName("serviceitem")
        let serviceitems = new Array();
        for (var i = 0; i < items.length; i++) {
            if (items[i].checked) {
                serviceitems.push(items[i].value)
            }
        }
        let serviceschineseArray = new Array();
        serviceitems.forEach(item => {
            serviceschineseArray.push(serviceitemsarray[item])
        });
        let temp = x.getAttribute("data-Serviceitem")
        serviceschineseArray.forEach(y => {
            if (!temp.includes(y)) {
                x.classList.add("d-none");
            }

        })
    })
    toastr.success("根據服務項目搜尋結果...")

    cleanSelected()
}



//將組合的資料傳去Controller
function postUserDefineData(tempitem) {
    let url = "/ProductPage/CreateUserDefinedData"
    var data = { UserDefinedAlls: tempitem }
    fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        })
    }).then(res => res.json())
        .then(result => {

            if (result.response == "success") {
                toastr.success("已將商品加入收藏!!!")
                setTimeout(() => {
                    cleanView()
                }, 1000)
                console.log('Success:', response)

            }
        }
        )
        .catch(error => console.error(error))
}


//清除自訂組合的畫面及陣列
function cleanView() {

    $(userdefinedbox).empty();
    userdefinedarray = [];
    document.getElementById("countprice").innerText = ""
    checkCartIsEmpty()
}

//彈出取名的Module
function showModule() {
    addfavoritebtn.addEventListener("click", function () {
        if (!document.cookie.includes("user")) {
            alert("目前還沒登入喔!")
            window.location.assign("/Account/Login")
        }
        else if (userdefinedarray.length == 0) {
            toastr.warning("目前還沒有商品喔!")
        }
        else {

            this.setAttribute("data-toggle", "modal");
            this.setAttribute("data-target", "#titlemodal");
        }
    })
}
//幫自訂義商品取名，傳去後端
function createPackageObj() {
    definenamebtn.addEventListener("click", function () {
        if (modalinput.innerText = "") {
            toastr.warning("目前還沒登入喔!")
        }
        else {
            var Title = document.getElementById("modalinput").value;


            userdefinedarray.forEach(x => {
                x.Title = Title;
            })

            postUserDefineData(userdefinedarray)


        }
    })
}
//取得套裝產品ID
function getPackageProductId() {
    $("button[name='cartbtn']").click(function () {

        let tempPackageProductId = $(this).attr("id");
        if (!document.cookie.includes("user")) {
            toastr.warning("目前還沒登入喔!")
        }
        else {
            postCreateFavoriteData(tempPackageProductId)
        }

    })
}
//傳套裝產品的Json資料去後端建立收藏
function postCreateFavoriteData(value) {
    let url = "/ProductPage/CreateFavoriteData"
    var data = { PackageProductId: value }
    fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        })
    }).then(res => res.json())
        .then(result => {

            if (result.response == "success") {
                setTimeout(() => {

                    getFavorites()
                    toastr.success("已將商品加入收藏!!!")
                }, 1000)
                console.log('Success:', response)

            }
        }
        )

        .catch(error => console.error('Error:', error))
}

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
    let localData = JSON.parse(localStorage.getItem('key'))
    if (localData == null) {
        let viewArray = [];
        let temp = {
            Id: src
        }
        viewArray.push(temp)
        localStorage.setItem('key', JSON.stringify(viewArray))
    }
    else {
        let temp = {
            Id: src
        }
        localData.push(temp)
        localStorage.setItem('key', JSON.stringify(localData))

    }
}
//創造瀏覽過的商品
function createViewedPic() {
    var temp = JSON.parse(localStorage.getItem("key"))
    if (Array.isArray(temp)) {
        temp.forEach(
            x => {
                let ViewedBox = document.getElementById("viewed-box")
                let ViewedPic = document.createElement("div")
                ViewedPic.setAttribute("class", "pic")
                let Viewalink = document.createElement("a")
                let Viewedimg = document.createElement("img")
                Viewedimg.src = x.Id
                Viewalink.href = x.Url
                Viewalink.appendChild(Viewedimg)
                ViewedPic.appendChild(Viewalink)
                ViewedBox.appendChild(ViewedPic)
            }
        )
    }
}






//加入收藏頁特效
function addToCart() {
    var $ball = document.getElementById('ball');
    allcartbtn.forEach(x => x.onclick = function (evt) {

        if (!document.cookie.includes("user")) {
            return;
        }
        else {
            $ball.style.top = evt.pageY + 'px';
            $ball.style.left = evt.pageX + 'px';
            $ball.style.transition = 'left 0s, top 0s';
            setTimeout(() => {
                $ball.style.opacity = '1';
                $ball.style.top = '24px';
                $ball.style.left = '1387px';
                $ball.style.fontSize = '22px';
                $ball.style.Color = "black";
                $ball.style.transition = 'left 1.5s , top 1.2s ease-in';
            }, 20)
            setTimeout(() => {
                $ball.style.opacity = '0';
            }, 2000)
        }
    })
}





