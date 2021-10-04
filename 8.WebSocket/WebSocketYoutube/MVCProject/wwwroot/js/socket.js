var uri = "wss://localhost:5001/chat";
var socket = new WebSocket(uri);

var list = document.getElementById("messages");
var button = document.getElementById("sendButton");

function connect() {
	socket.onopen = function (e) {
		console.log("Connection Established");
	};

	socket.onerror = function (e) {
		console.log("Connection Error");
	};

	socket.onclose = function (e) {
		console.log("Connection Closed");
	};

	socket.onmessage = function (e) {
		appendItem(list, e.data);
		console.log(e.data);
	}
}

connect();

button.addEventListener("click", function () {
	var sendMessage = function (element) {
		console.log("Sending message -------------------");
		socket.send(element);
	};

	var message = document.getElementById("messageToSend").value;
	sendMessage(message);
});

function appendItem(list, message) {
	var item = document.createElement("li");
	item.appendChild(document.createTextNode(message));
	list.appendChild(item);
}