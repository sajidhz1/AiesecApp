// var mysql = require("mysql");
// var jwt    = require('jsonwebtoken');
// var express = require("express");
// var app  = express();
// var config = require('./Config'); // get our config file

// function REST_ROUTER(router, connection, md5) {
//     var self = this;
//     self.handleRoutes(router, connection, md5);
// }

// REST_ROUTER.prototype.handleRoutes = function (router, connection, md5) {
//     router.get("/", function (req, res) {
//         res.json({ "Message": "Hello World !" });
//     });

//     router.post("/users", function (req, res) {
//         var query = "INSERT INTO ??(??,??) VALUES (?,?)";
//         var table = ["user_login", "user_email", "user_password", req.body.email, md5(req.body.password , config.secret)];
//         query = mysql.format(query, table);
//         connection.query(query, function (err, rows) {
//             if (err) {				
//                 res.json({ "Error": true, "Message": "Error executing MySQL query" });
//             } else {
//                 res.json({ "Error": false, "user_id": rows.insertId });
//             }
//         });
//     });

//     router.get("/users", function (req, res) {
//         var query = "SELECT * FROM ??";
//         var table = ["user_login"];
//         query = mysql.format(query, table);
//         connection.query(query, function (err, rows) {
//             if (err) {
//                 res.json({ "Error": true, "Message": "Error" })
//             } else {
//                 res.json({ "Error": false, "Message": "Success" })
//             }
//         });
//     });

//     router.get("/users/:user_id", function (req, res) {
//         var query = "SELECT * FROM ?? WHERE ??=?";
//         var table = ["user_login", "user_id", req.params.user_id];
//         query = mysql.format(query, table);
//         connection.query(query, function (err, rows) {
//             if (err) {
//                 res.json({ "Error": true, "Message": "Error executing MySQL query" });
//             } else {
//                 res.json({ "Error": false, "Message": "Success", "Users": rows });
//             }
//         });
//     });

//     router.put("/users",function(req,res){
//         var query = "UPDATE ?? SET ?? = ? WHERE ?? = ?";
//         var table = ["user_login","user_password",md5(req.body.password),"user_email",req.body.email];
//         query = mysql.format(query,table);
//         connection.query(query,function(err,rows){
//             if(err) {
//                 res.json({"Error" : true, "Message" : "Error executing MySQL query"});
//             } else {
//                 res.json({"Error" : false, "Message" : "Updated the password for email "+req.body.email});
//             }
//         });
//     });
	
// 	//returns a id token and access token upon authentication
// 	router.post('/authenticate', function(req, res) {

// 		var query = "SELECT * FROM ?? WHERE ??=?";
//         var table = ["user_login", "user_email", req.body.email];	
// 		query = mysql.format(query, table);
// 		connection.query(query, function (err, rows) {
//             if (err) {				
//                 res.json({ "Error": true, "Message": "Error executing MySQL query" });
//             } else {
//                 if (rows.length <= 0) {
// 				  res.json({ success: false, message: 'Authentication failed. User not found.' });
// 				} else{

// 				  // check if password matches
// 				  if (rows[0].user_password !== md5(req.body.password , config.secret)) {
// 					res.json({ success: false, message: 'Authentication failed. Wrong password.' });
// 				  } else {

// 				  var user = {
// 					user_email: rows.user_email,
// 					user_Id: rows.user_id,
// 					user_join_date: rows.user_join_date
// 				  };

// 					// return the information including token as JSON
// 					res.json({
// 					  success: true,
// 					  id_token : createIdToken(user),
// 					  access_token : createAccessToken(),
// 					  user : md5(req.body.password , config.secret)
// 					});
// 				  }   

// 			}
//         }
//         });
	
// 	});	
	
// }

// function genJti() {
//   let jti = '';
//   let possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
//   for (let i = 0; i < 16; i++) {
//       jti += possible.charAt(Math.floor(Math.random() * possible.length));
//   }
  
//   return jti;
// }

// function createIdToken(user) {
//   return jwt.sign(user, config.secret, { expiresIn: 60*60*5 });
// }

// function createAccessToken() {
//   return jwt.sign({
//     iss: config.issuer,
//     aud: config.audience,
//     exp: Math.floor(Date.now() / 1000) + (60 * 60),
//     scope: 'full_access',
//     sub: "lalaland|gonto",
//     jti: genJti(), // unique identifier for the token
//     alg: 'HS256'
//   }, config.secret);
// }

// module.exports = REST_ROUTER;