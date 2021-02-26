var FavoriteArea = new Vue({

    el: '#Favorite-Area',
    data: {
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
    },
    methods: {
        getData() {
            axios.post(this.data.DataArray.RequestUrl)
                .then(res => {
                    if (Array.isArray(res.data) && res.status == 200) {
                        this.DataArray.FavoriteDataArray = response.data.map((item) =>
                            ({
                                FavoriteId=item.FavoriteId,
                                IsPackage=item.IsPackage,
                                Data=item.Data
                            }))

                    }
                })
        }
    }



})