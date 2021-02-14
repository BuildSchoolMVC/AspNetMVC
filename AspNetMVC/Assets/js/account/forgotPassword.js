﻿const forgotBtn = document.querySelector(".section_forgetpassword #ForgotBtn");

const getForgotPasswordMail = () => {
    forgotBtn.addEventListener("click", function () {

        let accountName = document.querySelector("#AccountName");
        let email = document.querySelector("#Email");
        let data = {
            Email: email.value,
            AccountName: accountName.value
        }
        let url = "/Account/ForgotPassword"

        if (accountName.value.length != 0 && email.value.length != 0) {
            document.querySelector(".spinner-border").classList.remove("opacity");

            fetch(url,{
                method: "POST",
                body: JSON.stringify(data),
                headers: new Headers({
                    'Content-Type': 'application/json'
                })
                })
               .then(result => {
                if (result.response == "error") {
                    toastr.error("帳號與信箱不相符");
                } else if (result.response == "success") {
                    toastr.success("已寄送密碼重置信至你的信箱，請查收!")
                }
               })
                .catch(err => console.log(err))
        }
    })
}

window.addEventListener("load", function () {
    getForgotPasswordMail();
})