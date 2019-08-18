let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder().withUrl("/coffeehub").build();

    connection.on("ReceiveOrderUpdate", (update) => {
        $('#status').text(update);
    });

    connection.on("NewOrder", (order) => {
        console.log(order.product);
        console.log(order.size);
        $('#status').text(`Someone ordered a ${order.product}`);
    });
};

$('#submit').on('click', (e) => {
    e.preventDefault();
    let product = $('#product').val();
    let size = $('#size').val();

    setupConnection();
    connection.start()
        .then(() => {
        fetch("/Coffee",
            {
                method: "POST",
                body: JSON.stringify({ product, size }),
                headers: {
                    "content-type": "application/json"
                }
            })
            .then(response => response.text())
            .then(orderId => {
                connection.invoke("GetUpdateForOrder", orderId);
            });
    })
        .catch(console.log);
});

