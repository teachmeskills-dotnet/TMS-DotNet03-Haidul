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

connection.on("ReceiveMessage", function (evId, userName, message) {
    if (eventId == evId && userName == userName) {
        let div = document.createElement("div");
        div.classList.add("user-message");
        let label = document.createElement("label");
        label.textContent = userName;
        label.classList.add("user__authorName");
        let text = document.createElement("p");
        text.textContent = message;
        text.classList.add("user-message__text");
        let deleteButton = document.createElement("button");
        deleteButton.classList.add("delete-button");
        let editButton = document.createElement("button");
        editButton.classList.add("edit-button");
        deleteButton.innerHTML = `<i class="fas fa-trash-alt"></i>`
        editButton.innerHTML = `<i class="fas fa-edit"></i>`
        document.querySelector(".message-container").appendChild(div);
        div.append(label, text, deleteButton, editButton);
        AddDeleteEvent(deleteButton);
        AddEditEvent(editButton);
    }
});

connection.on("DeleteMessage", function (evId, userName, message) {
    if (eventId == evId && userName == userName) {
        div.remove();
    }
});

connection.on("EditMessage", function (evId, userName, message) {
    if (eventId == evId && userName == userName) {
        div.querySelector(`.user-message__text`).textContent = message;
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

if (userName)
    document.querySelectorAll(`.delete-button`).forEach(btn => AddDeleteEvent(btn));
document.querySelectorAll(".edit-button").forEach(btn => AddEditEvent(btn));

function AddEditEvent(btn) {
    if (userName == btn.parentElement.querySelector(`.user__authorName`).textContent) {
        btn.addEventListener("dblclick", function () {
            div = btn.parentElement;
            let oldMessage = div.querySelector(`.user-message__text`).textContent;
            let textArea = document.createElement("textarea");
            textArea.value = oldMessage;
            textArea.classList.add("edit-message-area");
            let text = div.querySelector(`.user-message__text`)
            div.replaceChild(textArea, text)
            btn.addEventListener("click", function (event) {
                div = btn.parentElement;
                newMessage = textArea.value;
                div.replaceChild(text, textArea);
                connection.invoke("EditMessage", eventId, userName, newMessage, oldMessage).catch(function (err) {
                    return console.error(err.toString());
                });
                event.preventDefault();
            })
        });
    }
}

function AddDeleteEvent(btn) {
    if (userName == btn.parentElement.querySelector(`.user__authorName`).textContent) {
        btn.addEventListener("click", function (event) {
            div = btn.parentElement;
            let message = btn.parentElement.querySelector(`.user-message__text`).textContent;
            connection.invoke("DeleteMessage", eventId, userName, message).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }
}