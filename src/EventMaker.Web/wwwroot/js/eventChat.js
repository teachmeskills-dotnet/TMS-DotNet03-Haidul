﻿
const eventId = document.getElementById("sendButton").getAttribute("eventId");
const connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chat/${eventId}`)
    .build();


//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (id, user, message) {
    if (eventId == id) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    }
    else {
        console.log(`hehe`);
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var userName = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var eventId = this.getAttribute("eventId");
    console.log(userName, message, eventId);
    connection.invoke("SendMessage", eventId, userName, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});