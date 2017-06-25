var express = require('express');
var bcrypt = require('bcrypt');
var jwt = require('jwt-simple');

var router = express.Router();
var dbUser = require('../models/userModel');
var config = require('../Config')

router.post('/', function (req, res) {
    //Authenticate a user by email and password
    var email = req.body.email || '';
    var password = req.body.password || '';

    if (email == '' || password == '') {
        res.status(401);
        res.json({
            "status": 401,
            "message": "Invalid credentials"
        });
        return;
    }

    // Fire a query to your DB and check if the credentials are valid
    dbUser.findUser(email).then(function (user) {
        bcrypt.compare(password, user.user_password).then(function (result) {
            if (result) {
                // If authentication is success, we will generate a token
                // and dispatch it to the client
                res.json(genToken(user));
            } else {
                res.status(401);
                res.json({
                    "status": 401,
                    "message": "Invalid credentials"
                });
                return;
            }
        })
            .catch(function (error) {
                return res.json({ "error": true, "message": error })
            });
    }).catch(function (error) {
        return res.json({ "error": true, "message": error })
    });
});


// private methods
function genToken(user) {
    var expires = expiresIn(7); // 7 days
    var token = jwt.encode({
        exp: expires
    }, config.secret);

    return {
        token: token,
        expires: expires,
        user: user
    };
}

function expiresIn(numDays) {
    var dateObj = new Date();
    return dateObj.setDate(dateObj.getDate() + numDays);
}

module.exports = router;