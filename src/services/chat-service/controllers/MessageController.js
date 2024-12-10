const Message = require('../models/Message');
const Chat = require('../models/Chat');
const mongoose = require('mongoose');

const MessageController = {
    async sendMessage(req, res) {
        try {
            const { chatId, senderId, text } = req.body;

            if (!mongoose.Types.ObjectId.isValid(chatId)) {
                return res.status(400).json({ error: 'Invalid chatId' });
            }
            const uuidPattern = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/;
            if (!uuidPattern.test(senderId)) {
                return res.status(400).json({ error: 'Invalid senderId format' });
            }

            const message = new Message({
                chatId: new mongoose.Types.ObjectId(chatId), 
                senderId: senderId,
                text
            });

            await message.save();
            res.status(201).json(message);
        } catch (error) {
            console.error('Error during message save:', error);
            res.status(500).json({ error: 'Error Message Sent' });
        }
    },

    async getMessageHistory(req, res) {
        try {
            const { chatId } = req.params; // отримуємо chatId з параметрів URL

            // Перевірка валідності chatId (чи це ObjectId)
            if (!mongoose.Types.ObjectId.isValid(chatId)) {
                return res.status(400).json({ error: 'Invalid chatId' });
            }

            // Отримуємо всі повідомлення для вказаного чату
            const messages = await Message.find({ chatId }).sort({ timestamp: -1 });

            if (messages.length === 0) {
                return res.status(404).json({ message: 'No messages found for this chat' });
            }

            res.json(messages);  // Повертаємо історію повідомлень
        } catch (error) {
            console.error('Error fetching message history:', error);  // Логування помилки
            res.status(500).json({ error: 'Error fetching message history' });
        }
    }
};

module.exports = MessageController;
