/**
 * ComplainController
 *
 * @description :: Server-side logic for managing Complains
 * @help        :: See http://sailsjs.org/#!/documentation/concepts/Controllers
 */

module.exports = {

    getComplainsByEpId: function (req, res) {
        var epId = req.param('id');
        Complain.find({
            where: { ExchangeParticipant_idExchangeParticipant: epId },
            sort: 'createdDate DESC'
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
        var projectId = req.param('id');
        Complain.find({
            where: { Project_idProject: projectId },
            sort: 'createdDate DESC'
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

