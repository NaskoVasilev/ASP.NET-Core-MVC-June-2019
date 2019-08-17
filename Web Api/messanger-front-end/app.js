const baseUrl = 'https://localhost:44373';
const apiUrl = `${baseUrl}/api`;

let user = null;

let connection = null;
function setUpSignalRConnection() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl(`${baseUrl}/chat`, { accessTokenFactory: getToken }).build();

    connection.on('ReceiveMessage', receiveMessage);

    connection.start().catch(catchError);
}