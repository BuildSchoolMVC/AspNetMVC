const renderUserInfo = () => {
    let userInfo = JSON.parse(localStorage.getItem("social"));
    if (userInfo) {
        document.querySelector(".form-header .social-platform").textContent = userInfo.SocialPlatform;
        document.querySelector(".form-header .pic").src = userInfo.ImgUrl;
        document.querySelector(".form-body .social-email").value = userInfo.Email;
        document.querySelector(".form-header h3 img").src = `/Assets/images/${userInfo.SocialPlatform.toLowerCase()}.png`;
        localStorage.removeItem("social");
    }
}

const registerForm = () => {
    let account = document.querySelector("#AccountName");
    let email = document.querySelector("#Email");
    let checkbox = document.querySelector("#Checkbox");
    let password = document.querySelector("#Password");

    document.querySelector(".submit").addEventListener("click", function () {
        if (account.value.length == 0) {
            toastr.error("帳號欄位不得為空");
            return;
        } else if (!judgeCharacter(account.value, "english") || account.value.length < 6 && account.value.length >= 1) {
            toastr.error("格式不對");
            return;
        } else if (checkbox.checked) {
            if (password.value.length == 0) {
                toastr.error("密碼欄位不得為空");
                return;
            }   
        }else {
            data = {
                AccountName: account.value,
                Email: email.value,
                IsIntegrated: checkbox.checked,
                Password: password.value,
                SocialPlatform: document.querySelector(".social-platform").textContent
            }

            fetch("/Account/SocialRegister", {
                method: "POST",
                body: JSON.stringify(data),
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            })
            .then(res => res.json())
            .then(result => {
                if (result.status) {
                    toastr.success(`${result.response}`)
                } else {
                    toastr.error(`${result.response}`)
                }

            })
        }
    })
}

window.addEventListener("load", function () {
    renderUserInfo();
    registerForm();
})