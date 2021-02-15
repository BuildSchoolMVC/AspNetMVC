const resetBtn = document.querySelector(".section_resetpassword #ResetBtn");
const password = document.querySelector("#Password");
const confirmedPassword = document.querySelector("#ConfirmPassword");
const id = document.querySelector("#UserId");

const resetPassword = () => {
    resetBtn.addEventListener("click", function () {
        if (password.value != confirmedPassword.value) {
            toastr.error("密碼不相同");
            return;
        } else if (password.value.length == 0 || id.value.length != 0) {
            toastr.error("不能為空!!!");
            return;
        } else if (!judgeCharacter(password.value, "lowercase") || !judgeCharacter(password.value, "capital") || judgeCharacter(password.value, "other") || password.value.length < 6) {
            toastr.error("格式不對，英文大小寫各1個、至少6位不包含特殊符號之字元!!!");
            return;
        }
        else {
            let data = {
                Password: password.value,
                AccountName: id.value
            }
            let url = "/Account/ResetPassord"

            document.querySelector(".spinner-border-wrap").classList.remove("opacity");

            fetch(url, {
                method: "POST",
                body: JSON.stringify(data),
                headers: new Headers({
                    'Content-Type': 'application/json'
                })
            })
                .then(result => {
                    if (result.response == "error") {
                        toastr.error("發生錯誤!!");
                    } else if (result.response == "success") {
                        toastr.success("密碼修改成功!!!")
                    }
                })
                .catch(err => console.log(err))
        }
    })
}

const judgeCharacter = (str, judge) => {
    let result;
    switch (judge) {
        case "capital":
            result = str.match(/^.*[A-Z]+.*$/);
            break;
        case "lowercase":
            result = str.match(/^.*[a-z]+.*$/);
            break;
        case "english":
            result = str.match(/^.*[a-zA-Z]+.*$/);
            break;
        case "number":
            result = str.match(/^.*[0-9]+.*$/);
            break;
        case "other":
            result = str.match(/^.*[^0-9A-Za-z]+.*$/);
            break;
    }
    return result == null ? false : true;
}

window.addEventListener("load", function () {
    resetPassword();
})