let recipientId = null;
let connection = null;

let recipient = $('.active-user')[0];
if (recipient) {
    recipientId = recipient.dataset.userid;
    if (recipientId) {
        connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

        connection.on("ReceiveMessage", receiveMessage);
        connection.on("SendedMessage", appendMessage);

        connection.start().catch(catchError);

        $("#send-btn").on("click", sendMessage);
    }
}



function receiveMessage(message, messageRecipienetId) {
    //if (recipientId == userId) {

    //}
    console.log(messageRecipienetId);
    appendMessage(message);
}

function appendMessage(message) {
    let newMessage = $(`<p>${message}</p>`);
    $("#message-list").append(newMessage);
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


