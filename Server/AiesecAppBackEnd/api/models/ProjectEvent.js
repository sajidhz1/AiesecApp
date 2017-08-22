/**
 * ProjectEvent.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'projectevent',
  identity: 'ProjectEvent',

  attributes: {
    idProjectEvent: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    Project_idProject: {
      model: 'Project',
      required: true
    },

    User_idUser: {
      model: 'User',
      required: true
    },

    venue: {
      type: 'string',
      required: true
    },

    start: {
      type: 'datetime',
      required: true
    },

    end: {
      type: 'datetime',
      required: true
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

