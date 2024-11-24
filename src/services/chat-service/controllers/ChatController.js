const Chat = require('../models/Chat');
const User = require('../models/User');

const ChatController = {
    async createChat(req, res) {
        try {
            const { name, members } = req.body;
            const chat = new Chat({ name, members });
            await chat.save();
            res.status(201).json(chat);
        } catch (error) {
            res.status(500).json({ error: 'Chat Creation error' });
        }
    },

    async joinChat(req, res) {
        try {
            const { userId, chatId } = req.body;
            const chat = await Chat.findById(chatId);
            if (!chat.members.includes(userId)) {
                chat.members.push(userId);
                await chat.save();
            }
            res.json({ message: 'User is enter the chat' });
        } catch (error) {
            res.status(500).json({ error: 'Error while entering the chat' });
        }
    },

    async leaveChat(req, res) {
        try {
            const { userId, chatId } = req.body;
            const chat = await Chat.findById(chatId);
            chat.members = chat.members.filter(id => id.toString() !== userId);
            await chat.save();
            res.json({ message: 'User leave the chat' });
        } catch (error) {
            res.status(500).json({ error: 'Error chat leaving' });
        }
    },

    async getUserChats(req, res) {
        try {
            const { userId } = req.query;
            const chats = await Chat.find({ members: userId });
            res.json(chats);
        } catch (error) {
            res.status(500).json({ error: 'Error chats fetching' });
        }
    }
};

module.exports = ChatController;
