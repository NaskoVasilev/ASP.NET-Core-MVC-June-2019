const apiUrl = 'https://localhost:44373/api';
let user = null;

loadMessages();
$('#reset-data').hide();
$('#choose').on('click', chooseUsername);
$('#reset').on('click', resetUsername);
$('#message-input button').on('click', createMessage);

function chooseUsername() {
    let username = $('#username').val();

    if (!username) {
        alert('Username is required.');
        return;
    }

    user = username;
    $('#username-choice').text(user);
    $('#reset-data').show();
    $('#choose-data').hide();
}

function resetUsername() {
    user = null;
    $('#reset-data').hide();
    $('#choose-data').show();
    $('#username').val('');
}

function loadMessages() {
    $.get(apiUrl + '/messages/all')
        .done(data => {
            renderMessages(data)
        })
        .fail(error => {
            console.log(error);
        })
}

function createMessage() {
    if (!user) {
        alert('You have to choose username before send message.')
        return;
    }

    let content = $('#message').val();
    if (!content) {
        alert('You can not send message without content.')
        return;
    }

    $.post({
        url: apiUrl + '/messages/create',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ content: content, user: user })
    })
        .done(() => {
            loadMessages();
        })
        .fail(error => {
            console.log(error);
        });
}

function renderMessages(data) {
    let messagesHolder = $('#messages');
    messagesHolder.empty();
    for (let message of data) {
        let newMessage = $(`<div class="message d-flex justify-content-start"><strong>${message.user}</strong>: ${message.content}</div>`)
        messagesHolder.append(newMessage);
    }
}