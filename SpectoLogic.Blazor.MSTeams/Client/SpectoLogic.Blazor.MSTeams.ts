import { Context } from "@microsoft/teams-js";

interface SpectoLogicTeamsInterface {
    Initialize(): void;
    GetUPN(): string;
    GetClientToken(): Promise<string>;
}

export class SpectoLogicTeams implements SpectoLogicTeamsInterface {

    private _context: Context;

    Initialize(): void {
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

    GetUPN(): string {
        return Object.keys(this._context).length > 0 ? this._context['upn'] : "";
    }

    GetClientToken(): Promise<string> {
        try {
            const promise = new Promise<string>((resolve, reject) => {

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

    GetServerToken(clientSideToken: string, scopes: string[]): Promise<string> {
        try {
            const promise = new Promise<string>((resolve, reject) => {
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
                            } else {
                                reject(response.statusText);
                            }
                        })
                        .then((responseJson) => {
                            if (responseJson.error) {
                                reject(responseJson.error);
                            } else {
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

export const blazorExtensions = 'BlazorExtensions';
// define what this extension adds to the window object inside BlazorExtensions
export const extensionObject = {
    SpectoLogicMSTeams: new SpectoLogicTeams()
};

export function Initialize(): void {
    if (typeof window !== 'undefined' && !window[blazorExtensions]) {
        // when the library is loaded in a browser via a <script> element, make the
        // following APIs available in global scope for invocation from JS
        window[blazorExtensions] = {
            ...extensionObject
        };
    }
    else {
        window[blazorExtensions] = {
            ...window[blazorExtensions],
            ...extensionObject
        };
    }
    let extObj: any;
    extObj = window['BlazorExtensions'];
    extObj.SpectoLogicMSTeams.Initialize();
}
