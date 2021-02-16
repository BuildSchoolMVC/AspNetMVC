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
        setTimeout(function () {
            $(".spinner-border-wrap").removeClass("opacity");
            $(".btn_login").attr("disabled","disabled");
        }, 200)
        $(".login-block .input").each(function (index, item) {
            if ($(item).val().length === 0) {
                $(item).addClass("input-warn");
                $(item).parent().find(".label").addClass("label-warn");
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
                        window.location.replace(`${window.location.origin}/Home/Index`);
                    } else if (result.response === "fail") {
                        toastr.error("登入失敗");

                        setTimeout(function () {
                            $(".spinner-border-wrap").addClass("opacity");
                            $(".btn_login").removeAttr("disabled");
                            window.location.replace(`${window.location.origin}/Account/Login`);
                        }, 1000)
                    } else if (result.response === "emailActivationFail") {
                        toastr.info("此帳號還未通過信箱驗證，請檢查信箱!!!");

                        setTimeout(function () {
                            $(".spinner-border-wrap").addClass("opacity");
                            $(".btn_login").removeAttr("disabled");
                            window.location.replace(`${window.location.origin}/Account/Login`);
                        }, 3000)
                    }
                    else if (result.response == "valdationFail") {
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
        }
    });
})();