/**
 * AdminUser.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'adminuser',
  identity: 'AdminUser',

  attributes: {
    AdminUserRole_idAdminUserRole: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      required: true,
      autoIncrement: true
    },

    User_idUser: {
      model: 'User',
      primaryKey: true,
      required: true
    },

    createdDate: {
      type: 'datetime'
    },

    updatedDate: {
      type: 'datetime'
    },

    expired: {
      type: 'boolean',
      required: true,
      defaultsTo: false
    }
  }
};

