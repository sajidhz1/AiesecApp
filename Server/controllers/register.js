var express = require('express');

var router = express.Router();

var dbUser = require('../models/userModel');

router.post('/', function (req, res) {
    //Create new User.
    var data = {
        email: req.body.email,
        password: req.body.password
    };
    dbUser.addNewUser(data).then(function (userInsertId) {
        res.json({
            "error": false,
            "message": "Added new user",
            "newuserid": userInsertId
        });
    }).catch(function (error) {
        return res.json({ "error": true, "message": error })
    });
});

module.exports = router;