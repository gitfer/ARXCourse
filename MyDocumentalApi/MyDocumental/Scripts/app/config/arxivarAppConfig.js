
angular.module('myDocumental.config', [])
    .constant('ARXivarAppConfig', {
        'prefix': 'ARX',
        'root': rootPath,
        'rootApi': rootPath + '/api/',
        'templateRootPath': rootPath + '/Scripts/app/',
        'viewRootPath': rootPath + '/Scripts/app/' + 'views/',
        'defaultLanguage': 'it'
    });
