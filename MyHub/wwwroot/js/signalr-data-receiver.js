"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/zeHub").build();

connection.on("Data", function (data) {
    var dataDiv = document.getElementById("data");
    dataDiv.textContent = data;
});

connection
    .start()
    .then(function () {
    })
    .catch(function (err) {
        return console.error(err.toString());
    });
