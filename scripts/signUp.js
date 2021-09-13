// const inputBoxHasValue = (() => {
//     const inputBoxes = document.querySelectorAll("[class*='input-']");
//     inputBoxes.forEach(inputBox => {
//         inputBox.addEventListener("keyup", () => {
//             inputBox.value ? inputBox.classList.add("hasValue") : inputBox.classList.remove("hasValue");
//         });
//     });
// })();

const showErrorStyles = (container, inputBox) => {
    container.classList.add("error");
    inputBox.classList.add("error");
    container.classList.remove("allow");
    inputBox.classList.remove("allow");
}
const removeErrorStyles = (container, inputBox) => {
    container.classList.remove("error");
    inputBox.classList.remove("error");
    container.classList.add("allow");
    inputBox.classList.add("allow");
}

const fullnameValidate = () => {
    const fullnameContainer = document.querySelector(".sign-up-fullname");
    const fullnameInputBox = document.querySelector(".input-fullname");
    const fullnameMessage = document.querySelector(".fullname-message");
    const fullname = fullnameInputBox.value;


    if (fullname === "") {
        showErrorStyles(fullnameContainer, fullnameInputBox);
        fullnameMessage.innerHTML = "Hãy điền Họ và tên";
    }
    else {
        removeErrorStyles(fullnameContainer, fullnameInputBox);
        fullnameMessage.innerHTML = "";
    }
};

const usernameValidate = () => {
    const usernameContainer = document.querySelector(".sign-up-username");
    const usernameInputBox = document.querySelector(".input-username");
    const usernameMessage = document.querySelector(".username-message");
    let username = usernameInputBox.value;


    if (username === "") {
        showErrorStyles(usernameContainer, usernameInputBox);
        usernameMessage.innerHTML = "Hãy điền tên tài khoản";
    }
    else {
        removeErrorStyles(usernameContainer, usernameInputBox);
        usernameMessage.innerHTML = "";
    }
}

const passwordValidate = () => {
    const passwordContainer = document.querySelector(".sign-up-password");
    const passwordInputBox = document.querySelector(".input-password");
    const passwordMessage = document.querySelector(".password-message");
    const password = passwordInputBox.value;

    if (password === "") {
        showErrorStyles(passwordContainer, passwordInputBox);
        passwordMessage.innerHTML = "Hãy điền mật khẩu";
    }
    else if (password.length <= 5) {
        showErrorStyles(passwordContainer, passwordInputBox);
        passwordMessage.innerHTML = "Mật khẩu phải lớn hơn 5 ký tự";
    }
    else {
        removeErrorStyles(passwordContainer, passwordInputBox);
        passwordMessage.innerHTML = "";
    }
}

const repasswordValidate = () => {
    const repasswordContainer = document.querySelector(".sign-up-repassword");
    const repasswordInputBox = document.querySelector(".input-repassword");
    const repasswordMessage = document.querySelector(".repassword-message");
    const repassword = repasswordInputBox.value;
    const password = document.querySelector(".input-password").value;

    if (repassword === "") {
        showErrorStyles(repasswordContainer, repasswordInputBox);
        repasswordMessage.innerHTML = "Hãy xác nhận mật khẩu";
    }
    else if (repassword !== password) {
        showErrorStyles(repasswordContainer, repasswordInputBox);
        repasswordMessage.innerHTML = "Mật khẩu xác nhận không chính xác";
    }
    else {
        removeErrorStyles(repasswordContainer, repasswordInputBox);
        repasswordMessage.innerHTML = "";
    }
}

const showPassword = showPasswordButton => {
    const passwordInputBox = document.querySelector(".input-password");
    const repasswordInputBox = document.querySelector(".input-repassword");

    if (passwordInputBox.getAttribute("type") == "password") {
        passwordInputBox.setAttribute("type", "text");
        repasswordInputBox.setAttribute("type", "text");
        showPasswordButton.innerHTML = "Ẩn password";
    }
    else {
        passwordInputBox.setAttribute("type", "password");
        repasswordInputBox.setAttribute("type", "password");
        showPasswordButton.innerHTML = "Hiển thị password";
    }
}

const addEvent = (() => {
    const inputBoxes = document.querySelectorAll("[class*='input-']");
    const showPasswordButton = document.getElementById("show-psw-btn");
    const submitButton = document.querySelector(".submit-button");

    inputBoxes[0].addEventListener("blur", () => {
        fullnameValidate();
    });
    inputBoxes[1].addEventListener("blur", () => {
        usernameValidate();
    });
    inputBoxes[2].addEventListener("blur", () => {
        passwordValidate();
    });
    inputBoxes[3].addEventListener("blur", () => {
        repasswordValidate();
    });

    showPasswordButton.addEventListener("click", () => {
        showPassword(showPasswordButton);
    });

    submitButton.addEventListener("click", e => {
        fullnameValidate();
        usernameValidate();
        passwordValidate();
        repasswordValidate();
        const errorMessage = document.querySelector("input.error");
        if (errorMessage) {
            e.preventDefault();
        }
    });
})();