var express = require('express');
var router = express.Router();

var auth = require('./auth.js');
var register = require('./register.js');
var user = require('./user.js');

function stop(err) {
    console.log("ISSUE WITH MYSQL n" + err);
    process.exit(1);
}

router.get('/', function (req, res) {
    res.json({ message: "Hello World" });
});

/*
 * Routes that can be accessed by any one
 */
router.use('/api/login', auth);
router.use('/api/register', register);

/*
 * Routes that can be accessed only by autheticated users
 */
router.use('/api/auth/user', user);

/*
 * Routes that can be accessed only by authenticated & authorized users
 */

module.exports = router;