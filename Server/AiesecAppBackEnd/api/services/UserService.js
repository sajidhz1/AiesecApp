var Promise = require('bluebird');

module.exports = {

    getUserProject: function (user) {
        var userType = user.userType;
        switch (userType) {
            case 1:
                return new Promise();
                break;

            case 2:
                return getProjectUserProject(user.idUser);
                break;

            case 3:
                return getEpUserProject(user.idUser);
                break;
        }
    }
};

function getEpUserProject(userId) {
    var query = "SELECT p.idProject,p.projectName, p.version ,p.shortDescription,p.startDate,p.endDate, p.expired FROM project AS p INNER JOIN exchangeparticipant AS e ON e.Project_idProject = p.idProject WHERE e.User_idUser = ? LIMIT 1";
    var projectQueryAsync = Promise.promisify(Project.query);
    return projectQueryAsync(query, [userId]);
}

function getProjectUserProject(userId) {
    var query = "SELECT p.idProject,p.projectName, p.version ,p.shortDescription,p.startDate,p.endDate, p.expired FROM project AS p INNER JOIN projectuserrole AS pr ON pr.Project_idProject = p.idProject WHERE pr.User_idUser = ? LIMIT 1";
    var projectQueryAsync = Promise.promisify(Project.query);
    return projectQueryAsync(query, [userId]);
}