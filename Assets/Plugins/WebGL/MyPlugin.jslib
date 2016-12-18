//var cbobj, HEAP8, HEAPU8, HEAP16, HEAPU16, HEAP32, HEAPU32, HEAPF32, HEAPF64, Runtime, _malloc, _free, autoAddDeps, mergeInto, LibraryManager;

var MyPlugin = {
    $cbobj: {},

    $receiveDataFromUnity: function(type, heap, ptr, size) {
        var strVals = [];
        debugger;
        heap.subarray(ptr / heap.BYTES_PER_ELEMENT, (ptr / heap.BYTES_PER_ELEMENT) + size).forEach(function(val) {
            strVals.push('' + val);
        });
        console.log(type + 'ArrayTest: [' + strVals.join(',') + ']');
    },

    $sendDataToUnity: function(data, heap, callback) {
        var writeData = typeof data === 'string' ? (new TextEncoder).encode(data + String.fromCharCode(0)) : data;
        var buffer = _malloc(writeData.length * (data.BYTES_PER_ELEMENT || 1));
        debugger;
        heap.set(writeData, buffer / (data.BYTES_PER_ELEMENT || 1));
        Runtime.dynCall('vii', callback, [buffer, writeData.length]);
        _free(buffer);
    },

    initCallback: function(onText, onByteArray, onSByteArray, onShortArray, onUShortArray, onIntArray, onUIntArray, onFloatArray, onDoubleArray) {
        cbobj.onText = onText;
        cbobj.onByteArray = onByteArray;
        cbobj.onSByteArray = onSByteArray;
        cbobj.onShortArray = onShortArray;
        cbobj.onUShortArray = onUShortArray;
        cbobj.onIntArray = onIntArray;
        cbobj.onUIntArray = onUIntArray;
        cbobj.onFloatArray = onFloatArray;
        cbobj.onDoubleArray = onDoubleArray;
    },

    sbyteArrayTest: function(ptr, size) {
        receiveDataFromUnity('sbyte', HEAP8, ptr, size);
    },

    byteArrayTest: function(ptr, size) {
        receiveDataFromUnity('byte', HEAPU8, ptr, size);
    },

    shortArrayTest: function(ptr, size) {
        receiveDataFromUnity('short', HEAP16, ptr, size);
    },

    ushortArrayTest: function(ptr, size) {
        receiveDataFromUnity('ushort', HEAPU16, ptr, size);
    },

    intArrayTest: function(ptr, size) {
        receiveDataFromUnity('int', HEAP32, ptr, size);
    },

    uintArrayTest: function(ptr, size) {
        receiveDataFromUnity('uint', HEAPU32, ptr, size);
    },

    floatArrayTest: function(ptr, size) {
        receiveDataFromUnity('float', HEAPF32, ptr, size);
    },

    doubleArrayTest: function(ptr, size) {
        receiveDataFromUnity('double', HEAPF64, ptr, size);
    },

    testSendDataToUnity: function() {
        sendDataToUnity('hoge_fuga_piyo', HEAPU8, cbobj.onText);
        sendDataToUnity('abc𩸽def', HEAPU8, cbobj.onText);
        sendDataToUnity(new Int8Array([1, 2, 3]), HEAP8, cbobj.onSByteArray);
        sendDataToUnity(new Uint8Array([1, 2, 3]), HEAPU8, cbobj.onByteArray);
        sendDataToUnity(new Int16Array([1, 2, 3]), HEAP16, cbobj.onShortArray);
        sendDataToUnity(new Uint16Array([1, 2, 3]), HEAPU16, cbobj.onUShortArray);
        sendDataToUnity(new Int32Array([1, 2, 3]), HEAP32, cbobj.onIntArray);
        sendDataToUnity(new Uint32Array([1, 2, 3]), HEAPU32, cbobj.onUIntArray);
        sendDataToUnity(new Float32Array([1, 2, 3]), HEAPF32, cbobj.onFloatArray);
        sendDataToUnity(new Float64Array([1, 2, 3]), HEAPF64, cbobj.onDoubleArray);
    }
};

autoAddDeps(MyPlugin, '$cbobj');
autoAddDeps(MyPlugin, '$receiveDataFromUnity');
autoAddDeps(MyPlugin, '$sendDataToUnity');
mergeInto(LibraryManager.library, MyPlugin);