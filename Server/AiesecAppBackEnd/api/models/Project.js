/**
 * Project.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'project',
  identity: 'Project',

  attributes: {
    idProject: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      required: true
    },

    LocalCommitte_idLocalCommitte: {
      primaryKey: true,
      model: 'LocalCommitte',
      required: true
    },

    projectName: {
      type: 'string',
      required: true
    },

    version: {
      type: 'string',
      required: true
    },

    shortDescription: {
      type: 'string',
      required: true
    },

    longDescription: {
      type: 'string',
    },

    startDate: {
      type: 'date',
      required: true
    },

    endDate: {
      type: 'date',
      required: true
    },

    createdDate: {
      type: 'datetime'
    },

    expired: {
      type: 'boolean',
      required: true,
      defaultsTo: false
    }
  }
};

