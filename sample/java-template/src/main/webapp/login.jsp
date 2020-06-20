<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>

<html>
<head>
    <script src="js/jquery-2.0.3.js"></script>

    <script language="javascript">
        $(document).ready(function () {
            sessionStorage.setItem("currentUser", null);
            $("#login-button").click(function () {
                if ($('#user').val().length && $('#password').val().length) {
                    $.post(
                        "/login",
                        {
                            action: "login",
                            user: $('#user').val(),
                            password: $('#password').val()
                        },
                        function (data, success) {
                            data = JSON.parse(data);
                            console.log(data);

                            if (data["success"] === true) {
                                let temp = {userId: data["userId"]};
                                sessionStorage.setItem("currentUser", JSON.stringify(temp));
                                console.log(JSON.parse(sessionStorage["currentUser"]));
                                location.href = './home.jsp';
                            } else {
                                $("#error").html('<h4 style="color: red;">Bad credentials!</h4>');

                            }
                        }
                    );
                } else {
                    alert("Both fields should be non-empty!")
                }
            });
        });
    </script>
</head>
<body>
<h3>Login</h3>
<form>
    <label for="user">Username:</label>
    <input type="text" id="user" name="user"><br><br>
    <label for="password">Password:</label>
    <input type="password" id="password" name="password"><br><br>
    <button type="button" id="login-button">Login</button>
</form>
<section id="error">
</section>
</body>
</html>
