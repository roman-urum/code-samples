'use strict';

define([
    'Controllers/Site/Dashboard/Helpers/PatientStorage',
    'Controllers/Site/Dashboard/Helpers/DashboardOptions',
    'Controllers/Site/Dashboard/Helpers/ErrorHandler'
], function (PatientStorage, DashboardOptions, ErrorHandler) {
    return {
        patientStorage: PatientStorage,
        errorHandler: ErrorHandler,
        OPTIONS: DashboardOptions,
        app: {
            views: {},
            models: {},
            collections: {}
        }
    };
});