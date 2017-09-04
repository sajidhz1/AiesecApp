/**
 * Country.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'country',
  identity: 'Country',

  attributes: {

    idCountry: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    name: {
      type: 'string',
      unique: true,
      required: true
    },

    countryCode: {
      type: 'string',
      unique: true,
      required: true
    },

    description: {
      type: 'string',
    }
    // ,

    // locationCoordinates: {
    //   type: 'string',
    // }
  }
};

