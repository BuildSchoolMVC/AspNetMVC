const vueSideMenu = new Vue({
  el: '.container-memberCenter .side-menu',
  data: {
    PaidCount: 0,
    CouponCount: 0,
  },
  methods: {
    closeSideMenu() {
      document.getElementById('side-menu-check').checked = false;
    }
  },
});
// member info
const memberInfo = {
  isEditing: false,
  isSubmitting: false,
  AccountName: null,
  Phone: '0975831568',
  Email: 'sean@gmail.com',
  Address: '台北市信義區101大樓台北市信義區101大樓',
  isPhoneOk: true,
  isEmailOk: true,
  isAddressOk: true,
  errorMsgPhone: '',
  errorMsgEmail: '',
  errorMsgAddress: '',
  tempPhone: '',
  tempEmail: '',
  tempAddress: '',
};
const vueMemberInfo = new Vue({
  el: '.block#member-info',
  data: memberInfo,
  methods: {
    changeInfo() {
      this.isEditing = true;
      //備份資料
      this.tempPhone = this.Phone;
      this.tempEmail = this.Email;
      this.tempAddress = this.Address;
    },
    changeCancel() {
      this.isEditing = false;
      //恢復資料
      this.isPhoneOk = true;
      this.isEmailOk = true;
      this.isAddressOk = true;
      this.Phone = this.tempPhone;
      this.Email = this.tempEmail;
      this.Address = this.tempAddress;
    },
    changeOk() {
      this.isPhoneOk = true;
      this.isEmailOk = true;
      this.isAddressOk = true;

      if (!/^09\d{8}$/.test(this.Phone)) {
        this.errorMsgPhone = '請輸入正確手機號碼';
        this.isPhoneOk = false;
      }
      if (!/^[\w\.\-]+\@[\w\.\-]+$/.test(this.Email)) {
        this.errorMsgEmail = '請輸入正確信箱';
        this.isEmailOk = false;
      }
      if (!/^.+$/.test(this.Address)) {
        this.errorMsgAddress = '請輸入正確地址';
        this.isAddressOk = false;
      }
      if (this.isPhoneOk && this.isEmailOk && this.isAddressOk) {
        this.isSubmitting = true;
        axios.post('/MemberCenter/ChangeInfo', {
          Phone: this.Phone,
          Email: this.Email,
          Address: this.Address,
        }).then((res) => {
          if (res.data.IsSuccessful) {
            toastr.success('修改成功');
            this.isEditing = false;
          } else {
            toastr.error(`修改失敗，${res.data.Message}`);
          }
          this.isSubmitting = false;
        });
      }
    },
    clearInput() {
      changePassword.isOldPasswordOk = true;
      changePassword.isNewPasswordOk = true;
      changePassword.oldPassword = '';
      changePassword.newPassword = '';
      changePassword.newPassword2 = '';
    },
  }
});
const changePassword = {
  isSubmitting: false,
  oldPassword: '',
  newPassword: '',
  newPassword2: '',
  isOldPasswordOk: true,
  isNewPasswordOk: true,
  errorMsgOld: '',
  errorMsgNew: '',
};
const vueChangePassword = new Vue({
  el: '#modal-changePassword',
  data: changePassword,
  methods: {
    submitPassword() {
      this.isOldPasswordOk = true;
      this.isNewPasswordOk = true;

      if (this.newPassword != this.newPassword2) {
        this.errorMsgNew = '密碼不符';
        this.isNewPasswordOk = false;
      } else if (!/^[a-zA-Z\d]+$/.test(this.oldPassword)) {
        this.errorMsgOld = '請輸入原密碼';
        this.isOldPasswordOk = false;
      } else if (!/^[a-zA-Z\d]{6,15}$/.test(this.newPassword)) {
        this.errorMsgNew = '需6 ~ 15位';
        this.isNewPasswordOk = false;
      } else if (!/^[a-zA-Z\d]*[a-z][a-zA-Z\d]*$/.test(this.newPassword)) {
        this.errorMsgNew = '需至少一位小寫';
        this.isNewPasswordOk = false;
      } else if (!/^[a-zA-Z\d]*[A-Z][a-zA-Z\d]*$/.test(this.newPassword)) {
        this.errorMsgNew = '需至少一位大寫';
        this.isNewPasswordOk = false;
      } else {
        this.isSubmitting = true;
        axios.post('/MemberCenter/ChangePassword', {
          oldPassword: this.oldPassword,
          newPassword: this.newPassword,
        }).then((res) => {
          if (res.data.IsSuccessful) {
            toastr.success('修改成功');
            document.querySelector('#modal-changePassword .close').click();
          } else {
            toastr.error(`修改失敗，${res.data.Message}`);
          }
          this.isSubmitting = false;
        });
      }
    },
  }
});
// order
const RoomTypes = ['廚房', '客廳', '臥室', '浴廁', '陽台'];
const SquareFeets = ['5坪以下', '6-10坪', '11-15坪', '16坪以上'];
const InvoiceTypes = ['個人電子發票', '捐贈'];
const InvoiceDonateTos = ['中華民國唐氏症基金會', '陽光社會福利基金會', '台灣兒童暨家庭扶助基金會'];
const OrderStates = ['待付款', '已付款', '完成', '已取消'];

