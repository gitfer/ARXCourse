
angular.module('myDocumental.config', [])
    .constant('arxivarAppConfig', {
        'prefix': 'ARX',
        'root': rootPath,
        'rootApi': 'http://localhost:49860/api/v0',
        'templateRootPath': rootPath + '/Scripts/app/',
        'viewRootPath': rootPath + '/Scripts/app/' + 'views/',
        'defaultLanguage': 'it'
    });
