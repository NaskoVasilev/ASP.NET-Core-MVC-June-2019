loadMessages();

window.onload = () => { 
    console.log('here   ')
    if(isLoggedIn())
    {
        hideLoginAndRegisterAndShowLoggedInData();
        user = getUser();
        $('#username-logged-in').text(user);
    }
}

function loadMessages() {
    $.get(apiUrl + '/messages/all')
        .done(data => {
            renderMessages(data)
        })
        .fail(catchError)
}

function createMessage() {
    if (!isLoggedIn()) {
        alert('You have to login before send message.')
        return;
    }

    let content = $('#message').val();
    if (!content) {
        alert('You can not send message without content.')
        return;
    }

    if (connection) {
        connection.invoke('SendMessage', content).catch(catchError)
    }
}

function renderMessages(data) {
    let messagesHolder = $('#messages');
    messagesHolder.empty();
    for (let message of data) {
        appendMessage(messagesHolder, message);
    }
}

function catchError(error) {
    console.log(error);
}

function receiveMessage(user, content) {
    let messagesHolder = $('#messages');
    appendMessage(messagesHolder, { user, content });
}

function appendMessage(messagesHolder, message) {
    let newMessage = $(`<div class="message d-flex justify-content-start"><strong>${message.user}</strong>: ${message.content}</div>`)
    messagesHolder.append(newMessage);
}

function getToken() {
    return localStorage.getItem('auth_token');
}