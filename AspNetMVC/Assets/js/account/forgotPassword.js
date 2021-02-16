const forgotBtn = document.querySelector(".section_forgetpassword #ForgotBtn");
let cookieIsSent = parseInt(getCookieName("isSent"));
let cookieEmail = getCookieName("email");
let cookieAccountName = getCookieName("accountname");



const getForgotPasswordMail = () => {
    forgotBtn.addEventListener("click", function () {

        let accountName = document.querySelector("#AccountName");
        let email = document.querySelector("#Email");
        let data = {
            Email: email.value,
            AccountName: accountName.value
        }
        let url = "/Account/ForgotPassword";

        if (accountName.value.length == 0 || email.value.length == 0) {
            toastr.error("欄位不能為空!!");
        }
        else if (accountName.value.length != 0 && email.value.length != 0) {
            document.querySelector(".spinner-border-wrap").classList.remove("opacity");
            forgotBtn.setAttribute("disabled", "disabled");

            if (cookieIsSent == 1 && cookieAccountName == data.AccountName && cookieEmail == data.Email) {
                toastr.warning("已寄信至你的信箱，請勿重複按");
                setTimeout(() => {
                    document.querySelector(".spinner-border-wrap").classList.add("opacity");
                    forgotBtn.removeAttribute("disabled");
                }, 1000)
            } else {
                fetch(url, {
                    method: "POST",
                    body: JSON.stringify(data),
                    headers: new Headers({
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    })
                })
                    .then(res => res.json())
                    .then(result => {
                        if (result.response == "error") {
                            toastr.error("帳號與信箱不相符");
                            setTimeout(() => {
                                document.querySelector(".spinner-border-wrap").classList.add("opacity");
                                forgotBtn.removeAttribute("disabled");
                            }, 1000)
                        } else if (result.response == "success") {
                            toastr.success("已寄送密碼重置信至你的信箱，請查收!");

                            document.cookie = `isSent=1;expires=${new Date().setHours(2)}`;
                            document.cookie = `email=${data.Email};expires=${new Date().setHours(2)}`;
                            document.cookie = `accountname=${data.AccountName};expires=${new Date().setHours(2)}`;

                            setTimeout(() => {
                                document.querySelector(".spinner-border-wrap").classList.add("opacity");
                                forgotBtn.removeAttribute("disabled");
                            }, 1000)
                        }
                    })
                    .catch(err => console.log(err))
            }
        }
    })
}


window.addEventListener("load", function () {
    getForgotPasswordMail();
})