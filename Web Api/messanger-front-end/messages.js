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
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + getToken()  
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

function getToken(){
    return localStorage.getItem('auth_token');
}