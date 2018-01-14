/**
 * ComplainController
 *
 * @description :: Server-side logic for managing Complains
 * @help        :: See http://sailsjs.org/#!/documentation/concepts/Controllers
 */
var Promise = require('bluebird');

module.exports = {

    getComplainsByEpId: function (req, res) {
        var searchQuery = req.param('query');
        var epId = req.param('id');
        var searchObject = {};

        if (searchQuery) {
            searchObject = {
                or: [
                    { title: { contains: searchQuery } },
                    { description: { contains: searchQuery } }
                ]
            }
        }
        if (epId) {
            searchObject['ExchangeParticipant_idExchangeParticipant'] = epId;
        }

        Complain.find({
            where: searchObject
        }).then(function (complains) {
            return res.json({
                "status": res.statusCode,
                "complainsCount": complains.length,
                "complains": complains
            });
        }).catch(function (error) {
            return res.json({ "status": res.statusCode, "message": error })
        });
    },

    getComplainsByProjectId: function (req, res) {
        var searchQuery = req.param('query');
        var projectId = req.param('id');
        var searchObject = {};

        if (searchQuery) {
            searchObject = {
                or: [
                    { title: { contains: searchQuery } },
                    { description: { contains: searchQuery } }
                ]
            }
        }
        if (epId) {
            searchObject['Project_idProject'] = projectId;
        }

        Complain.find({
            where: searchObject
        }).then(function (complains) {
            return res.json({
                "status": res.statusCode,
                "complainsCount": complains.length,
                "complains": complains
            });
        }).catch(function (error) {
            return res.json({ "status": res.statusCode, "message": error })
        });
    },

    getComplainsByLcId: function (req, res) {
        var searchQuery = req.param('query');
        var lcId = req.param('id');
        var searchObject = {};

        var query = "SELECT * from complain As c INNER JOIN project AS p WHERE p.idProject = c.Project_idProject AND p.LocalCommitte_idLocalCommitte = ?"

        var complainQueryAsync = Promise.promisify(Complain.query);

        complainQueryAsync(query, [lcId]).then(function (complains) {
            return res.json({
                "status": res.statusCode,
                "complainsCount": complains.length,
                "complains": complains
            });
        }).catch(function (error) {
            return res.json({ "status": res.statusCode, "message": error })
        });
    },

    epComplainResolve: function (req, res) {
        var complainId = req.param('id');
        Complain.update({ where: { idComplain: complainId } }, req.body).exec(function afterwards(error, updated) {
            if (error) {
                return res.json({ "status": res.statusCode, "message": error });
            }
            return res.json({
                "status": res.statusCode,
                "complain": updated[0]
            });
        });
    },

    tlComplainResolve: function (req, res) {
        var complainId = req.param('id');
        Complain.update({ where: { idComplain: complainId } }, req.body).exec(function afterwards(error, updated) {
            if (error) {
                return res.json({ "status": res.statusCode, "message": error });
            }
            return res.json({
                "status": res.statusCode,
                "complain": updated[0]
            });
        });
    }
};

