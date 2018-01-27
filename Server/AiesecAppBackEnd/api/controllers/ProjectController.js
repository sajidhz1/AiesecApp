/**
 * ProjectController
 *
 * @description :: Server-side logic for managing Projects
 * @help        :: See http://sailsjs.org/#!/documentation/concepts/Controllers
 */

module.exports = {
    searchProject: function (req, res) {
        var searchQuery = req.param('query');
        var searchObject = {};

        if (searchQuery) {
            searchObject = {
                or: [
                    { projectName: { contains: searchQuery } },
                    { shortDescription: { contains: searchQuery } }
                ]
            }
        }

        Project.find({
            where: searchObject
        }).then(function (projects) {
            return res.json(projects);
        }).catch(function (error) {
            return res.json({ "status": res.statusCode, "message": error })
        });
    }
};

