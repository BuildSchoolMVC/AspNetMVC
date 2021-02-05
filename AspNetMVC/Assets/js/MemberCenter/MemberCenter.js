
function display() {
    var fg = document.getElementById("Fg");
    var icon = document.getElementById("direction");
    console.log(fg.classList);
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
    var name_edit = document.getElementById("name");
    var phone_edit = document.getElementById("phone");
    var email_edit = document.getElementById("email");
    var address_edit = document.getElementById("address");
    var btn = document.getElementById("edit-btnblock");
function edit() {
    var name = $('#name').text().replace(/\s*/g, "");
    var phone = $('#phone').text().replace(/\s*/g, "");
    var email = $('#email').text().replace(/\s*/g, "");
    var address = $('#address').text().replace(/\s*/g, "");
    btn.innerHTML = `<button class="btn btn-main" onclick="finish()" id="finish">完成</button>`;
    console.log(name);
    console.log(phone);
    name_edit.innerHTML = `<input type="text" value="${name}" id="name_edit">`;
    phone_edit.innerHTML = `<input type = "tel" value="${phone}" id="phone_edit">`;
    email_edit.innerHTML = `<input type="email" value="${email}" id="email_edit">`;
    address_edit.innerHTML = `<input type="text" value="${address}" id="address_edit">`
}
function finish() {
    var name = document.getElementById("name_edit").value;
    var phone = document.getElementById("phone_edit").value;
    var email = document.getElementById("email_edit").value;
    var address = document.getElementById("address_edit").value;
    name_edit.innerHTML = `${name}`;
    phone_edit.innerHTML = `${phone}`;
    email_edit.innerHTML = `${email}`;
    address_edit.innerHTML = `${address}`;
    btn.innerHTML = `<button type="button" class="btn btn-password" data-toggle="modal" data-target="#exampleModal" id="btn-model-space">
                            修改密碼
                        </button>
                        <button class="btn btn-main" onclick="edit()" id="edit">編輯會員資料</button>`;
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