"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

connection.on("LoadAllItems", function () {
    location.href = '/Question/Index';
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
