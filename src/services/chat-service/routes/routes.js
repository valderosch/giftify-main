const express = require('express');
const UserController = require('../controllers/UserController');
const ChatController = require('../controllers/ChatController');
const MessageController = require("../controllers/MessageController");
const router = express.Router();

router.get('/', (req, res) => {
    res.send('API is working!');
});

//user routes
router.post('/status/online', UserController.setStatusOnline);
router.post('/status/offline', UserController.setStatusOffline);
router.post('/block', UserController.blockUser);
router.post('/unblock', UserController.unblockUser);


// chat routes
router.post('/chat/create', ChatController.createChat);
router.post('/chat/join', ChatController.joinChat);
router.post('/chat/leave', ChatController.leaveChat);
router.get('/chat/list', ChatController.getUserChats);

//message routes
router.post('/chat/message/send', MessageController.sendMessage);
router.get('/message/history/:chatId', MessageController.getMessageHistory);

module.exports = router;
