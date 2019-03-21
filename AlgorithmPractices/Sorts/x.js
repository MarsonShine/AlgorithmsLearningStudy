function isString(str) {
    return Object.prototype.toString.call(str) === "[object String]";
}



var obj = (function () {
    return {
        getName: function () { },
        sayHello: function (name) { alert(name) }
    }
})();

var str = "aaaassewqessxxxx";
var arrays = str.split("");
var json = {};
for (let i = 0; i < arrays.length; i++) {
    var c = arrays[i];
    if (!json[c]) {//不存在
        json[c] = 1;
    } else {
        json[c]++;
    }
}
var max;
for (var key in json) {
    if (json[key] > max) {
        max = key;
    }
}
//输出最多的字符就是 key，次数 就是 json[i]
