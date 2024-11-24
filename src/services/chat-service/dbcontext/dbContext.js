const mongoose = require('mongoose');
const config = require("config");

async function connectDB() {
    try {
        await mongoose.connect(config.get("db-url"), { useNewUrlParser: true, useUnifiedTopology: true });
        console.log('MongoDB connected');
    } catch (error) {
        console.error('MongoDB connection error:', error);
    }
}

module.exports = connectDB;


