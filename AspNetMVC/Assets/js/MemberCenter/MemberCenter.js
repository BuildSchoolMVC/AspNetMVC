var name_edit = document.getElementById("name");
var phone_edit = document.getElementById("phone");
var email_edit = document.getElementById("email");
var address_edit = document.getElementById("address");
var btn = document.getElementById("edit-btnblock");
var edit_btn = document.getElementById("edit");
var menu = document.getElementById("menu-control");
var savePassword = document.getElementById("savePassword");
var ps_edit = document.getElementById("ps_edit");
var error = document.querySelector(".error");
var success = document.querySelector(".success");


window.onload = NotFoundId();

menu.onclick = function display() {
    var fg = document.getElementById("Fg");
    var icon = document.getElementById("direction");
    if (fg.classList.contains("Fg-sm")) {
        fg.classList.remove("Fg-sm");
        icon.innerHTML = "<"
    } else {
        fg.classList.add("Fg-sm");
        icon.innerHTML=">"
    }
    
}
function remove(event) {
    var id = event.currentTarget.id;
    let num = id.replace("remove","");
    let a = document.getElementById(`prod${num}`);
    $(a).remove();
}


edit_btn.onclick = function edit() {
    var _Input = [];
    var _Label = [];
    for (i = 1; i <= 4; i++) {
        var input = _Input.push(document.getElementById(`input${i}`));
        var label = _Label.push(document.getElementById(`label${i}`));
    }
    _Label.forEach(function (item)
    {
        item.classList.add("d-none");
    });
    _Input.forEach(function (item)
    {
        if (item.classList.contains("d-none"))
        {
            item.classList.remove("d-none");

        }
    });
        btn.innerHTML = `<input type="submit" value="完成" class="btn btn-main" />`;
    }

savePassword.onclick = function savepassword() {
    let new_pw = document.getElementById("new_password").value;
    let confirm_pw = document.getElementById("confirm_password").value;
    var password = document.getElementById("origin_password").value;
    let body = document.getElementById("password_body");
    
    
    let url = "/MemberCenter/Password";
    
    let data = {
        Password: password,
        NewPassword: new_pw,
        ConfirmPassword: confirm_pw
    };

    if (new_pw != confirm_pw) {
        success.innerHTML = ``;
        error.innerHTML = `確認密碼錯誤!`;
    }
    else if (password == new_pw) {
        success.innerHTML = ``;
        error.innerHTML = `新密碼與原始密碼相同!`;
    }
    else
    {
        fetch(url, {
            method: "POST",
            body: JSON.stringify(data),
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(res => res.json())
        .then(result => {
            if (result.response == "success") {
                error.innerHTML = ``;
                success.innerHTML = `密碼修改成功!`;
                toastr.success("密碼修改成功!!");
                $('#exampleModal').modal('hide');
            }
            else {
                success.innerHTML = ``;
                error.innerHTML = `輸入密碼錯誤!`;
            }
        })
    }
}
function NotFoundId() {
    if (!document.cookie.includes("user")) {
            alert("目前還沒登入喔!")
            window.location.assign("/Account/Login")
    }        
}
ps_edit.onclick = function () {
    
    $("#new_password").val("");
    $("#confirm_password").val("");
    $("#origin_password").val("");
    error.innerHTML = ``;
    success.innerHTML = ``;
    console.log("123");
}

