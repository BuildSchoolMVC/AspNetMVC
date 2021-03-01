window.onload = function () {
    deleteFromFavorite();
}

var FavoriteArea = new Vue({

    el: '#app',
    data: {
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

    },
    created: function () {
        this.getData();
        deleteFromFavorite();
    },
    methods: {
        getData() {
            axios.post(this.DataArray.RequestUrl)
                .then(res => {
                    console.log(res.data)
                    if (Array.isArray(res.data)) {
                        this.DataArray.FavoriteDataArray = res.data.map(x => (
                            {
                                Price: x.Data.map(y => parseInt(y.Price)).reduce(function (accumulator, currentValue) {
                                    return accumulator + currentValue
                                }),
                                Hour: x.Data.map(y => y.Hour).toString(),
                                Title: x.Data[0].Title,
                                SquareFeet: x.IsPackage == true ? x.Data.map(y => y.Squarefeet).toString().split(',') : x.Data.map(y => squarefeetSwitch(y.Squarefeet)).toString().split(',') ,
                                ServiceItem: x.Data.map(y => y.ServiceItem).toString(),
                                RoomType: x.IsPackage == true ? x.Data.map(y => y.RoomType).toString().split(',') : x.Data.map(y => roomTypeSwitch(y.RoomType)).toString().split(','),
                                PhotoUrl: x.Data.map(y => y.PhotoUrl),
                                FavoriteId: x.FavoriteId,
                                IsPackage: x.IsPackage
                            //Price: x.Data[0].Price,
                            //Hour: x.Data[0].Hour,
                            //Title: x.Data[0].Title,
                            //SquareFeet: x.Data[0].Squarefeet,
                            //ServiceItem: x.Data[0].ServiceItem,
                            //RoomType: x.Data[0].RoomType,
                            //PhotoUrl: x.Data[0].PhotoUrl,
                            //FavoriteId: x.FavoriteId
                        }))
                        console.log(this.DataArray.FavoriteDataArray)
                    }
                })
        },


    }



})

function deleteFromFavorite() {
    $("a[name='deletebtn']").click(function () {
        let tempFavoriteProductId = $(this).attr("id").replace("deletebtn","");
        postFavoriteId(tempFavoriteProductId);
        setTimeout(() => {
            $(this).parent().parent().empty();
        }, 1500)



    })
}

//將組合的資料傳去Controller
function postFavoriteId(tempitem) {
    let url = "/ProductPage/DeleteFavorite"
    var data = { favoriteId: tempitem}
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
