var mysql = require("mysql");
var jwt    = require('jsonwebtoken');
var express = require("express");
var app  = express();
var config = require('./Config'); // get our config file

function REST_ROUTER(router, connection, md5) {
    var self = this;
    self.handleRoutes(router, connection, md5);
}

REST_ROUTER.prototype.handleRoutes = function (router, connection, md5) {
    router.get("/", function (req, res) {
        res.json({ "Message": "Hello World !" });
    });

    router.post("/users", function (req, res) {
        var query = "INSERT INTO ??(??,??) VALUES (?,?)";
        var table = ["user_login", "user_email", "user_password", req.body.email, md5(req.body.password)];
        query = mysql.format(query, table);
        connection.query(query, function (err, rows) {
            if (err) {				
                res.json({ "Error": true, "Message": "Error executing MySQL query" });
            } else {
                res.json({ "Error": false, "user_id": rows.insertId });
            }
        });
    });

    router.get("/users", function (req, res) {
        var query = "SELECT * FROM ??";
        var table = ["user_login"];
        query = mysql.format(query, table);
        connection.query(query, function (err, rows) {
            if (err) {
                res.json({ "Error": true, "Message": "Error" })
            } else {
                res.json({ "Error": false, "Message": "Success" })
            }
        });
    });

    router.get("/users/:user_id", function (req, res) {
        var query = "SELECT * FROM ?? WHERE ??=?";
        var table = ["user_login", "user_id", req.params.user_id];
        query = mysql.format(query, table);
        connection.query(query, function (err, rows) {
            if (err) {
                res.json({ "Error": true, "Message": "Error executing MySQL query" });
            } else {
                res.json({ "Error": false, "Message": "Success", "Users": rows });
            }
        });
    });

    router.put("/users",function(req,res){
        var query = "UPDATE ?? SET ?? = ? WHERE ?? = ?";
        var table = ["user_login","user_password",md5(req.body.password),"user_email",req.body.email];
        query = mysql.format(query,table);
        connection.query(query,function(err,rows){
            if(err) {
                res.json({"Error" : true, "Message" : "Error executing MySQL query"});
            } else {
                res.json({"Error" : false, "Message" : "Updated the password for email "+req.body.email});
            }
        });
    });
	
	router.post('/authenticate', function(req, res) {

		var query = "SELECT * FROM ?? WHERE ??=?";
        var table = ["user_login", "user_email", req.body.email];	
		query = mysql.format(query, table);
		connection.query(query, function (err, rows) {
            if (err) {				
                res.json({ "Error": true, "Message": "Error executing MySQL query" });
            } else {
                if (rows.user_id == "") {
				  res.json({ success: false, message: 'Authentication failed. User not found.' });
				} else{

				  // check if password matches
				  if (rows.user_password != req.body.password) {
					res.json({ success: false, message: 'Authentication failed. Wrong password.' });
				  } else {

				  var user = {
					user_email: rows.user_email,
					user_password: rows.user_password,
					user_Id: rows.user_id,
					user_join_date: rows.user_join_date
				  };
					// if user is found and password is right
					// create a token
					var token = jwt.sign(user , config.secret , {
					  expiresIn : 1440 // expires in 24 hours
					});

					// return the information including token as JSON
					res.json({
					  success: true,
					  token: token
					});
				  }   

			}
        }
        });
	
	});
}

module.exports = REST_ROUTER;