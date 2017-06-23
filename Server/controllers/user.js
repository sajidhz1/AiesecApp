var express = require('express');
var mysql = require("mysql");
var bcrypt = require('bcrypt');
var jwt = require('jsonwebtoken');

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

router.get('/:useremail', function (req, res) {
    // Find a user by email
    var email = req.params.useremail;
    dbUser.findUser(email).then(function (user) {
        res.json({
            "error": false,
            "user": user
        });
    }).catch(function (error) {
        return res.json({ "error": true, "message": error })
    });
});

router.post('/login', function (req, res) {
    //Authenticate a user by email and password
    var email = req.body.email;
    var password = req.body.password;

    dbUser.findUser(email).then(function (user) {
        bcrypt.compare(password, user.user_password).then(function (result) {
            if (result) {
                return res.json({ "error": false, "message": "User found for given credentials" })
            } else {
                return res.json({ "error": false, "message": "Invalid credetials were provided!" })
            }
        })
        .catch(function (error) {
            return res.json({ "error": true, "message": "hash err" })
        });
    }).catch(function (error) {
        return res.json({ "error": true, "message": error })
    });
});

module.exports = router;