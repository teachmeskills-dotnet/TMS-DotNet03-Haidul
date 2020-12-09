
const eventId = document.getElementById("sendButton").getAttribute("eventId");
const userName = document.getElementById("sendButton").getAttribute("userName");
const connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chat`)
    .build();


//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (id, userName, message) {
    if (eventId == id) {
        var div = document.createElement("div");
        div.classList.add("user-message");
        var label = document.createElement("label");
        label.classList.add("user-message__authorName");
        var text = document.createElement("p");
        text.classList.add("user-message__text");
     
        label.textContent = userName;
        text.textContent = message;
        document.querySelector(".message-container").appendChild(div);
        div.appendChild(label);
        div.appendChild(text);

    }
});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageArea").value;
    var authorName = document.getElementById("sendButton").getAttribute("userName");
    if (userName == authorName) {
        connection.invoke("SendMessage", eventId, userName, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
});