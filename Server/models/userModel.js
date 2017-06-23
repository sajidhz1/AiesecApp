var mysql = require("mysql");
var md5 = require('MD5');
var bcrypt = require('bcrypt');
var async = require('async');

var config = require('../config'); // get our config file
var dbconnection = require('../middlewares/database'); //get the db connection to model

module.exports = {
    addNewUser(userData) {
        return new Promise((resolve, reject) => {
            dbconnection.getConnection().then(function (con) {
                bcrypt.hash(userData.password, config.saltrounds).then(function (hash) {
                    var query = "INSERT INTO ??(??,??) VALUES (?,?)";
                    var table = ["user_login", "user_email", "user_password", userData.email, hash];
                    query = mysql.format(query, table);

                    con.query(query, function (err, rows) {
                        if (err) {
                            return reject(err)
                        }
                        return resolve(rows.insertId)
                    })
                }).catch(function (err) {
                    return reject(err)
                })
            }).catch(function (err) {
                return reject(err)
            })
        })
    },

    findUser(emailAddress) {
        return new Promise(function (resolve, reject) {
            dbconnection.getConnection().then(function (con) {
                var query = "SELECT * FROM ?? WHERE ?? = ?";
                var table = ["user_login", "user_email", emailAddress];
                query = mysql.format(query, table);

                con.query(query, function (err, result) {
                    if (err) {
                        return reject(err)
                    }
                    return resolve(result[0])
                })
            }).catch(function (err) {
                return reject(err)
            })
        })
    }
}

// module.exports = UserModel;