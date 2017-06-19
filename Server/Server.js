var express = require("express");
var mysql   = require("mysql");
var bodyParser  = require("body-parser");
var md5 = require('MD5');
var rest = require("./REST.js");
var protectedRoutes = require("./ProtectedRoutes.js");
var app  = express();

var jwt    = require('jsonwebtoken'); // used to create, sign, and verify tokens
var jwToken = require('express-jwt');
var config = require('./Config');

function REST(){
    var self = this;
    self.connectMysql();
};

REST.prototype.connectMysql = function() {
    var self = this;
    var pool      =    mysql.createPool({
        connectionLimit : 100,
        host     : 'localhost',
        user     : 'root',
        password : '1234',
        database : 'aiesec',
        debug    :  false
    });
    pool.getConnection(function(err,connection){
        if(err) {
          self.stop(err);
        } else {
          self.configureExpress(connection);
        }
    });
}

REST.prototype.configureExpress = function(connection) {
      var self = this;
      app.use(bodyParser.urlencoded({ extended: true }));
      app.use(bodyParser.json());
	  
      var router = express.Router();
      app.use('/api', router);
	  var rest_router = new rest(router,connection,md5);
	  
	  router = express.Router();
	  app.use('/api/protected', jwtCheck, requireScope('full_access'));	   	 
	  var protected_routes = new protectedRoutes(router, connection);
	  
      self.startServer();
}

REST.prototype.startServer = function() {
      app.listen(3000,function(){
          console.log("All right ! I am alive at Port 3000.");
      });
}

REST.prototype.stop = function(err) {
    console.log("ISSUE WITH MYSQL n" + err);
    process.exit(1);
}


var jwtCheck = jwToken({
  secret: config.secret,
  audience: config.audience,
  issuer: config.issuer
});

// Check for scope
function requireScope(scope) {
  return function (req, res, next) {
    var has_scopes = req.user.scope === scope;
    if (!has_scopes) { 
        res.sendStatus(401); 
        return;
    }
    next();
  };
}


new REST();
