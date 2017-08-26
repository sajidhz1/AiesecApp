/**
 * ExchangeParticipant.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'exchangeparticipant',
  identity: 'ExchangeParticipant',

  attributes: {
    idExchangeParticipant: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    User_idUser: {
      model: 'User',
      unique: true,
      required: true
    },

    Country_idCountry: {
      model: 'Country',
      required: true
    },

    Project_idProject: {
      model: 'Project',
      required: true
    }
  }
};

