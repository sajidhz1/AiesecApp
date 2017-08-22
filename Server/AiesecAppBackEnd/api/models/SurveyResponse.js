/**
 * SurveyResponse.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'surveyresponse',
  identity: 'SurveyResponse',

  attributes: {
    idSurveyResponse: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    ExchangeParticipant_idExchangeParticipant: {
      model: 'ExchangeParticipant',
      primaryKey: true
    },

    Project_idProject: {
      model: 'Project',
      primaryKey: true
    },

    createdDate: {
      type: 'datetime'
    }
  }
};