const vueBlockOrder = new Vue({
  el: '.block#order',
  data: {
    orderCount: 0,
    paidCount: 0,
    unpaidCount: 0,
    finishedCount: 0,
    focusOrders: null,
    focusOrderFull: null,
    focusPage: 1,
    isShowDetail: false,
    isLoading: false,
    isNoOrder: false,
    countPerPage: 3,
    commentOrderId: null,
    hoverRate: 0,
    rate: 0,
    comment: '',
    cancel: null,
  },
  methods: {
    changeTab(sort, page) {
      this.hideDetail();
      this.getOrderBrief(sort, page);
      this.$el.querySelector('.tab-pane.active').classList.add('show');
    },
    showDetail(orderId) {
      this.$el.querySelector('.tab-pane.show').classList.remove('show');
      this.isShowDetail = true;
      this.isLoading = true;
      axios.get(`/MemberCenter/GetOrderFull?orderId=${orderId}`)
        .then((res) => {
          this.focusOrderFull = res.data;
          this.focusOrderFull.OrderStateText = OrderStates[this.focusOrderFull.OrderState];
          this.focusOrderFull.InvoiceTypeText = InvoiceTypes[this.focusOrderFull.InvoiceType];
          if (this.focusOrderFull.InvoiceDonateTo != null) {
            this.focusOrderFull.InvoiceDonateToText = InvoiceDonateTos[this.focusOrderFull.InvoiceDonateTo];
          }
          if (this.focusOrderFull.PackageModel) {
            this.focusOrderFull.PackageModel.RoomTypes.forEach((value, i) => {
              this.focusOrderFull.PackageModel.RoomTypes[i] = RoomTypes[value];
            });
            this.focusOrderFull.PackageModel.SquareFeets.forEach((value, i) => {
              this.focusOrderFull.PackageModel.SquareFeets[i] = SquareFeets[value];
            });
          } else {
            this.focusOrderFull.UserDefinedList.forEach((item) => {
              item.RoomType = RoomTypes[item.RoomType];
              item.SquareFeet = SquareFeets[item.SquareFeet];
            });
          }
          this.isLoading = false;
        });
    },
    goBack() {
      this.hideDetail();
      this.$el.querySelector('.tab-pane.active').classList.add('show');
    },
    hideDetail() {
      this.isShowDetail = false;
      this.isLoading = false;
      this.focusOrderFull = null;
    },
    getOrderBrief(sort, page) {
      this.focusPage = page;
      this.focusOrders = null;
      this.isLoading = true;
      //頻繁點擊，下次請求前取消上次請求
      if (this.cancel) this.cancel();
      axios.get(`/MemberCenter/GetOrderBrief?sort=${sort}&page=${page}`, {
        cancelToken: new CancelToken(function executor(c) {
          vueBlockOrder.cancel = c;
        }),
      }).then((res) => {
        this.orderCount = res.data.orderCount;
        this.paidCount = res.data.paidCount;
        this.unpaidCount = res.data.unpaidCount;
        this.finishedCount = res.data.finishedCount;
        if (res.data.orderBriefs.length == 0) {
          this.isNoOrder = true;
        } else {
          this.isNoOrder = false;
          res.data.orderBriefs.forEach((item) => {
            item.date = item.DateService.split(',')[0];
            item.time = item.DateService.split(',')[1];
            item.OrderStateText = OrderStates[item.OrderState];
          });
          this.focusOrders = res.data.orderBriefs;
        }
        this.isLoading = false;
      }).catch(error => {
        console.log(error);
      });
    },
    changePage(sort, page) {
      if (this.focusPage != page) {
        this.focusPage = page;
        this.getOrderBrief(sort, page);
      }
    },
    lastPage(sort) {
      if (this.focusPage == 1) {
        return;
      }
      this.focusPage--;
      this.getOrderBrief(sort, this.focusPage);
    },
    nextPage(sort, sortCount) {
      if (this.focusPage == Math.ceil(sortCount / 3)) {
        return;
      }
      this.focusPage++;
      this.getOrderBrief(sort, this.focusPage);
    },
    clearComment(orderId) {
      this.commentOrderId = orderId;
      this.hoverRate = 0;
      this.rate = 0;
      this.comment = '';
    },
    submitComment() {
      if (this.rate == 0 && !this.comment) {
        toastr.warning('給個評價吧^^');
        return;
      }
      axios.post('/MemberCenter/SubmitComment', {
        OrderId: this.commentOrderId,
        Rate: this.rate,
        Comment: this.comment,
      }).then((res) => {
        if (res.data.IsSuccessful) {
          toastr.success('成功');
          document.querySelector('#modal-comment .close').click();
        } else {
          toastr.error(`提交失敗，${res.Message}`);
        }
      });
    },
    getECPayForm(orderId) {
      axios.get(`/Checkout/Repay?orderId=${orderId}`)
        .then((res) => {
          vueECPayForm.repay(res.data);
        });
    }
  },
  components: {
    'pagination': {
      props: ['sortCount', 'pageCount', 'focusPage', 'sort'],
      template: `
      <nav class="order-pagination">
        <ul class="pagination">
          <li class="page-item" @click="$emit('last-page', sort)">
            <i class="fas fa-angle-double-left page-link"></i>
          </li>
          <li class="page-item" v-bind:class="{active: focusPage == n}" v-for="n in pageCount" @click="$emit('change-page', sort, n)">
            <div class="page-link">{{ n }}</div>
          </li>
          <li class="page-item" @click="$emit('next-page', sort, sortCount)">
            <i class="fas fa-angle-double-right page-link"></i>
          </li>
        </ul>
      </nav>`,
    },
    'no-order-msg': {
      template: `
      <div class="no-data">
        <img src="/Assets/Images/empty.png" alt="無資料">
        <div class="text">查無訂單資料</div>
      </div>
      `,
    },
  }
});
const vueBlockCoupon = new Vue({
  el: '.block#coupon',
  data: {
    couponList: [],
    isLoading: true,
  },
  methods: {
    getCouponDetails() {
      this.isLoading = true;
      axios.get(`/MemberCenter/GetCouponList`).then(res => {
        this.couponList = res.data;
        this.isLoading = false;
      }).catch(error => {
        console.log(error);
      });
    }
  }
});
Vue.component('order-brief', {
  props: ['item', 'showdetail'],
  template: `
  <div class="card order-item">
    <div class="card-header">
      <div class="orderDate kv-box">
        <div class="key">
          <i class="icon fas fa-clock"></i>
        </div>
        <div class="value">
          <div class="date">{{ item.date }}</div>
          <div class="time">{{ item.time }}</div>
        </div>
      </div>
      <div class="orderState">{{ item.OrderStateText }}</div>
    </div>
    <div class="card-body">
      <div class="brief-box">
        <div class="kv-box address">
          <div class="key">
            <i class="icon fas fa-home"></i>
          </div>
          <div class="value">{{ item.Address }}</div>
        </div>
        <div class="total">
          <div class="key">訂單金額:</div>
          <div class="value">{{ item.FinalPrice }}</div>
        </div>
      </div>
      <div class="df">
        <button class="button detail light" @click="$emit('show-detail', item.OrderId)" type="button">查看訂單詳情</button>
        <button class="button comment dark" v-if="item.OrderState == 2" @click="$emit('clear-comment', item.OrderId)" data-toggle="modal" data-target="#modal-comment" type="button">給個評價</button>
        <button class="button comment dark" v-if="item.OrderState == 0" @click="$emit('get-ecpay-form', item.OrderId)" type="button">前往付款</button>
      </div>
    </div>
  </div>`,
});
const vueECPayForm = new Vue({
  el: '#ECPayForm',
  data: {
    CheckMacValue: null,
    ChoosePayment: null,
    ClientBackURL: null,
    EncryptType: null,
    ItemName: null,
    MerchantID: null,
    MerchantTradeDate: null,
    MerchantTradeNo: null,
    OrderResultURL: null,
    PaymentType: null,
    ReturnURL: null,
    TotalAmount: null,
    TradeDesc: null,
  },
  methods: {
    repay(form) {
      this.$el.querySelector('[name="CheckMacValue"]').value = form.CheckMacValue;
      this.$el.querySelector('[name="ChoosePayment"]').value = form.ChoosePayment;
      this.$el.querySelector('[name="EncryptType"]').value = form.EncryptType;
      this.$el.querySelector('[name="ItemName"]').value = form.ItemName;
      this.$el.querySelector('[name="MerchantID"]').value = form.MerchantID;
      this.$el.querySelector('[name="MerchantTradeDate"]').value = form.MerchantTradeDate;
      this.$el.querySelector('[name="MerchantTradeNo"]').value = form.MerchantTradeNo;
      this.$el.querySelector('[name="OrderResultURL"]').value = form.OrderResultURL;
      this.$el.querySelector('[name="PaymentType"]').value = form.PaymentType;
      this.$el.querySelector('[name="ReturnURL"]').value = form.ReturnURL;
      this.$el.querySelector('[name="TotalAmount"]').value = form.TotalAmount;
      this.$el.querySelector('[name="TradeDesc"]').value = form.TradeDesc;
      this.$el.submit();
    },
  },
});
//進入訂單頁載入資料
document.querySelector('#nav-list [href="#order"]').addEventListener('click', () => {
  vueBlockOrder.getOrderBrief('all', 1);
});
//進入優惠券頁載入資料
document.querySelector('#nav-list [href="#coupon"]').addEventListener('click', () => {
  vueBlockCoupon.couponList = [];
  vueBlockCoupon.getCouponDetails();
});
const CancelToken = axios.CancelToken;