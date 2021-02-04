(function () {
    const $titleLogin = $(".change-box .title-login");
    const $titleRegister = $(".change-box .title-register");
    const $loginBlock = $(".login-block");
    const $registerBlock = $(".register-block");
    const $inputLogin = $(".login-block .input");
    const $inputRegister = $(".register-block .input");
    const $warnText = $(".register-block .warn");
    const fadeDuration = 100;
    const clearWarn = function ($ele) {
        $ele.removeClass("input-warn");
    };
    const clearInput = function ($ele) {
        $ele.val("");
    }
    const passwordCheck = function (str) {
        let arr = [];
        if (str.match(/^.*[a-z]+.*$/) == null) arr.push(false);
        else arr.push(true);

        if (str.match(/^.*[0-9]+.*$/) == null) arr.push(false);
        else arr.push(true);

        return arr.every(x => x == true);
    }

    $(".change-box .title-login").on("click", function () {
        $titleLogin.addClass("selected");
        $titleRegister.removeClass("selected");
        $registerBlock.fadeOut(fadeDuration, () => {
            $loginBlock.fadeIn(fadeDuration);
            clearWarn($inputRegister);
            clearInput($inputRegister);
            $warnText.text("");
        });
    });
    $(".change-box .title-register").on("click", function () {
        $titleLogin.removeClass("selected");
        $titleRegister.addClass("selected");
        $loginBlock.fadeOut(fadeDuration, () => {
            $registerBlock.fadeIn(fadeDuration);
            clearWarn($inputLogin);
            clearInput($inputLogin);
        });
    });
    $(".btn_login").on("click", function (e) {
        e.preventDefault();
        $(".spinner-border").removeClass("opacity");
        console.log($(".btn_login"))
        $(".btn_login").attr("disabled", "disabled");
        $(".login-block .input").each(function () {
            clearWarn($(this));
            if ($(this).val().length === 0) {
                $(this).addClass("input-warn");
                if ($(this).hasClass("login-email")) {
                    $("p.warn.login-email").text("查無此信箱或有誤");
                } else if ($(this).hasClass("login-password")) {
                    $("p.warn.login-password").text("密碼錯誤");
                }
            }
        });

        const data = {};
        data.Email = $(".login-email").val();
        data.Password = $(".login-password").val();
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "/Account/Login",
            method: "POST",
            data: {
                model: data,
                __RequestVerificationToken: token,
                returnUrl: "Home/Index"
            },
            success: function (result) {
                if (result.Url == undefined) {
                    return;
                }
                if (result.Url === "Home/Index") {
                    toastr.success("登入成功");
                    setTimeout(function () {
                        window.location.replace(`${window.location.origin}/${result.Url}`);
                    }, 1000)
                } else if (result.Url === "Error") {
                    toastr.error("登入失敗");
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
    });
    $(".btn_submit").on("click", function (e) {
        e.preventDefault();
        const $email = $(".register-block .register-email");
        const $password = $(".register-block .register-password");
        const $passwordCheck = $(".register-block .register-password-check");
        let isCorrect = true;
        $(".warn").each(function () {
            $(this).text("");
        })
        $(".register-block .input").each(function () {
            clearWarn($(this));
            document.querySelectorAll("p").forEach(x => { x.textContent = "" });
            console.log(document.querySelectorAll("p"))
            if ($(this).val().length === 0) {
                isCorrect = false;
                $(this).addClass("input-warn");
                if ($(this).hasClass("register-email")) {
                    $("p.warn.register-email").text("請填寫信箱");
                } else if ($(this).hasClass("register-password")) {
                    $("p.warn.register-password").text("請填寫密碼");
                } else if ($(this).hasClass("register-password-check")) {
                    $("p.warn.register-password").text("請填寫密碼");
                }
            }
        });

        if ($password.val() !== $passwordCheck.val()) {
            isCorrect = false;
            $password.addClass("input-warn");
            $passwordCheck.addClass("input-warn");
            $("p.warn.register-password-check").text("密碼不相符");
        }
        if (!$email.val().includes("@") || !$email.val().includes(".")) {
            isCorrect = false;
            clearWarn($email);
            $($email).addClass("input-warn");
            $("p.warn.register-email").text("信箱格式錯誤，請輸入有效信箱");
        }

        if (($password.val().length < 6 || $password.val().length > 18)) {
            isCorrect = false;
            $password.addClass("input-warn");
            $("p.warn.register-password").text("請輸入6~18英數字，字母與數字至少各1個");
        }
        if (!passwordCheck($password.val())) {
            isCorrect = false;
            $password.addClass("input-warn");
            $("p.warn.register-password").text("請輸入6~18英數字，字母與數字至少各1個");
        }

        if (!isCorrect) {
            return;
        }
    });
})();