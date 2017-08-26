/**
 * UserPersonalInfo.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'userpersonalinfo',
  identity: 'UserPersonalInfo',

  attributes: {
    idUserPersonalInfo: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    User_idUser: {
      model: 'User',
      required: true,
      unique: true
    },

    firstName: {
      type: 'string',
      required: true
    },

    lastName: {
      type: 'string',
      required: true
    },

    sex: {
      type: 'string'
    },

    shortBio: {
      type: 'string'
    },

    currentResidence: {
      string: 'string',
      required: true
    },

    mobileNumber: {
      string: 'string',
      required: true
    }

  }
};

