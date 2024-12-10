//app init
const express = require('express');
const connectDB = require('./dbcontext/dbContext');
const mongoose = require("mongoose");
const config = require("config");
const app = express();
const axios = require("axios");
const axiosRetry = require("axios-retry");

//web socket
const http = require('http');
const { Server } = require('socket.io');
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: '*' } });

// axiosRetry(axios, {
//     retries: 3,
//     retryDelay: (retryCount) => {
//         console.log(`Спроба запиту #${retryCount}`);
//         return retryCount * 1000;
//     },
//     retryCondition: (error) => {
//         return error.response?.status >= 500;
//     }
// });


const PORT = process.env.PORT || config.get('app-port');
const routes = require('./routes/routes');

app.use(express.json());
app.use('/chats/', routes);

io.on('connection', (socket) => {
    console.log('A user connected');

    socket.on('disconnect', () => {
        console.log('A user disconnected');
    });

    socket.on('joinChat', (chatId) => {
        socket.join(chatId);
    });

    socket.on('leaveChat', (chatId) => {
        socket.leave(chatId);
    });

    socket.on('sendMessage', (message) => {
        io.to(message.chatId).emit('newMessage', message);
    });
});

const start = async () => {
    try{
        await connectDB();

        server.listen(PORT, () => {
            console.log(`Server started and listening port: ${PORT}`)
        })
    } catch (e){
        console.log(e);
    }
}

start()