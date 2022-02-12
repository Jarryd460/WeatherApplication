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

export class Token {
    'value'?: string | null;
    'expiration'?: Date;

    static discriminator: string | undefined = undefined;

    static attributeTypeMap: Array<{name: string, baseName: string, type: string}> = [
        {
            "name": "value",
            "baseName": "value",
            "type": "string"
        },
        {
            "name": "expiration",
            "baseName": "expiration",
            "type": "Date"
        }    ];

    static getAttributeTypeMap() {
        return Token.attributeTypeMap;
    }
}
