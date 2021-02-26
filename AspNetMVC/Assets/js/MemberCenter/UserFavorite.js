var FavoriteArea = new Vue({

    el: '#Favorite-Area',
    data: {
        item1:"空間類型",
        item2:"空間大小",
        item3:"服務項目",
        button1:"前往結帳",
        button2:"修改收藏",
        button3: "刪除收藏",
        ispackage:flase,
        DataArray: {
            pokemonRequestUrl: '/ProductPage/SearchForFavorite',
            pokemonArray: [],
            cardArray: [],
            currentPokemon: {

            }
        },

    },
    created: function () {
        this.getData();
    }
    computed: {},
    methods: {
        getData() {
            axios.get()
        }
  
    },
    components: {
        
    }

})