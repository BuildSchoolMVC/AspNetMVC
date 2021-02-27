(function () {
    const clearWarn = function ($ele) {
        $ele.removeClass("input-warn");
    };

    $(".login-block .input").each(function (index, item) {
        if ($(item).val().length == 0){
            $(item).parent().find(".label-group").removeClass("active");
        }
        $(item).change(function () {
            if ($(item).val().length > 0) {
                if ($(item).hasClass("input-warn")) {
                    clearWarn($(item));
                }
                $(item).parent().find(".warn").text("");
                $(item).parent().find(".label").removeClass("label-warn");
                $(item).parent().find(".label-group").addClass("active");
            } else {
                $(item).addClass("input-warn");
                if ($(item).hasClass("input-warn")) {
                    $(item).parent().find(".warn").text("不能為空");
                    $(item).parent().find(".label-group").removeClass("active");
                    $(item).parent().find(".label").addClass("label-warn");
                }
            }
        })
    });

    $(".btn_login").on("click", function (e) {
        e.preventDefault();
        $(".login-block .input").each(function (index,item) {
            if ($(item).val().length == 0) {
                $(item).addClass("input-warn");
                if ($(item).hasClass("input-warn")) {
                    $(item).parent().find(".warn").text("不能為空");
                    $(item).parent().find(".label-group").removeClass("active");
                    $(item).parent().find(".label").addClass("label-warn");
                }
            }
        })
        if ($(".input-warn").length > 0) {
            return;
        } else {
            setTimeout(function () {
                $(".btn_login .spinner-border-wrap").removeClass("opacity");
                $(".btn_login").attr("disabled", "disabled");
            }, 200)
            $(".login-block .input").each(function (index, item) {
                if ($(item).val().length === 0) {
                    $(item).addClass("input-warn");
                    $(item).parent().find(".label").addClass("label-warn");
                    $("p.warn").text("不能為空");
                }
            })

            const data = {};
            data.AccountName = $(".login-account").val();
            data.Password = $(".login-password").val();
            data.RememberMe = $(".login-remember")[0].checked;
            data.validationMessage = grecaptcha.getResponse();

            $.ajax({
                url: "/Account/Login",
                method: "POST",
                data: data,
                success: function (result) {
                    if (result.status == 1) {
                        toastr.success("登入成功");
                        window.location.href = "/Home/Index";
                    } else if (result.status == 0 && result.response == "無此人") {
                        toastr.error("登入失敗");

                        setTimeout(function () {
                            $(".spinner-border-wrap").addClass("opacity");
                            $(".btn_login").removeAttr("disabled");
                            window.location.href  = `${window.location.origin}/Account/Login`;
                        }, 1000)
                    } else if (result.status == 0 && result.response == "信箱尚未驗證成功") {
                        toastr.info("此帳號還未通過信箱驗證，請檢查信箱!!!");

                        setTimeout(function () {
                            $(".spinner-border-wrap").addClass("opacity");
                            $(".btn_login").removeAttr("disabled");
                            window.location.href = `${window.location.origin}/Account/Login`;
                        }, 3000)
                    }
                    else if (result.status == 0 && result.response == "驗證失敗") {
                        toastr.warning("請勾選驗證");

                        setTimeout(function () {
                            $(".spinner-border-wrap").addClass("opacity");
                            $(".btn_login").removeAttr("disabled");
                        }, 1000)
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })


            setTimeout(function () {
                $(".spinner-border-wrap").removeClass("opacity");
                $(".btn_login").attr("disabled","disabled");
            }, 8000)
        }
    });
})();

document.querySelector("#btnGoogleSignIn").addEventListener("click", function () {
    googleLogin(this);
    document.querySelectorAll("button").forEach(x => {
        x.setAttribute("disabled", "disabled");
        x.classList.add("disabled");
    })
    this.querySelector(".spinner-border-wrap").classList.remove("opacity");
});

document.querySelector("#btnFacebookSignIn").addEventListener("click", function () {
    document.querySelectorAll("button").forEach(x => {
        x.setAttribute("disabled", "disabled");
        x.classList.add("disabled");
    })
    this.querySelector(".spinner-border-wrap").classList.remove("opacity");
    checkLoginState(this);
})