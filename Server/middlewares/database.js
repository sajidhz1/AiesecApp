var mysql = require("mysql");

var pool = mysql.createPool({
    connectionLimit: 100,
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'aiesec',
    debug: false
});

function stop(err) {
    console.log("ISSUE WITH MYSQL : " + err);
    process.exit(1);
}

module.exports.getConnection = function getConnection() {
    return new Promise((resolve, reject) => {
        pool.getConnection(function (err, connection) {
            if (err) {
                stop(err);
                return reject(err);
            }
            return resolve(connection);
        });
    })
}



// module.exports = getConnection;