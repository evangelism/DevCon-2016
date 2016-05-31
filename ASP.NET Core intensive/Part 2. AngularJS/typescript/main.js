//tsc main.ts --target ES5
var MyCorporation;
(function (MyCorporation) {
    var TestClass = (function () {
        function TestClass() {
        }
        return TestClass;
    }());
    MyCorporation.TestClass = TestClass;
})(MyCorporation || (MyCorporation = {}));
var Direction;
(function (Direction) {
    Direction[Direction["Up"] = 1] = "Up";
    Direction[Direction["Down"] = 2] = "Down";
    Direction[Direction["Left"] = 3] = "Left";
    Direction[Direction["Right"] = 4] = "Right";
})(Direction || (Direction = {}));
var Manager = (function () {
    function Manager(age, _persons, pantsColor) {
        if (pantsColor === void 0) { pantsColor = 'yellow'; }
        this._persons = _persons;
        this.pantsColor = pantsColor;
        this.age = age;
    }
    Manager.prototype.writeEmail = function () {
        this._persons.forEach(function (c) {
            console.log("User: " + c.firstName + " " + c.lastName);
        });
        return this._persons.length;
    };
    Object.defineProperty(Manager.prototype, "grade", {
        get: function () {
            return this.age * 2 / 1.5;
        },
        enumerable: true,
        configurable: true
    });
    return Manager;
}());
var manager = new Manager(31, [
    { firstName: 'Ivan', lastName: 'Ivanov' },
    { firstName: 'Sergey', lastName: 'Pugachev' }
]);
