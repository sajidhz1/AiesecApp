/**
 * User.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'user',
  identity: 'User',

  attributes: {
    idUser: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true   
    },

    email: {
      type: 'email',
      required: true,
      unique: true
    },

    username: {
      type: 'string',
      required: true,
      unique: true,
      alphanumericdashed: true
    },

    password: {
      type: 'string',
      required: true
    },

    createdDate: {
      type: 'datetime'
    },

    updatedDate: {
      type: 'datetime'
    },

    approved: {
      type: 'integer',
      required: true,
      defaultsTo: 0
    },

    userType: {
      type: 'integer',
      required: true
    },

    expired: {
      type: 'boolean',
      required: true,
      defaultsTo: false
    },

    LocalCommitte_idLocalCommitte: {
      model: 'Localcommitte'
    },

    toJSON: function () {
      var obj = this.toObject();
      delete obj.password;
      return obj;
    }
  },

  //one of the nice things about sails is that it makes our services available globally.
  beforeUpdate: function (values, next) {
    CipherService.hashPassword(values);
    next();
  },
  beforeCreate: function (values, next) {
    CipherService.hashPassword(values);
    next();
  }
};

