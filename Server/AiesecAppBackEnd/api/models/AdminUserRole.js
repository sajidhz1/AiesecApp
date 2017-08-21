/**
 * AdminUserRole.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'adminuserrole',
  identity: 'AdminUserRole',

  attributes: {
    idAdminUserRole: {
      type: 'integer',
      primaryKey: true,
      unique: true
    },

    adminRole: {
      type: 'string',
      required: true
    },

    description: {
      type: 'string',
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
    },
  }

};

