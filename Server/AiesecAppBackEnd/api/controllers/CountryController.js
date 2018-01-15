/**
 * CountryController
 *
 * @description :: Server-side logic for managing Countries
 * @help        :: See http://sailsjs.org/#!/documentation/concepts/Controllers
 */

module.exports = {
    searchCountry: function (req, res) {
        var searchQuery = req.param('query');
        var searchObject = {};

        if (searchQuery) {
            searchObject = {
                or: [
                    { countryCode: { contains: searchQuery } },
                    { name: { contains: searchQuery } }
                ]
            }
        }

        Country.find({
            where: searchObject
        }).then(function (countries) {
            return res.json(countries);
        }).catch(function (error) {
            return res.json({ "status": res.statusCode, "message": error })
        });
    }
};

