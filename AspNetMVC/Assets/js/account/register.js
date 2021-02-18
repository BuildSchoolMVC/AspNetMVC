const nextBtn = document.querySelector(".section_registerForm .btn_next");
const preBtn = document.querySelector(".section_registerForm .btn_pre");
const submitBtn = document.querySelector(".section_registerForm .btn_submit");
const nameInput = document.querySelector("#Name");
const passwordInput = document.querySelector("#Password");
const confirmPasswordInput = document.querySelector("#ConfirmPassword");
const emailInput = document.querySelector("#Email");
const phoneInput = document.querySelector("#Phone");
const addressInput = document.querySelector("#Address");

const nextStep = () => {
    nextBtn.addEventListener("click", function () {
        if (passwordInput.value.length == 0) showWarnInfo(passwordInput, "不能為空")
        else if (!judgeCharacter(passwordInput.value, "lowercase") || !judgeCharacter(passwordInput.value, "capital") || judgeCharacter(passwordInput.value, "other")) showWarnInfo(passwordInput, "格式不對")
        else clearWarnInfo(passwordInput)

        if (confirmPasswordInput.value.length == 0) showWarnInfo(confirmPasswordInput, "不能為空")
        else clearWarnInfo(confirmPasswordInput)

        if (passwordInput.value != confirmPasswordInput.value) {
            showWarnInfo(passwordInput, "密碼不相同")
            showWarnInfo(confirmPasswordInput, "")
        }

        if (nameInput.value.length == 0) showWarnInfo(nameInput, "不能為空")
        else if (!judgeCharacter(nameInput.value, "english")) showWarnInfo(nameInput, "格式不對")
        else if (nameInput.value.length < 6 && nameInput.value.length >= 1) showWarnInfo(nameInput, "帳號格式不對")
        else clearWarnInfo(nameInput)

        if (emailInput.value.length == 0) showWarnInfo(emailInput, "不能為空")
        else if (!emailInput.value.includes("@") || !emailInput.value.includes(".")) showWarnInfo(emailInput, "信箱格式不對")
        else clearWarnInfo(emailInput)

        if (Array.from(document.querySelectorAll(".step1 .input-warn")).length > 0) return;
        else document.querySelectorAll("div[class*='step']").forEach(x => x.classList.add("next"));

    })
}
const preStep = () => {
    preBtn.addEventListener("click", function () {
        document.querySelectorAll("div[class*='step']").forEach(x => x.classList.remove("next"));
    })
}
const accountNameCheck = () => {
    document.querySelector("#Name").addEventListener("blur", function () {
        $.ajax({
            url: "/Account/RegisterIsExist",
            method: "POST",
            data: {
                name: this.value
            },
            success: function (result) {
                if (document.querySelector(".input.register-name").value.length > 0) {
                    if (document.querySelector(".register-name").classList.contains("input-warn")) clearWarnInfo(nameInput)

                    if (result.response == "exist") showWarnInfo(nameInput, "此帳號已存在")
                    else clearWarnInfo(nameInput)
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
}
const emailCheck = () => {
    document.querySelector("#Email").addEventListener("blur", function () {
        let data = {
            email: this.value
        }
        fetch("/Account/RegisterEmailIsExist",{
            method: "POST",
            body: JSON.stringify(data),
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
                })
        })
        .then(res => res.json())
        .then(res => {
            if (document.querySelector(".input.register-email").value.length > 0) {
                if (document.querySelector(".register-email").classList.contains("input-warn")) clearWarnInfo(emailInput)

                if (res.response == "Exist") showWarnInfo(emailInput, "此信箱已被註冊過")
                else clearWarnInfo(emailInput)
            }
        })
        .catch((err) => console.log(err))
    })
}
const submitRegister = () => {
    submitBtn.addEventListener("click", function () {
        if (Array.from(document.querySelectorAll(".step2 .input-warn")).length > 0) return;
        else {
            document.querySelector(".btn_submit .spinner-border-wrap").classList.remove("opacity");
            document.querySelector(".btn_submit").setAttribute("disabled", "disabled");
            document.querySelector(".btn_pre").setAttribute("disabled", "disabled");

            $.ajax({
                url: "/Account/Register",
                method: "POST",
                data: {
                    name: nameInput.value,
                    password: passwordInput.value,
                    email: emailInput.value,
                    phone: phoneInput.value,
                    address: addressInput.value,
                    gender: +document.querySelector(".register-gender:checked").value,
                    validationMessage: grecaptcha.getResponse() //取得驗證token
                },
                success: function (result) {
                    if (result.response == "success") {
                        setTimeout(function () {
                            toastr.success("註冊成功，請前往信箱進行驗證");
                            setTimeout(() => {
                                window.location.replace(`${window.location.origin}/Account/Login`);
                            }, 1500)
                        }, 1000)
                    } else if (result.response == "valdationFail") {
                        toastr.warning("請勾選以便進行驗證");
                        setTimeout(function () {
                            document.querySelector(".btn_submit .spinner-border-wrap").classList.add("opacity");
                            document.querySelector(".btn_submit").removeAttribute("disabled");
                            document.querySelector(".btn_pre").removeAttribute("disabled");
                        },500)
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            })
        }
    })
}
const showWarnInfo = (ele, info) => {
    if (ele) {
        ele.classList.add("input-warn");
    }
    if (ele.parentNode.querySelector(".fa-exclamation-circle")) {
        ele.parentNode.querySelector(".fa-exclamation-circle").classList.add("i-warn");
    }
    ele.parentNode.querySelector(".label").classList.add("label-warn");
    ele.parentNode.querySelector("p").textContent = info;
}
const clearWarnInfo = (ele) => {
    ele.classList.remove("input-warn");
    ele.parentNode.querySelector(".label").classList.remove("label-warn");

    if (ele.parentNode.querySelector(".fa-exclamation-circle")) {
        ele.parentNode.querySelector(".fa-exclamation-circle").classList.remove("i-warn");
    }
    ele.parentNode.querySelector("p").textContent = ""
}

function GoogleSigninInit() {
    gapi.load('auth2', function () {
        gapi.auth2.init({
            client_id: GoolgeApp_Cient_Id
        })
    })
}
function GoogleLogin() {
    let auth2 = gapi.auth2.getAuthInstance();
    auth2.signIn()
        .then(function (GoogleUser) {
        let user_id = GoogleUser.getId();
        let AuthResponse = GoogleUser.getAuthResponse(true);
        let id_token = AuthResponse.id_token;
        let data = { id_token };

        fetch(id_token_to_userIDUrl, {
            method: "post",
            body: JSON.stringify(data),
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
        .then(res => res.json())
        .then(res => { console.log(res) })
        .catch(err => { console.log(err)})
    });

}

window.addEventListener("load", function () {
    document.querySelectorAll(".input").forEach(x => {
        if (x.value.length == 0) x.parentNode.querySelector(".label-group").classList.remove("active");
        x.addEventListener("change", function () {
            if (x.value.length == 0) {
                showWarnInfo(x, "不能為空");
                x.parentNode.querySelector(".label-group").classList.remove("active");
            } else {
                clearWarnInfo(x);
                x.parentNode.querySelector(".label-group").classList.add("active");
            }
        })
    })
    nextStep();
    preStep();
    accountNameCheck();
    emailCheck();
    submitRegister();

    document.querySelector("#btnGoogleSignIn").addEventListener("click", GoogleLogin);
})