/**
 * ComplainReply.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'complainreply',
  identity: 'ComplainReply',

  attributes: {
    idComplainReply: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    Complain_idComplain: {
      model: 'Complain',
      required: true
    },

    User_idUser: {
      model: 'User',
      required: true
    },

    replyText: {
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

