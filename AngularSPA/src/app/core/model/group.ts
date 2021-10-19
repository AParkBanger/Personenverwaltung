/**
 * WebApi
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { Person } from '././person';
import { FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
/*
https://github.com/swagger-api/swagger-codegen/wiki/Mustache-Template-Variables
*/
import { BaseModel } from '../variables';

/**
 * Generated Model Interface for Model "Group"
 *
 */
export interface IGroup {

    id?: number;
    name?: string;
    persons?: Array<Person>;
}
/**
 * Generated Model Class for Model "Group"
 *
 */
export class Group extends BaseModel implements IGroup {
    public  static readonly MODEL  = 'Group';
    public                  MODEL  = 'Group';


    public id?: number;
    public name?: string;
    public persons?: Array<Person>;


    /**
     * constructor
     * @param values Can be used to set a webapi response or formValues to this newly constructed model
     * @useFormGroupValuesToModel if true use formValues
    */
    constructor(values?: any, useFormGroupValuesToModel = false) {
       super();

        if (values) {
            this.setValues(values, useFormGroupValuesToModel);
        }

        // define non enumerable properties so these are omitted in JSON.stringify.
        Object.defineProperty(this, 'MODEL', {
            enumerable: false,
            writable: false
        });
    }


    /**
     * set the values.
     * @param values Can be used to set a webapi response to this newly constructed model
     */
    public setValues(values: any, useFormGroupValuesToModel = false): void {
        if (values) {
            const rawValues = this.getValuesToUse(values, useFormGroupValuesToModel);

            this.id = this.castTo('number', rawValues.id);

            this.name = this.castTo('string', rawValues.name);
            this.persons = rawValues.persons ? rawValues.persons
                    .map(_ => {
                        return Person && Person['prototype']
                            ? ( _ ? Reflect.construct(<any>Person, [_]) : Reflect.construct(<any>Person, []) )
                            : _ ;
                    }) : null;

            // set values in model properties for added formControls
            super.setValuesInAddedPropertiesOfAttachedFormControls(values, useFormGroupValuesToModel);
        }
    }

    /**
     * returns the FormGroup of that model
     */
    public getFormGroup(): FormGroup {
        if (!this._formGroup) {
            this._formGroup = new FormGroup( {
            id: new FormControl(this.id, [] ),
            name: new FormControl(this.name, [] ),
            persons: new FormArray(this.persons ? this.persons
                .map(_ => {
                    return _ && _['getFormGroup'] ? _['getFormGroup']() : new FormControl(_, [])
                }) : []),
            // persons: new FormControl(this.persons, [] ),

            });
        }
        return this._formGroup;
    }

    /**
     * set the FormGroup values to the model values.
    */
    public setFormGroupValues() {
        this.$formGroup.controls['id'].setValue(this.id);
        this.$formGroup.controls['name'].setValue(this.name);
        this.$formGroup.controls['persons'].setValue(this.persons);

        // set formValues in added formControls
        super.setFormGroupValuesInAddedFormControls();
    }

    /**
     * checks, if attribute is required for this model
     */
    public isRequired(attribute: string): boolean {
        switch (attribute) {
            case 'id': return false;
            case 'name': return false;
            case 'persons': return false;
            default: return false;
        }
    }
}


