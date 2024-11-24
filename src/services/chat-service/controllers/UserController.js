const User = require('../models/User');

const UserController = {
    async register(req, res) {
        try {
            const { username, password } = req.body;
            const user = new User({ username, password });
            await user.save();
            res.status(201).json(user);
        } catch (error) {
            res.status(500).json({ error: 'Error creating user' });
        }
    },

    async setStatusOnline(req, res) {
        try {
            const user = await User.findById(req.user.id);
            user.isOnline = true;
            await user.save();
            res.json({ message: 'User is now online' });
        } catch (error) {
            res.status(500).json({ error: 'Error updating status' });
        }
    },

    async setStatusOffline(req, res) {
        try {
            const user = await User.findById(req.user.id);
            user.isOnline = false;
            await user.save();
            res.json({ message: 'User is now offline' });
        } catch (error) {
            res.status(500).json({ error: 'Error updating status' });
        }
    },

    async blockUser(req, res) {
        try {
            const user = await User.findById(req.user.id);
            const userToBlock = req.body.userId;
            user.blockedUsers.push(userToBlock);
            await user.save();
            res.json({ message: 'User blocked successfully' });
        } catch (error) {
            res.status(500).json({ error: 'Error blocking user' });
        }
    },

    async unblockUser(req, res) {
        try {
            const user = await User.findById(req.user.id);
            user.blockedUsers = user.blockedUsers.filter(id => id.toString() !== req.body.userId);
            await user.save();
            res.json({ message: 'User unblocked successfully' });
        } catch (error) {
            res.status(500).json({ error: 'Error unblocking user' });
        }
    }
};

module.exports = UserController;
