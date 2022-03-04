"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/MyHub").build();
connection.on("send", (Email) => {
    alert(Email);
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});


