var button = document.getElementById("sendButton");

button.addEventListener("click", function () {
	var message = document.getElementById("messageToSend").value;
	console.log(message);
	$.get("/Home/SendMessage?message=" + message);
});