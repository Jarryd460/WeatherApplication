/**
 * API
 * An Api to perform weather forecasts
 *
 * The version of the OpenAPI document: 1
 * Contact: jon.doe@gmail.com
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { RequestFile } from './models';

export class LoginCredentials {
    /**
    * Username
    */
    'username'?: string | null;
    /**
    * Password
    */
    'password'?: string | null;

    static discriminator: string | undefined = undefined;

    static attributeTypeMap: Array<{name: string, baseName: string, type: string}> = [
        {
            "name": "username",
            "baseName": "username",
            "type": "string"
        },
        {
            "name": "password",
            "baseName": "password",
            "type": "string"
        }    ];

    static getAttributeTypeMap() {
        return LoginCredentials.attributeTypeMap;
    }
}

