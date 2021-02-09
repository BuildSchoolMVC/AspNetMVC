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
                if (result.response === "success") {
                    toastr.success("登入成功");
                    window.location.replace(`${window.location.origin}/Home/Index`);
                } else if (result.response === "fail") {
                    toastr.error("登入失敗");

                    setTimeout(function () {
                        $(".spinner-border").addClass("opacity");
                        $(".btn_login").removeAttr("disabled");
                    }, 1000)
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
    });
})();