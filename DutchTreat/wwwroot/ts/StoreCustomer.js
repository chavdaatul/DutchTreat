"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var StoreCustomer = /** @class */ (function () {
    //public showme(name: string): boolean {
    //    alert(name);
    //    return true;
    //}
    function StoreCustomer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 0;
    }
    StoreCustomer.prototype.showName = function () {
        alert(this.firstName + " " + this.lastName);
    };
    Object.defineProperty(StoreCustomer.prototype, "name", {
        get: function () {
            return this.ourName;
        },
        set: function (val) { this.ourName = val; },
        enumerable: true,
        configurable: true
    });
    return StoreCustomer;
}());
exports.StoreCustomer = StoreCustomer;
//# sourceMappingURL=StoreCustomer.js.map