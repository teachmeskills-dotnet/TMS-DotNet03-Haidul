
const eventId = document.getElementById("sendButton").getAttribute("eventId");
const userName = document.getElementById("sendButton").getAttribute("userName");
const connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chat`)
    .build();
let div;


//Disable buttons until connection is established
document.getElementById("sendButton").disabled = true;
document.querySelectorAll(".delete-button").forEach(comment => {
    comment.disabled = true;
})
document.querySelectorAll(".delete-button").forEach(comment => {
    comment.disabled = true;
})

connection.on("ReceiveMessage", function (id, userName, message) {
    if (eventId == id && userName == userName) {
        let div = document.createElement("div");
        div.classList.add("user-message");
        let label = document.createElement("label");
        label.textContent = userName;
        label.classList.add("user__authorName");
        let text = document.createElement("p");
        text.textContent = message;
        text.classList.add("user-message__text");
        document.querySelector(".message-container").appendChild(div);
        div.appendChild(label);
        div.appendChild(text);

    }
});

connection.on("DeleteMessage", function (id, userName, message) {
    if (eventId == id && userName == userName) {
        div.remove();
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.querySelectorAll(".delete-button").forEach(btn => {
        btn.disabled = false;
    })
    document.querySelectorAll(".delete-button").forEach(btn => {
        btn.disabled = false;
    })
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    let message = document.getElementById("messageArea").value;
    connection.invoke("SendMessage", eventId, userName, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.querySelectorAll(`.delete-button`).forEach(btn => {
    btn.addEventListener("click", function (event) {
        div = btn.parentElement;
        let message = btn.parentElement.querySelector(`.user-message__text`).textContent;
        connection.invoke("DeleteMessage", eventId, userName, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
});
