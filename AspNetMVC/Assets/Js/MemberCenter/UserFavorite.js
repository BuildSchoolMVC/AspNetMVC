////window.onload = function () {

////    setTimeout(() => {
////        FavoriteArea.deleteFromFavorite();

////    }, 1500)

////}
var tempUserDefinedId;
var tempname;
var FavoriteArea = new Vue({

    el: '#app',
    data: {
        IsDelete: true,
        active: '',
        RoomType: '',
        SquareFeet: '',
        IsPackage: true,
        item1: "空間類型",
        item2: "空間大小",
        item3: "服務項目",
        button1: "前往結帳",
        button2: "修改收藏",
        button3: "刪除收藏",
        ispackage: false,
        DataArray: {
            RequestUrl: '/ProductPage/SearchForFavorite',
            FavoriteDataArray: [],


        },
        SameUserDefindIdProductData: {
            RequestUrl: '/ProductPage/SearchAllUserDefinedByFavoriteId',
            ModifyRequestUrl: '/ProductPage/ModifyUserDefinedByUserDefindId',
            ProductArray: []
        },
        Roomtypeoptions: [
            { name: '廚房', value: 0, photo: 'https://i.imgur.com/HQwLxRh.jpg' },
            { name: '客廳', value: 1, photo: 'https://i.imgur.com/Tvcj3OR.jpg' },
            { name: '臥室', value: 2, photo: 'https://i.imgur.com/7hTQ5xR.jpg' },
            { name: '浴廁', value: 3, photo: 'https://i.imgur.com/7Z8nhs9.jpg' },
            { name: '陽台', value: 4, photo: 'https://i.imgur.com/LewIn3G.jpg' }
        ],
        Squareoptions: [
            { name: '5坪以下', value: 0 },
            { name: '6-10坪', value: 1 },
            { name: '11-15坪', value: 2 },
            { name: '16坪以上', value: 3 },
        ],

    },
    created: function () {
        this.getData();
    },
    methods: {
        getData() {
            axios.post(this.DataArray.RequestUrl)
                .then(res => {

                    if (Array.isArray(res.data)) {
                        this.DataArray.FavoriteDataArray = res.data.map(x => (
                            {
                                Price: x.Data.map(y => parseInt(y.Price)).reduce(function (accumulator, currentValue) {
                                    return accumulator + currentValue
                                }),
                                Hour: x.Data.map(y => y.Hour).toString(),
                                Title: x.Data[0].Title,
                                SquareFeet: x.IsPackage == true ? x.Data.map(y => y.Squarefeet).toString().split(',') : x.Data.map(y => squarefeetSwitch(y.Squarefeet)).toString().split(','),
                                ServiceItem: x.IsPackage == true ? x.Data.map(y => y.ServiceItem).toString().split("+").join("、") : Array.from(new Set(x.Data.map(y => y.ServiceItem).toString().split(",").filter(x => x != ""))).join("、"),
                                RoomType: x.IsPackage == true ? x.Data.map(y => y.RoomType).toString().split(',') : x.Data.map(y => roomTypeSwitch(y.RoomType)).toString().split(','),
                                PhotoUrl: x.Data.map(y => y.PhotoUrl),
                                FavoriteId: x.FavoriteId,
                                IsPackage: x.IsPackage
                            }))

                    }
                })
        },
        getSameUserDefindIdProduct(target) {
            var UserdefindedId = target.target.id.replace("modiftybtn", "")
            axios.post(this.SameUserDefindIdProductData.RequestUrl, { favoriteid: UserdefindedId })
                .then(res => {
                    console.log(res.data)
                    if (Array.isArray(res.data)) {
                        tempUserDefinedId = res.data[0].UserDefinedId;
                        tempname = res.data[0].Title;
                        this.SameUserDefindIdProductData.ProductArray = res.data.map(x => (
                            {
                                UserDefinedProductId: x.UserDefinedProductId,
                                IsDelete: x.IsDelete,
                                Name: x.Title,
                                UserDefinedId: x.UserDefinedId,
                                RoomType: x.RoomType,
                                SquareFeet: x.Squarefeet,
                                ServiceItem: {
                                    Clean: x.ServiceItem.includes("清潔"),
                                    Storage: x.ServiceItem.includes("收納"),
                                    Deworming: x.ServiceItem.includes("除蟲")
                                }
                            }))
                    }
                    console.log(this.SameUserDefindIdProductData.ProductArray)
                })
        },
        updateUserDefindData() {
            var tempArray = this.SameUserDefindIdProductData.ProductArray.map(x => (
                {
                    UserDefinedProductId: x.UserDefinedProductId,
                    UserDefinedId: x.UserDefinedId,
                    IsDelete: x.IsDelete,
                    Title: x.Name,
                    RoomType: x.RoomType,
                    SquareFeet: x.SquareFeet,
                    ServiceItem: (CleanSwitch(x.ServiceItem.Clean) + StorageSwitch(x.ServiceItem.Storage) + DewormingSwitch(x.ServiceItem.Deworming)).slice(0, -1)
                }
            ))
            axios.post(this.SameUserDefindIdProductData.ModifyRequestUrl, { userDefinedall: tempArray })
                .then(response => {
                    console.log(response)
                    if (response.data.response == "success") {
                        toastr.success("修改成功!!!")
                        setTimeout(() => {
                            document.getElementById("modiftyModal").click()
                            this.getData();
                            getFavorites()
                        }, 1000)
                        console.log('Success:', response.response)

                    }
                }
                )
        },
        addData() {
            var tempItem = {
                UserDefinedProductId: "", UserDefinedId: tempUserDefinedId, Name: tempname, RoomType: 0, SquareFeet: 0, IsDelete: false, ServiceItem: {
                    Clean: true,
                    Storage: true,
                    Deworming: true
                }
            }
            this.SameUserDefindIdProductData.ProductArray.unshift(tempItem)
        },
        setIsDeletePropotity(target) {
            var deleteItemId = target.target.id.replace("deletebtn", "")
            var temp = this.SameUserDefindIdProductData.ProductArray.find(x => x.UserDefinedProductId == deleteItemId)
            if (!temp.IsDelete) {
                temp.IsDelete = true;

            }
            else {
                temp.IsDelete = false;
            }

        },
        deleteFromFavorite(id, event) {
            postFavoriteId(id)
          setTimeout(() => {
              FavoriteArea.deleteDataFromFavorite(id)
          }, 1500)
                
        },

        deleteDataFromFavorite(temp) {

            var tempindex = this.DataArray.FavoriteDataArray.findIndex(x => x.FavoriteId == temp)
            this.DataArray.FavoriteDataArray.splice(tempindex, 1)

        },
        createGUID() {
            GUID = Math.floor((1 + Math.random()) * 1000000000).toString().substring(1)
            return GUID
        }
    }
})



//將組合的資料傳去Controller
function postFavoriteId(tempitem) {
    let url = "/ProductPage/DeleteFavorite"
    var data = { favoriteId: tempitem }
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
                getFavorites()
                toastr.success("移除成功!!!")

                console.log('Success:', result.response)

            }
        }
        )
        .catch(error => console.error(error))
}

function showModal(event) {

    event.target.setAttribute("data-toggle", "modal");
    event.target.setAttribute("data-target", "#modiftyModal");
    //setTimeout(() => {
    //    setDisplayNone()
    //}, 1000)
}

const CleanSwitch = value =>
    value == true ? "清潔," : ""

const StorageSwitch = value =>
    value == true ? "收納," : ""

const DewormingSwitch = value =>
    value == true ? "除蟲," : ""
