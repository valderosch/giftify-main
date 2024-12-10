const Chat = require('../models/Chat');
const mongoose = require('mongoose');
const axios = require('axios');

const ChatController = {
    async createChat(req, res) {
        try {
            const { name, members } = req.body;

            const userIds = await Promise.all(members.map(async (email) => {
                try {
                    const response = await axios.get(`http://localhost:5000/identity/user/GetUserProfile/profile?email=${email}`);
                    if (!response.data || !response.data.id) {
                        throw new Error(`User with email ${email} not found`);
                    }

                    return response.data.id;
                } catch (error) {
                    console.error(`Error fetching user data for ${email}: ${error.message}`);
                    return null;
                }
            }));

            if (userIds.some(id => id === null)) {
                return res.status(400).json({ error: 'One or more users not found or invalid' });
            }

            const chat = new Chat({ name, members: userIds });
            await chat.save();

            res.status(201).json(chat);
        } catch (error) {
            console.error('Chat creation error:', error);
            res.status(500).json({ error: 'Chat creation error' });
        }
    },

    async joinChat(req, res) {
        try {
            const { email, chatId } = req.body;
            const chat = await Chat.findById(chatId);
            const userResponse = await axios.get(`http://localhost:5000/identity/user/GetUserProfile/profile?email=${email}`, {
                // headers: {
                //     Authorization: `Bearer ${req.headers.authorization}`
                // }
            });

            if (!userResponse.data) {
                return res.status(404).json({ error: 'User not found' });
            }

            const userId = userResponse.data.id;
            if (!chat.members.includes(userId)) {
                chat.members.push(userId);
                await chat.save();
            }

            res.json({ message: 'User connected to chat' });
        } catch (error) {
            console.error(`Error connecting to chat: ${error.message}`);
            res.status(500).json({ error: 'Error connecting to chat' });
        }
    },

    async leaveChat(req, res) {
        try {
            const { userId, chatId } = req.body;

            const chat = await Chat.findById(chatId);
            if (!chat) {
                return res.status(404).json({ error: 'Chat not found' });
            }

            if (!chat.members.includes(userId)) {
                return res.status(400).json({ error: 'User not in the chat' });
            }

            chat.members = chat.members.filter(id => id.toString() !== userId);
            await chat.save();

            res.json({ message: 'User has left the chat successfully' });
        } catch (error) {
            console.error('Error leaving chat:', error);
            res.status(500).json({ error: 'Error leaving chat' });
        }
    },

    async getUserChats(req, res) {
        try {
            const { userId } = req.query;
    
            if (!userId) {
                return res.status(400).json({ error: 'UserId is required' });
            }
    
            const chats = await Chat.find({ members: userId });
    
            if (!chats.length) {
                return res.status(404).json({ message: 'No chats found for this user' });
            }
    
            res.json(chats);
        } catch (error) {
            console.error('Error fetching chats:', error);
            res.status(500).json({ error: 'Error fetching user chats' });
        }
    }
};

module.exports = ChatController;
