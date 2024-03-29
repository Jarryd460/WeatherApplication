"use strict";
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
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginCredentials = void 0;
class LoginCredentials {
    static getAttributeTypeMap() {
        return LoginCredentials.attributeTypeMap;
    }
}
exports.LoginCredentials = LoginCredentials;
LoginCredentials.discriminator = undefined;
LoginCredentials.attributeTypeMap = [
    {
        "name": "username",
        "baseName": "username",
        "type": "string"
    },
    {
        "name": "password",
        "baseName": "password",
        "type": "string"
    }
];
//# sourceMappingURL=loginCredentials.js.map