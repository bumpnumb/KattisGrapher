var host = window.location.origin.replace("http", "ws");
var socket = new WebSocket("ws://127.0.0.1:80");

socket.onopen = function (openEvent) {
    console.log("Socket connection is open.");
    sendTextMessage("ping");
};

socket.onmessage = function (e) {
    str = "";
    for (var i = 0; i < e.data.length; i += 8) {
        val = e.data[i + 4] + e.data[i + 6] + e.data[i] + e.data[i + 2] + "";
        str += String.fromCharCode(parseInt(val, 16));
    }
    var options = new ChartOptions(str);

    DrawChart(options);
};

socket.onerror = function (err) {
    console.error(err);
};

function sendTextMessage(message) {
    if (socket.readyState !== WebSocket.OPEN) {
        console.log("Socket is not open for connection.");
        return;
    }
    socket.send(message);
    console.log("Sent: " + message);
}

window.onbeforeunload = function () {
    socket.onclose = function () { }; // disable onclose handler first
    socket.send("Exit<00>");
    socket.close();
};