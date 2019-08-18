let recipientId = null;
let connection = null;

let recipient = $('.active-user')[0];
if (recipient) {
    recipientId = recipient.dataset.userid;
    if (recipientId) {
        connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

        connection.on("ReceiveMessage", receiveMessage);
        connection.on("SendedMessage", appendSendedMessage);

        connection.start().catch(catchError);

        $("#send-btn").on("click", sendMessage);
    }
}

scrollMessageListToEnd();

function scrollMessageListToEnd(){
    $('#message-list').scrollTop($('#message-list')[0].scrollHeight);
}

function receiveMessage(message, messageRecipienetId) {
    if (recipientId === messageRecipienetId) {
        appendReceivedMessage(message);
    }
    else {
        let span = $(`#${messageRecipienetId} > span`)[0];
        let value = Number.parseInt(span.textContent);
        span.textContent = value + 1;
    }
}

function appendMessage(messageHolder) {
    $("#message-list").append(messageHolder);
    scrollMessageListToEnd();
}

function appendSendedMessage(message) {
    let messageHolder = $(`<div class="ml-4"><p class="btn btn-primary d-inline-block">${message}</p></div>`);
    appendMessage(messageHolder);
}

function appendReceivedMessage(message) {
    let messageHolder = $(`<div class="d-flex justify-content-end mr-4 "> <p class="btn btn-dark text-right d-inline-block">${message}</p></div>`);
    appendMessage(messageHolder);
}

function catchError(error) {
    console.log(error);
}

function sendMessage(event) {
    let messageInput = $("#content");
    let message = messageInput.val();
    if (!message) {
        alert("Message cannot be empty string.");
        return;
    }

    connection.invoke("SendMessage", message, recipientId).catch(catchError);

    messageInput.val("");
}


