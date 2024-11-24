
//app init
const express = require('express');
const connectDB = require('./dbcontext/dbContext');
const mongoose = require("mongoose");
const config = require("config");
const app = express();

//web socket
const http = require('http');
const { Server } = require('socket.io');
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: '*' } });


const PORT = process.env.PORT || config.get('app-port');
const routes = require('./routes/routes');

app.use(express.json());
app.use('/api', routes);

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