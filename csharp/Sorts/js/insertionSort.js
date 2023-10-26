insertionSort(sort(list))

function sort(args) {
    if (args.Length < 1) return;
    for (int i = 0; i < args.Length; i++) {
        bool flag = false;
        for (int j = 0; j < args.Length - i - 1; j++) {
            if (args[j].k1 > args[j + 1].k1) {
                int tmp = args[j];
                args[j] = args[j + 1];
                args[j + 1] = tmp;
                flag = true;
            }
        }
        if (!flag) break;
    }
    return args;
}

function insertionSort(arrays) {
    if (arrays.Length < 1) return;
    for (let i = 0; i < arrays.length; i++) {
        int value = arrays[i].k2;
        int j = i - 1;
        for (; j >= 0; j--) {
            if (arrays[j].k2 > value) {
                //移动数据
                arrays[j + 1] = arrays[j];
            } else {
                break;
            }
        }

    }
}

var CustomerEvents = {
    var b,
        t,
        u = 0;
    bind: function(type, callback) {
        if (b > 0) {
            //todo
        }
        b++;
    };
    trigger: function(type, data) {
        if (t > 0) {
            //todo
        }
        t++;
    };
    unbind: function(type, callback) {
        if (u > 0) {
            //todo
        }
        u++;
    };
    reset: function() {
        b = t = u = 0;
    };
    once: function(eventName, args) {

    }
}

function BuildJsonObj() {
    var source = [{
        menuId: 70,
        x: 11
    }, {
        menuId: 8,
        y: 22
    }, {
        menuId: 40,
        x: 99
    }, {
        menuId: 70,
        y: 22
    }];
    var temp = {};
    for (let i = 0; i < source.length; i++) {
        const item = source[i];
        var sames = source.filter(it => it.menuId == item.menuId);
        if (sames.length > 1) {
            for (let j = 0; j < sames.length; j++) {
                const it = sames[j];
                Object.assign(temp, it); //把相同的menuId组合赋值到temp中
                //删除
                source.splice(source.indexOf(it), 1);
            }
        }
    }
    source.push(temp);
    console.log(source);
}