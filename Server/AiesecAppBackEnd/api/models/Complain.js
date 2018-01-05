/**
 * Complain.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {
  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'complain',
  identity: 'Complain',

  attributes: {
    idComplain: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    ExchangeParticipant_idExchangeParticipant: {
      model: 'ExchangeParticipant',
      required: true
    },

    Project_idProject: {
      model: 'Project',
      required: true
    },

    title:{
      type: 'string',
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

    complainStatus: {
      type: 'boolean',
      required: true,
      defaultsTo: false
    },

    expired: {
      type: 'boolean',
      required: true,
      defaultsTo: false
    }
  }
};

