var name_edit = document.getElementById("name");
var phone_edit = document.getElementById("phone");
var email_edit = document.getElementById("email");
var address_edit = document.getElementById("address");
var btn = document.getElementById("edit-btnblock");
var edit_btn = document.getElementById("edit");

function display() {
    var fg = document.getElementById("Fg");
    var icon = document.getElementById("direction");
    if (fg.classList.contains("Fg-sm")) {
        fg.classList.remove("Fg-sm");
        icon.innerHTML = "<"
    } else {
        fg.classList.add("Fg-sm");
        icon.innerHTML=">"
    }
    console.log(fg.classList);
}
function remove(event) {
    var id = event.currentTarget.id;
    let num = id.replace("remove","");
    let a = document.getElementById(`prod${num}`);
    $(a).remove();
}


edit_btn.onclick = function edit() {
        var input = document.getElementById("input");
        var label = document.getElementById("label");
        if (input.classList.contains("d-none")) {
            label.classList.add("d-none");
            input.classList.remove("d-none");
        }
        btn.innerHTML = `<input type="submit" value="Save" class="btn btn-default" />`;
    }
function finish() {
    
    btn.innerHTML = `<button type="button" class="btn btn-password" data-toggle="modal" data-target="#exampleModal" id="btn-model-space">
                            修改密碼
                        </button>
                        <button class="btn btn-main" onclick="edit()" id="edit">編輯會員資料</button>`;
    var username = document.getElementById("username");
    username.innerHTML = `<p>Hi!${name} 您好!</p>`;
}
function savepassword() {
    let new_pw = document.getElementById("new_password").value;
    let confirm_pw = document.getElementById("confirm_password").value;
    let body = document.getElementById("password_body");
    var error = document.querySelector(".error");
    var success = document.querySelector(".success");

    if (new_pw != confirm_pw) {
        success.innerHTML = ``;
        error.innerHTML = `確認密碼錯誤!`;
    }
    else {
        error.innerHTML = ``;
        success.innerHTML = `密碼修改成功!`;
    }
}
