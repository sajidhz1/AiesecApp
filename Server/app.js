var express = require('express');
var bodyParser = require('body-parser');
var path = require('path')
var logger = require('morgan');
global.config = require('./config');

var app = express();

app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

app.all('/*', function(req, res, next) {
  // CORS headers
  res.header("Access-Control-Allow-Origin", "*"); // restrict it to the required domain
  res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE,OPTIONS');
  // Set custom headers for CORS
  res.header('Access-Control-Allow-Headers', 'Content-type,Accept,X-Access-Token,X-Key');
  if (req.method == 'OPTIONS') {
    res.status(200).end();
  } else {
    next();
  }
});

app.all('/api/auth/*', [require('./middlewares/validateRequest')]);

app.use('/',require('./controllers')); //routes which does't require token authentication should be placed here


// If no route is matched by now, it must be a 404
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

app.set('port', process.env.PORT || config.port);

app.listen(app.get('port'),function() {
  console.log("Listening at Port "+config.port);
});