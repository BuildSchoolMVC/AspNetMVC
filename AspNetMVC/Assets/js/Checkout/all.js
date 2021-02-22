// (function() {
const obj_now = new Date();
const disableList = ['2021-01-29', '2021-01-30', '2021-02-15'];
const enableBtn = (btn) => {
  btn.removeAttr('disabled');
  btn.removeClass('disable');
};
const disableBtn = (btn) => {
  btn.attr('disabled', '');
  btn.addClass('disable');
};
const $countyList = $('#county-list');
const $districtList = $('#district-list');
$.ajax({
  type: 'GET',
  url: '/Checkout/GetDistricts',
  dataType: 'json',
  success: (data) => {
    data.forEach((item) => {
      let option = document.createElement('option');
      option.text = item.Name;
      $countyList[0].add(option);
    });
    $countyList.on('change', function () {
      let districtList = $districtList[0];
      let prompt = districtList.firstElementChild;
      $districtList.empty();
      $districtList.append(prompt);
      data[this.selectedIndex - 1].Districts.forEach((item) => {
        let option = document.createElement('option');
        option.text = item;
        districtList.add(option);
      });
    });
  }
});
// model區
const dateService = {
  obj: new Date(),
  date: null,
  time: null,
  totalHours: null,
  set setDate(t) {
    this.date = true;
    this.allSet();
  },
  set setTime(t) {
    this.time = true;
    this.allSet();
  },
  allSet: function () {
    if (this.date && this.time) {
      let ele_date = document.createElement('div');
      let ele_time = document.createElement('div');
      ele_date.classList.add('date');
      ele_date.classList.add('time');
      let pickY = this.obj.getFullYear();
      let pickM = (parseInt(this.obj.getMonth()) + 1).toString().padStart(2, '0');
      let pickD = this.obj.getDate().toString().padStart(2, '0');
      let pickHour = this.obj.getHours().toString().padStart(2, '0');
      let pickMin = this.obj.getMinutes().toString().padStart(2, '0');
      ele_date.textContent = `${pickY}-${pickM}-${pickD}`;
      ele_time.textContent = `${pickHour}:${pickMin}`;
      const $serviceDate = $('#service-date');
      const $value = $('#service-date .value');
      $value.empty();
      $value.append(ele_date, ele_time);
      $serviceDate.addClass('set');
    }
  },
};
const addressService = {
  county: () => $('#county-list').val(),
  district: () => $('#district-list').val(),
  address: () => $('#input_address').val(),
};
const printAddress = function () {
  for (const key in addressService) {
    if (addressService[key]() == null) return;
  }
  const strCounty = addressService.county();
  const strDistrict = addressService.district();
  const strAddress = addressService.address();
  $('#address .value').text(`${strCounty} ${strDistrict} ${strAddress}`);
  $('#address').addClass('set');
};
$('#input_address').on('change', printAddress);
$countyList.on('change', printAddress);
$districtList.on('change', printAddress);
// model區
const hasInput = (ele) => ele.value.length != 0;
const hasSelect = (ele) => ele.selectedIndex != 0;
const steps = [{

}, {
  inputs: [{
    ele: document.querySelector('#fill-info #input_name'),
    check: hasInput,
    warn: '請輸入您的姓名',
  }, {
    ele: document.querySelector('#fill-info #input_phone'),
    check: (ele) => (/^09\d{2}\-?\d{3}\-?\d{3}$/).test(ele.value),
    warn: '請輸入正確的手機號碼，例如: 0912345678',
  }, {
    ele: document.querySelector('#fill-info #input_email'),
    check: (ele) => (/^[\w\.\-]+\@[\w\.\-]+$/).test(ele.value),
    warn: '請輸入正確的email',
  }, {
    ele: document.querySelector('#fill-info #county-list'),
    check: hasSelect,
    warn: '必填欄位',
  }, {
    ele: document.querySelector('#fill-info #district-list'),
    check: hasSelect,
    warn: '必填欄位',
  }, {
    ele: document.querySelector('#fill-info #input_address'),
    check: hasInput,
    warn: '請輸入正確的地址',
  }],
}, {
  creditInputs: [{
    ele: document.querySelector('#pay #input_credit'),
    check: (ele) => ele.value.length == 19,
    warn: '請輸入有效的信用卡號碼',
  }, {
    ele: document.querySelector('#pay #input_security'),
    check: (ele) => ele.value.length == 3,
    warn: '請輸入信用卡背面的安全碼',
  }, {
    ele: document.querySelector('#pay #input_expireM'),
    check: hasSelect,
    warn: '必填欄位',
  }, {
    ele: document.querySelector('#pay #input_expireY'),
    check: hasSelect,
    warn: '必填欄位',
  }],
  atmInputs: [],
  agrees: [{
    ele: document.querySelector('#pay #read'),
    check: (ele) => ele.checked,
    warn: '請閱讀並確認勾選以上內容',
  }],
}, {

}];
// common event
const onlyNum = $('.only-num');
onlyNum.on('input', function () {
  this.value = this.value.replace(/\D/g, '');
});
// pick time
const $row_date = $('#row_date');
const $row_time = $('#row_time');
const makeMonth = (obj_startDate, count) => {
  $row_date.empty();
  const startY = obj_startDate.getFullYear();
  const startM = obj_startDate.getMonth();
  const startD = obj_startDate.getDate();
  $('#pick-time .year').text(`${startY}`);
  $('#pick-time .month').text(`${startM + 1}`);
  // 開始空格
  for (let i = 0; i < obj_startDate.getDay(); i++) {
    $row_date.append(`<div class="date"></div>`);
  }
  for (let i = 0; i < count; i++) {
    const d = new Date(
      obj_now.getFullYear(), startM, startD + i
    );
    const $ele_date = $(`
      <div class="date">
        <div class="text">${d.getMonth() + 1}/${d.getDate()}</div>
      </div>
    `);
    $ele_date[0].obj_date = d;
    $ele_date.on('click', function () {
      if ($row_date.focusDate) {
        $row_date.focusDate.removeClass('selected');
      }
      $(this).addClass('selected');
      $row_date.focusDate = $(this);
      dateService.obj.setFullYear(
        this.obj_date.getFullYear(),
        this.obj_date.getMonth(),
        this.obj_date.getDate()
      );
      dateService.setDate = true;
    });
    // 恢復高亮上次選取
    if ($row_date.focusDate) {
      if ($row_date.focusDate[0].obj_date.getTime() == d.getTime()) {
        $ele_date.addClass('selected');
        $row_date.focusDate = $ele_date;
      }
    }
    $row_date.append($ele_date);
  }
};
const $btn_lastM = $('#last-month');
const $btn_nextM = $('#next-month');
const thisY = obj_now.getFullYear();
const thisM = obj_now.getMonth();
const thisD = obj_now.getDate();
const obj_tomorrow = new Date(
  thisY, thisM, thisD + 1
);
const obj_nextMonthEnd = new Date(
  thisY, thisM + 2, 0
);
$btn_lastM.on('click', function () {
  disableBtn($(this));
  enableBtn($btn_nextM);
  makeMonth(obj_tomorrow, generateCount);
});
$btn_nextM.on('click', function () {
  disableBtn($(this));
  enableBtn($btn_lastM);
  const tomorrowY = obj_tomorrow.getFullYear();
  const tomorrowNextM = obj_tomorrow.getMonth() + 1;
  const obj_tomorrowNextM = new Date(
    tomorrowY, tomorrowNextM, 1
  );
  const obj_nextMonthEnd = (new Date(
    tomorrowY, tomorrowNextM + 1, 0
  )).getDate();
  makeMonth(obj_tomorrowNextM, obj_nextMonthEnd);
});
let generateCount = ((obj_nextMonthEnd - obj_tomorrow) / 86400000) + 1;
generateCount = Math.min(generateCount, 31);
// 產生time方塊
const createTime = (workTime) => {
  let time = document.createElement('div');
  let text = document.createElement('div');
  time.classList.add('time');
  text.classList.add('text');
  const hh = workTime.getHours().toString().padStart(2, '0');
  const mm = workTime.getMinutes().toString().padStart(2, '0');
  text.textContent = `${hh}:${mm}`;
  time.appendChild(text);
  time.workTime = new Date(workTime);
  return time;
};
let workTime = new Date();
workTime.setHours(8);
workTime.setMinutes(0);
for (let i = 0; i < 4; i++) {
  ele = createTime(workTime);
  $('#row_time').append(ele);
  workTime.setMinutes(workTime.getMinutes() + 30);
}
workTime.setHours(13);
workTime.setMinutes(0);
for (let i = 0; i < 12; i++) {
  ele = createTime(workTime);
  $('#row_time').append(ele);
  workTime.setMinutes(workTime.getMinutes() + 30);
}
$row_time.children().on('click', function () {
  if ($row_time.focusTime) {
    $row_time.focusTime.removeClass('selected');
  }
  $(this).addClass('selected');
  $row_time.focusTime = $(this);
  dateService.obj.setHours(this.workTime.getHours());
  dateService.obj.setMinutes(this.workTime.getMinutes());
  dateService.setTime = true;
});
// 上下一步
const $lastStep = $("#last-step");
const $nextStep = $("#next-step");
const $stepList = $('.step');
const $barFront = $('.barGroup .barFront');
const $numList = $('.numGroup .num');
const $radioCredit = $('#credit');
const $checkRead = $('#read');
let state = 0; /* 0 ~ 3 */
const isComplete = function () {
  let ok = true;
  switch (state) {
    case 0:
      if ($row_date.focusDate && $row_time.focusTime) {
        return true;
      } else {
        toastr.remove();
        toastr.error('請選擇日期');
        return false;
      }
      break;
    case 1:
      steps[1]['inputs'].forEach((item) => {
        if (!item.check(item.ele)) {
          ok = false;
          $(item.ele).addClass('input-warn');
          $(item.ele).next('.warn').text(item.warn);
        } else {
          $(item.ele).removeClass('input-warn');
          $(item.ele).next('.warn').text('');
        }
      });
      return ok;
    case 2:
      const payMethod = $('#pay input[name=pay-method]:checked').attr('data-inputs');
      steps[2][payMethod].forEach((item) => {
        if (!item.check(item.ele)) {
          ok = false;
          $(item.ele).addClass('input-warn');
          $(item.ele).next('.warn').text(item.warn);
        } else {
          $(item.ele).removeClass('input-warn');
          $(item.ele).next('.warn').text('');
        }
      });
      steps[2]['agrees'].forEach((item) => {
        if (!item.check(item.ele)) {
          ok = false;
          $(item.ele).addClass('input-warn');
          $(item.ele).siblings('.warn').text(item.warn);
        } else {
          $(item.ele).removeClass('input-warn');
          $(item.ele).siblings('.warn').text('');
        }
      });
      return ok;
    default:
      break;
  }
  return false;
};
$lastStep.on('click', function () {
  state--;
  if (state < 0) {
    state = 0;
    return;
  }
  switch (state) {
    case 0:
      disableBtn($lastStep);
      break;
  }
  $barFront.css('width', `${33.333 * state}%`);
  $numList[state + 1].classList.remove('on');
  $stepList[state + 1].classList.remove('on');
  $stepList[state].classList.add('on');
});
$nextStep.on('click', function () {
  if (!isComplete()) {
    return;
  }
  state++;
  if (state > 3) {
    state = 3;
    return;
  }
  switch (state) {
    case 1:
      enableBtn($lastStep);
      break;
    case 3:
      disableBtn($lastStep);
      disableBtn($nextStep);
      $('.coupon button').replaceWith($('<span>優惠券</span>'));
      $.ajax({
        type: 'POST',
        url: '/Checkout/AddOrder',
        data: {
          
        },
        // success: (message) => {
        //   $('#done .pic').html(`
        //     <svg id="tick" viewBox="0 0 32 32">
        //       <path d="M27,9 l-15,15 -7,-7"></path>
        //     </svg>
        //   `);
        //   $('#done .title').text(message.title);
        //   $('#done .content').text(message.content);
        // },
      });
      break;
  }
  $barFront.css('width', `${33.333 * state}%`);
  $numList[state].classList.add('on');
  $stepList[state - 1].classList.remove('on');
  $stepList[state].classList.add('on');
});
// fill-info

