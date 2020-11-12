define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.Initialize = exports.extensionObject = exports.blazorExtensions = exports.SpectoLogicTeams = void 0;
    class SpectoLogicTeams {
        Initialize() {
            console.log("Initializing teams,...");
            try {
                microsoftTeams.initialize();
                microsoftTeams.getContext((context) => {
                    this._context = context;
                });
            }
            catch (err) {
                alert(err.message);
            }
        }
        GetUPN() {
            return Object.keys(this._context).length > 0 ? this._context['upn'] : "";
        }
        GetClientToken() {
            try {
                const promise = new Promise((resolve, reject) => {
                    console.log("1. Get auth token from Microsoft Teams");
                    microsoftTeams.authentication.getAuthToken({
                        successCallback: (result) => {
                            resolve(result);
                        },
                        failureCallback: function (error) {
                            alert(error);
                            reject("Error getting token: " + error);
                        }
                    });
                });
                return promise;
            }
            catch (err) {
                alert(err.message);
            }
        }
        GetServerToken(clientSideToken, scopes) {
            try {
                const promise = new Promise((resolve, reject) => {
                    microsoftTeams.getContext((context) => {
                        fetch('/auth/token', {
                            method: 'post',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify({
                                'tid': context.tid,
                                'token': clientSideToken,
                                'scopes': scopes
                            }),
                            mode: 'cors',
                            cache: 'default'
                        })
                            .then((response) => {
                            if (response.ok) {
                                return response.json();
                            }
                            else {
                                reject(response.statusText);
                            }
                        })
                            .then((responseJson) => {
                            if (responseJson.error) {
                                reject(responseJson.error);
                            }
                            else {
                                const serverSideToken = responseJson;
                                console.log(serverSideToken);
                                resolve(serverSideToken);
                            }
                        });
                    });
                });
                return promise;
            }
            catch (err) {
                alert(err.message);
            }
        }
    }
    exports.SpectoLogicTeams = SpectoLogicTeams;
    exports.blazorExtensions = 'BlazorExtensions';
    // define what this extension adds to the window object inside BlazorExtensions
    exports.extensionObject = {
        SpectoLogicMSTeams: new SpectoLogicTeams()
    };
    function Initialize() {
        if (typeof window !== 'undefined' && !window[exports.blazorExtensions]) {
            // when the library is loaded in a browser via a <script> element, make the
            // following APIs available in global scope for invocation from JS
            window[exports.blazorExtensions] = Object.assign({}, exports.extensionObject);
        }
        else {
            window[exports.blazorExtensions] = Object.assign(Object.assign({}, window[exports.blazorExtensions]), exports.extensionObject);
        }
        let extObj;
        extObj = window['BlazorExtensions'];
        extObj.SpectoLogicMSTeams.Initialize();
    }
    exports.Initialize = Initialize;
});
//# sourceMappingURL=SpectoLogic.Blazor.MSTeams.js.map