const resetBtn = document.querySelector(".section_resetpassword #ResetBtn");
const password = document.querySelector("#Password");
const confirmedPassword = document.querySelector("#ConfirmPassword");
const id = document.querySelector("#UserId");


const resetPassword = () => {
    resetBtn.addEventListener("click", function () {
        if (password.value != confirmedPassword.value) {
            toastr.error("密碼不相同");
            return;
        } else if (password.value.length == 0 || id.value.length == 0) {
            toastr.error("不能為空!!!");
            return;
        } else if (!judgeCharacter(password.value, "lowercase") || !judgeCharacter(password.value, "capital") || judgeCharacter(password.value, "other") || password.value.length < 6) {
            toastr.error("格式不對，英文大小寫各1個、至少6位不包含特殊符號之字元!!!");
            return;
        }
        else {
            let data = {
                NewPassword: password.value,
                AccountId: id.value
            }
            let url = "/Account/ResetPassword"

            document.querySelector(".spinner-border-wrap").classList.remove("opacity");

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
                        toastr.error("發生錯誤!!");
                        setTimeout(() => {
                            document.querySelector(".spinner-border-wrap").classList.add("opacity");
                        },1000)
                    } else if (result.response == "success") {
                        toastr.success("密碼修改成功!!!")
                        setTimeout(() => {
                            document.querySelector(".spinner-border-wrap").classList.add("opacity");
                        }, 1000)
                    }
                })
                .catch(err => console.log(err))
        }
    })
}

window.addEventListener("load", function () {
    resetPassword();
})