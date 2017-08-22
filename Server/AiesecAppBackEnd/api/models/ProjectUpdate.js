/**
 * ProjectUpdate.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'projectupdate',
  identity: 'ProjectUpdate',

  attributes: {
    idProjectUpdate: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    Project_idProject: {
      model: 'Project',
      primaryKey: true
    },

    User_idUser: {
      model: 'User',
      primaryKey: true
    },

    description: {
      type: 'string',
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

