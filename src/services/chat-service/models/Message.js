const mongoose = require('mongoose');

const MessageSchema = new mongoose.Schema({
    chatId: { type: mongoose.Schema.Types.ObjectId, ref: 'Chat', required: true },
    senderId: { type: String,  required: true },
    text: { type: String, required: true },
}, { timestamps: true });

module.exports = mongoose.model('Message', MessageSchema);