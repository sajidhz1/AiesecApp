/**
 * ProjectUserRole.js
 *
 * @description :: TODO: You might write a short summary of how this model works and what it represents here.
 * @docs        :: http://sailsjs.org/documentation/concepts/models-and-orm/models
 */

module.exports = {

  schema: true,
  autoCreatedAt: false,
  autoUpdatedAt: false,
  tableName: 'projectuserrole',
  identity: 'ProjectUserRole',

  attributes: {
    idProjectUserRole: {
      type: 'integer',
      primaryKey: true,
      unique: true,
      autoIncrement: true
    },

    User_idUser: {
      model: 'User',
      primaryKey: true
    },

    UserRole_idUserRole: {
      model: 'UserRole',
      primaryKey: true
    },

    Project_idProject: {
      model: 'Project',
      primaryKey: true
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
}
