import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';

const OnlineUserCount = () => {
    const [userCount, setUserCount] = useState(0);
    const [userName, setUserName] = useState('');

    useEffect(() => {
        //const connection = new signalR.HubConnectionBuilder()
        //    .withUrl("https://localhost:7138/userCountHub", {
        //        accessTokenFactory: () => localStorage.getItem("token") // Replace with how you store the token
        //    })
        //    .withAutomaticReconnect()
        //    .build();
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7138/userCountHub") // Change this to your API URL
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(() => console.log("Connected to SignalR"))
            .catch(err => console.error("SignalR Connection Error: ", err));

        connection.on("UpdateUserCount", count => {
            setUserCount(count);
        });
        connection.on("Welcome", data => {
            setUserName(data);
        });

        return () => {
            connection.stop();
        };
    }, []);

    return (
        <div>
            <h2>Welcome: {userName}</h2>
            <h2>Online Users: {userCount}</h2>
        </div>
    );
};

export default OnlineUserCount;
