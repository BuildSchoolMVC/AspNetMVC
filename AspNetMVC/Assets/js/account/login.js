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

    $(".btn_loginBySocial").on("click", function () {
        $(".website-login").addClass("pre");
        $(".social-login").addClass("pre");
    })

    $(".btn_website-login").on("click", function () {
        $(".website-login").removeClass("pre");
        $(".social-login").removeClass("pre");
    })

    $("#btnGoogleSignIn").on("click", function () {
        GoogleLogin();
        document.querySelectorAll("button").forEach(x => {
            x.setAttribute("disabled", "disabled");
            x.classList.add("disabled");
        })
        this.querySelector(".spinner-border-wrap").classList.remove("opacity");
    })

    document.querySelector("#btnFacebookSignIn").addEventListener("click", function(){
        document.querySelectorAll("button").forEach(x => {
            x.setAttribute("disabled", "disabled");
            x.classList.add("disabled");
        })
        this.querySelector(".spinner-border-wrap").classList.remove("opacity");
        checkLoginState();
    })
})();

function GoogleSigninInit() {
    gapi.load('auth2', function () {
        gapi.auth2.init({
            client_id: GoolgeApp_Cient_Id
        })
    })
}

function GoogleLogin() {
    let auth2 = gapi.auth2.getAuthInstance();
    let url = "/Account/LoginByGoogleLogin"

    auth2.signIn().then(function (GoogleUser) {
        let AuthResponse = GoogleUser.getAuthResponse(true);
        let id_token = AuthResponse.id_token;
        $.ajax({
            url: url,
            method: "post",
            data: { token: id_token },
            success: function (result) {
                if (result.status == true) {
                    toastr.success("登入成功");
                    window.location.replace(`${window.location.origin}/Home/`);
                }
                else {
                    toastr.error(`${result.response}`)
                    document.querySelectorAll("button").forEach(x => {
                        x.removeAttribute("disabled");
                        x.classList.remove("disabled");
                    })
                    document.querySelectorAll(".spinner-border-wrap").forEach(x => {
                        if (!x.classList.contains("opacity")) x.classList.add("opacity");
                    })
                } 
            }
        });

    },
        function (error) {
            toastr.error("Google登入失敗");
            document.querySelectorAll("button").forEach(x => {
                x.removeAttribute("disabled");
                x.classList.remove("disabled");
            })
            document.querySelectorAll(".spinner-border-wrap").forEach(x => {
                if (!x.classList.contains("opacity")) x.classList.add("opacity");
            })

        });
}

function facebookLogin(response) {
    if (response.status === 'connected') {
        getProfile();
    } else {
        FB.login(function () {
            getProfile()
        }, { scope: 'email' });
    }
}


function checkLoginState() {
    FB.getLoginStatus(function (response) {
        facebookLogin(response);
    });
}

function getProfile() {
    FB.api('/me', "GET", { fields: 'name,email,id' }, function (response) {
        fetchData(response)
    })
}
function fetchData(response) {
    let url = "/Account/LoginByFacebookLogin"
    let data = {
        Email: response.email,
        Name: response.name,
        FacebookId: response.id
    }
    fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(result => {
            if (result.status == true) {
                toastr.success("登入成功")
                setTimeout(() => {
                    window.location.replace(`${window.location.origin}/Home/`);
                }, 1500)
            } else {
                toastr.error(`${result.response}`)
                document.querySelectorAll("button").forEach(x => {
                    x.removeAttribute("disabled");
                    x.classList.remove("disabled");
                })
                document.querySelectorAll(".spinner-border-wrap").forEach(x => {
                    if (!x.classList.contains("opacity")) x.classList.add("opacity");
                })
            }
        })
        .catch(err => console.log(err))
}