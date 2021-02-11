(function () {
    const clearWarn = function ($ele) {
        $ele.removeClass("input-warn");
    };

    $(".login-block .input").each(function (index, item) {
        $(item).change(function () {
            if ($(item).val().length > 0) {
                if ($(item).hasClass("input-warn")) {
                    clearWarn($(item));
                    $(item).siblings().text("");
                }
            } else {
                $(item).addClass("input-warn");
                if ($(item).hasClass("input-warn")) {
                    $(item).siblings().text("不能為空");
                }
            }
        })
    });
    $(".btn_login").on("click", function (e) {
        e.preventDefault();
        setTimeout(function () {
            $(".spinner-border").addClass("opacity");
            $(".btn_login").removeAttr("disabled");
        }, 200)
        $(".login-block .input").each(function (index, item) {
            if ($(item).val().length === 0) {
                $(item).addClass("input-warn");
                $("p.warn").text("不能為空");
            }
        })
        if ($(".input-warn").length > 0) {
            return;
        } else {
            const data = {};
            data.AccountName = $(".login-account").val();
            data.Password = $(".login-password").val();
            data.RememberMe = $(".login-remember")[0].checked;
            data.validationMessage = grecaptcha.getResponse();;

            $.ajax({
                url: "/Account/Login",
                method: "POST",
                data: data,
                success: function (result) {
                    if (result.response === "success") {
                        toastr.success("登入成功");
                        localStorage.setItem(isLogin, true)
                        window.location.replace(`${window.location.origin}/Home/Index`);
                    } else if (result.response === "fail") {
                        toastr.error("登入失敗");

                        setTimeout(function () {
                            $(".spinner-border").addClass("opacity");
                            $(".btn_login").removeAttr("disabled");
                        }, 1000)
                    } else if (result.response == "valdationFail") {
                        toastr.warning("請勾選驗證");

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
        }
    });
})();