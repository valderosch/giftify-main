const Message = require('../models/Message');
const Chat = require('../models/Chat');

const MessageController = {
    async sendMessage(req, res) {
        try {
            const { chatId, senderId, text } = req.body;
            const message = new Message({ chatId, senderId, text });
            await message.save();
            res.status(201).json(message);
        } catch (error) {
            res.status(500).json({ error: 'Помилка при відправленні повідомлення' });
        }
    },

    async getMessageHistory(req, res) {
        try {
            const { chatId } = req.params;
            const messages = await Message.find({ chatId }).sort({ timestamp: -1 });
            res.json(messages);
        } catch (error) {
            res.status(500).json({ error: 'Помилка при отриманні історії повідомлень' });
        }
    }
};

module.exports = MessageController;
