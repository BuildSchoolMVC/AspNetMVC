(function () {
    const clearWarn = function ($ele) {
        $ele.removeClass("input-warn");
    };

    $(".btn_login").on("click", function (e) {
        e.preventDefault();
        $(".spinner-border").removeClass("opacity");
        $(".btn_login").attr("disabled", "disabled");
        $(".login-block .input").each(function () {
            clearWarn($(this));
            if ($(this).val().length === 0) {
                $(this).addClass("input-warn");
                if ($(this).hasClass("login-account")) {
                    $("p.warn.login-email").text("查無此信箱或有誤");
                } else if ($(this).hasClass("login-password")) {
                    $("p.warn.login-password").text("密碼錯誤");
                }
            }
        });

        const data = {};
        data.AccountName = $(".login-account").val();
        data.Password = $(".login-password").val();
        data.RememberMe = $(".login-remember")[0].checked;

        $.ajax({
            url: "/Account/Login",
            method: "POST",
            data: data,
            success: function (result) {
                if (result.response === "success") {
                    toastr.success("登入成功");
                    localStorage.setItem(isLogin,true)
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