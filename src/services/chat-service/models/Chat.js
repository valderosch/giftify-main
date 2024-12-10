const mongoose = require('mongoose');

const ChatSchema = new mongoose.Schema({
    name: { type: String, required: true },
    members: [{ type: String }],
}, { timestamps: true });

module.exports = mongoose.model('Chat', ChatSchema);