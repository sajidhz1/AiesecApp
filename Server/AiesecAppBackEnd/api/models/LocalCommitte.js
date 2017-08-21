/**
 * LocalCommitte.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'localcommitte',
  identity: 'LocalCommitte',

  attributes: {
    idLocalCommitte: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      required: true,
      autoIncrement: true
    },

    lcCode: {
      type: 'string',
      unique: true,
      required: true
    },

    officialAddress: {
      type: 'string',
      required: true
    },

    contactNumber: {
      type: 'string',
      required: true
    },

    shortDescription: {
      type: 'string',
    }
  }
};

