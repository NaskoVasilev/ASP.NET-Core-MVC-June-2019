function login() {
    let username = $('#username-login').val();
    let password = $('#password-login').val();

    $('#username-login').val('');
    $('#password-login').val('');

    let requestBody = {
        username: username,
        password: password
    };

    $.post({
        url: apiUrl + '/account/login',
        data: JSON.stringify(requestBody),
        headers:{
            "Content-Type": "application/json"
        },
        success: function (data) {
            $('#guest-navbar').hide();
            $('#caption').text('Welcome to Chat-Inc!');
            hideLoginAndRegisterAndShowLoggedInData();
            saveToken(data);
            user = getUser();
            $('#username-logged-in').text(user);
            setUpSignalRConnection();
            loadMessages();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function register() {
    let username = $('#username-register').val();
    let password = $('#password-register').val();

    $('#username-register').val('');
    $('#password-password').val('');

    let requestBody = {
        username: username,
        password: password
    };

    $.post({
        url: apiUrl + '/account/register',
        data: JSON.stringify(requestBody),
        headers:{
            "Content-Type": "application/json"
        },
        success: function (data) {
            toggleLogin();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function toggleLogin() {
    $('#login-data').show();
    $('#register-data').hide();
}

function toggleRegister() {
    $('#login-data').hide();
    $('#register-data').show();
}

function hideLoginAndRegisterAndShowLoggedInData() {
    $('#login-data').hide();
    $('#register-data').hide();

    $('#logged-in-data').show();
}

function showLoginAndHideLoggedInData() {
    toggleLogin();

    $('#logged-in-data').hide();
}

function logout() {
    $('#caption').text('Login to begin chatting!');
    evictToken();
    showLoginAndHideLoggedInData();
}

function saveToken(token) {
    localStorage.setItem('auth_token', token);
}

function evictToken() {
    localStorage.removeItem('auth_token');
}

function getUser() {
    let token = localStorage.getItem('auth_token');

    let claims = token.split('.')[1];
    let decodedClaims = atob(claims);
    let parsedClaims = JSON.parse(decodedClaims);
    return parsedClaims['unique_name'];
}

function isLoggedIn() {
    return localStorage.getItem('auth_token') != null;
}

$('#logged-in-data').hide();
toggleLogin();