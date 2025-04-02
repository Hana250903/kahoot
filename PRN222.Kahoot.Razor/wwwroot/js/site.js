"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalRServer")
    .build();

connection.on("LoadAllItems", function () {
    location.href = '/Medicine/Index';
});

// Khởi động connection
connection.start()
    .catch(err => console.error(err.toString()));
