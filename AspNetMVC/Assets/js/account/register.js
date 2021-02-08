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
                if (document.querySelector(".register-name").classList.contains("input-warn")) clearWarnInfo(nameInput)

                if (result.response == "exist") showWarnInfo(nameInput, "此帳號已存在")
                else clearWarnInfo(nameInput)
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
}
const emailCheck = () => {
    document.querySelector("#Email").addEventListener("blur", function () {
        $.ajax({
            url: "/Account/RegisterEmailIsExist",
            method: "POST",
            data: {
                email: this.value
            },
            success: function (result) {
                if (document.querySelector(".register-email").classList.contains("input-warn")) clearWarnInfo(emailInput)

                if (result.response == "exist") showWarnInfo(emailInput, "此信箱已被註冊過")
                else clearWarnInfo(emailInput)
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
}
const submitRegister = () => {
    submitBtn.addEventListener("click", function () {
        if (emailInput.value.length == 0) showWarnInfo(emailInput, "不能為空")
        else if (!emailInput.value.includes("@") || !emailInput.value.includes(".")) showWarnInfo(emailInput, "信箱格式不對")
        else clearWarnInfo(emailInput)

        if (Array.from(document.querySelectorAll(".step2 .input-warn")).length > 0) return;
        else {
            document.querySelector(".btn_submit .spinner-border").classList.remove("opacity");
            document.querySelector(".btn_submit").setAttribute("disabled", "disabled");
            $.ajax({
                url: "/Account/Register",
                method: "POST",
                data: {
                    name: nameInput.value,
                    password: passwordInput.value,
                    email: emailInput.value,
                    phone: phoneInput.value,
                    address: addressInput.value,
                    gender: +document.querySelector('.register-gender:checked').value
                },
                success: function (result) {
                    if (result.response == "success") {
                        setTimeout(function () {
                            document.querySelector(".btn_submit .spinner-border").classList.add("opacity");
                            document.querySelector(".btn_submit").removeAttribute("disabled");
                            toastr.success("註冊成功");
                            window.location.replace(`${window.location.origin}/Account/Login`);
                        }, 1000)
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            })
        }
    })
    //前端驗證
    //指定欄位不能為空
    //帳號是否存在
    //密碼是否不同
    //密碼加密
    //密碼是否符合大小寫 + 6個以上 格式
    //號碼是否符合格式 
    //信箱是否符合格式
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
const showWarnInfo = (ele, info) => {
    if (ele) {
        ele.classList.add("input-warn");
    }
    ele.parentNode.querySelector("p").textContent = info
}
const clearWarnInfo = (ele) => {
    ele.classList.remove("input-warn");
    ele.parentNode.querySelector("p").textContent = ""
}

window.addEventListener("load", function () {
    nextStep();
    preStep();
    submitRegister();
    accountNameCheck();
    emailCheck();
})