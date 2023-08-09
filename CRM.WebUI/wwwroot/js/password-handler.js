document.addEventListener("DOMContentLoaded", () => {
    const pwShowHide = document.querySelectorAll(".showHidePw"),
        pwFields = document.querySelectorAll(".password");

    pwShowHide.forEach((eyeIcon) => {
        eyeIcon.addEventListener("click", () => {
            pwFields.forEach((pwField) => {
                if (pwField.type === "password") {
                    pwField.type = "text";

                    pwShowHide.forEach((icon) => {
                        icon.classList.replace("fa-eye-slash", "fa-eye");
                    });
                } else {
                    pwField.type = "password";

                    pwShowHide.forEach((icon) => {
                        icon.classList.replace("fa-eye", "fa-eye-slash");
                    });
                }
            });
        });
    });
});
function confirm_password() {
    var pass = document.getElementById("password").value;
    var confirm_pass = document.getElementById("confirmPassword").value;

    if (pass != confirm_pass) {
        document.getElementById('wrong_pass_alert').style.color = 'red';
        document.getElementById('wrong_pass_alert').innerHTML
            = 'Use same password';
        document.getElementById('create').disabled = true;
        document.getElementById('create').style.opacity = (0.4);
    } else {
        document.getElementById('wrong_pass_alert').style.color = 'green';
        document.getElementById('wrong_pass_alert').innerHTML =
            'Password Matched';
        document.getElementById('create').disabled = false;
        document.getElementById('create').style.opacity = (1);
    }
}