// pay
const $creditMethod = $(document.querySelector('.credit-method'));
$('#credit, #atm').on('change', (e) => {
  if (e.target.getAttribute('data-i') == 0) {
    $creditMethod.slideDown(200);
  } else {
    $creditMethod.slideUp(200);
  }
});
const $inputCredit = $('#input_credit');
const $inputSecurity = $('#input_security');
$inputCredit.lastStr = '';
$inputCredit.realStr = '';
$inputSecurity.lastStr = '';
$inputCredit.on('input', (e) => {
  const num = $inputCredit.val();
  $inputCredit.realStr = num.replaceAll(' ', '');
  // format
  let output = '';
  for (let i = 0; i < $inputCredit.realStr.length; i++) {
    if (i % 4 == 0 && i != 0) {
      output += ' ';
    }
    output += $inputCredit.realStr[i];
  }
  $inputCredit.lastStr = output;
  $inputCredit.val(output);
});
//coupon
const $couponBox = $('#modal_coupon .modal-body');
const setOneSelect = ($box) => {
  const $allCoupon = $box.children();
  $allCoupon.on('click', function () {
    $box[0].selectedGuid = this.obj.CouponDetailId;
    $('.coupon .money').text(this.obj.DiscountAmount);
    $allCoupon.removeClass('selected');
    $(this).addClass('selected');
  });
};
$('.coupon button').on('click', function () {
  $couponBox.empty();
  $couponBox.append($(`
    <div class="loading">
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
    </div>
  `));
  $.ajax({
    type: 'GET',
    url: '/Checkout/GetCouponList',
    dataType: 'json',
    success: (data) => {
      $couponBox.empty();
      data.forEach((obj) => {
        let $item = $(`
          <div class="coupon-item">
            <div class="money">${obj.DiscountAmount}</div>
            <div class="text">
              <div class="name">${obj.CouponName}</div>
              <div class="date-end">有效日期: ${obj.DateEnd}</div>
            </div>
          </div>
        `);
        $item[0].obj = obj;
        $couponBox.append($item);
        if (obj.CouponDetailId == $couponBox[0].selectedGuid) {
          $item.addClass('selected');
        }
      });
      setOneSelect($couponBox);
    }
  });
});
const totalBox = $('.block_items .total .money')[0];
totalBox.total = totalBox.textContent;
$('#modal_coupon .ok').on('click', () => {
  $('#modal_coupon').modal('hide');
  totalBox.textContent = totalBox.total - $('.block_items .coupon .money').text();
});
$('#modal_coupon .clear').on('click', () => {
  $couponBox[0].selectedGuid = null;
  $('.coupon .money').text(0);
  $couponBox.children().removeClass('selected');
});
// invoice
const invoiceData = {
  // 傳回後端用
  invoiceI: 0,
  foundationI: 0
};
const $invoiceCheck = $('#invoice-check');
const $foundationCheck = $('#foundation-check');
const $caption = $('.form_invoice .caption');
const $component = $('#component > *');
const $donateSelect = $('.form_invoice #donate-select');
$('#invoice-select .option').on('click', (e) => {
  $invoiceCheck.click();
  invoiceData.invoiceI = e.target.getAttribute('data-index');
  // update內容
  let content;
  switch (invoiceData.invoiceI) {
    case '0':
      content = `<p>由uCleaner自動為您兌獎，中獎後將主動通知您並掛號寄出紙本發票</p>
        <p>提醒您，個人電子發票一旦開立，不得任意更改或改開三聯式發票(統編)</p>`;
      $component.removeClass('show');
      break;
    case '1':
      content = `
          <p>提醒您，捐贈發票後無法變更成開立或索取紙本發票</p>
        `;
      $donateSelect.addClass('show');
      break;
  }
  $caption.html(content);
});
$('#donate-select .option').on('click', (e) => {
  $foundationCheck.click();
  invoiceData.foundationI = e.target.getAttribute('data-index');
});
// 關閉各自的下拉選單
$('.my-dropdown .head-list').on('blur', (e) => {
  const $checkbox = $(e.target).children('[type=checkbox]')[0];
  if ($checkbox.checked) {
    $checkbox.click();
  }
});
// toastr
toastr.options = {
  "closeButton": false,
  "debug": false,
  "newestOnTop": false,
  "progressBar": false,
  "positionClass": "toast-top-center",
  "preventDuplicates": false,
  "onclick": null,
  "showDuration": "200",
  "hideDuration": "1000",
  "timeOut": "2500",
  "extendedTimeOut": "1000",
  "showEasing": "swing",
  "hideEasing": "linear",
  "showMethod": "fadeIn",
  "hideMethod": "fadeOut"
};
// 初始化
let year = obj_now.getFullYear().toString().substring(2);
const endYear = parseInt(year) + 25;
for (; year < endYear; year++) {
  $('#input_expireY').append(`<option>${year}</option>`);
}
$("[data-toggle=tooltip").tooltip();
// main
makeMonth(obj_tomorrow, generateCount);
// })();