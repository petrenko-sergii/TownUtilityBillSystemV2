"use strict";
exports.__esModule = true;
var $ = require("jquery");
var Message = /** @class */ (function () {
    function Message() {
    }
    Message.prototype.show = function () {
        $("#ShowBtn").html('Demo');
        alert("Hello world! - 7");
    };
    return Message;
}());
exports["default"] = Message;
//# sourceMappingURL=Message.js.map