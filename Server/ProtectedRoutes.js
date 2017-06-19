var mysql = require("mysql");
var jwt    = require('jsonwebtoken');
var express = require("express");
var app  = express();
var config = require('./Config'); // get our config file

function PROTECTED_ROUTER(router, connection) {
    var self = this;
    self.handleRoutes(router, connection);
}

PROTECTED_ROUTER.prototype.handleRoutes = function (router, connection, md5) {
    router.get("/", function (req, res) {
        res.json({ "Message": "Hello World !" });
    });
  
    router.get('/protected/userdetails', function(req, res) {
	    res.status(200).send("test");
	});
}

module.exports = PROTECTED_ROUTER;