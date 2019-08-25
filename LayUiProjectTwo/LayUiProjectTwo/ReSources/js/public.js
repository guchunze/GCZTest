

/**
 * 工具方法集合
 * $.public
 * stringToJson 字符串转json
 * jsonToString json转字符串
 * GetQueryString 获取页面传参
 * utcToTimeStrUTC 时间转本地时间 yyyy-mm-dd hh:mm:ss
 * getNowFormatDate 获取当前日期
 * CompareDate 日期比较
 * base64encode Base64加密
 * base64decode Base64解密
 * @Author liuyanxin @gvSoft
 * @Date 2017/6/15 10:49
 */

var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
var base64DecodeChars = new Array(
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
    52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
    -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
    -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);

var base64encode = function(str) {
    var out, i, len;
    var c1, c2, c3;

    len = str.length;
    i = 0;
    out = "";
    while (i < len) {
        c1 = str.charCodeAt(i++) & 0xff;
        if (i == len) {
            out += base64EncodeChars.charAt(c1 >> 2);
            out += base64EncodeChars.charAt((c1 & 0x3) << 4);
            out += "==";
            break;
        }
        c2 = str.charCodeAt(i++);
        if (i == len) {
            out += base64EncodeChars.charAt(c1 >> 2);
            out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
            out += base64EncodeChars.charAt((c2 & 0xF) << 2);
            out += "=";
            break;
        }
        c3 = str.charCodeAt(i++);
        out += base64EncodeChars.charAt(c1 >> 2);
        out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
        out += base64EncodeChars.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
        out += base64EncodeChars.charAt(c3 & 0x3F);
    }
    return out;
}

var base64decode = function (str) {
    var c1, c2, c3, c4;
    var i, len, out;

    len = str.length;
    i = 0;
    out = "";
    while (i < len) {
        /* c1 */
        do {
            c1 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
        } while (i < len && c1 == -1);
        if (c1 == -1)
            break;

        /* c2 */
        do {
            c2 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
        } while (i < len && c2 == -1);
        if (c2 == -1)
            break;

        out += String.fromCharCode((c1 << 2) | ((c2 & 0x30) >> 4));

        /* c3 */
        do {
            c3 = str.charCodeAt(i++) & 0xff;
            if (c3 == 61)
                return out;
            c3 = base64DecodeChars[c3];
        } while (i < len && c3 == -1);
        if (c3 == -1)
            break;

        out += String.fromCharCode(((c2 & 0XF) << 4) | ((c3 & 0x3C) >> 2));

        /* c4 */
        do {
            c4 = str.charCodeAt(i++) & 0xff;
            if (c4 == 61)
                return out;
            c4 = base64DecodeChars[c4];
        } while (i < len && c4 == -1);
        if (c4 == -1)
            break;
        out += String.fromCharCode(((c3 & 0x03) << 6) | c4);
    }
    return out;
}

var utf16to8 = function (str) {
    var out, i, len, c;

    out = "";
    len = str.length;
    for (i = 0; i < len; i++) {
        c = str.charCodeAt(i);
        if ((c >= 0x0001) && (c <= 0x007F)) {
            out += str.charAt(i);
        } else if (c > 0x07FF) {
            out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
            out += String.fromCharCode(0x80 | ((c >> 6) & 0x3F));
            out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
        } else {
            out += String.fromCharCode(0xC0 | ((c >> 6) & 0x1F));
            out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
        }
    }
    return out;
}

var utf8to16 = function (str) {
    var out, i, len, c;
    var char2, char3;

    out = "";
    len = str.length;
    i = 0;
    while (i < len) {
        c = str.charCodeAt(i++);
        switch (c >> 4) {
            case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                // 0xxxxxxx
                out += str.charAt(i - 1);
                break;
            case 12: case 13:
                // 110x xxxx   10xx xxxx
                char2 = str.charCodeAt(i++);
                out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
                break;
            case 14:
                // 1110 xxxx  10xx xxxx  10xx xxxx
                char2 = str.charCodeAt(i++);
                char3 = str.charCodeAt(i++);
                out += String.fromCharCode(((c & 0x0F) << 12) |
                    ((char2 & 0x3F) << 6) |
                    ((char3 & 0x3F) << 0));
                break;
        }
    }
    return out;
}

var public = {
    /**
     * 字符串转json
     * @param str
     * @returns {Object}
     */
    stringToJson: function (str){
        return eval("(" + str + ")");
    },
    /**
     * json转字符串
     * @param jsonObj
     */
    jsonToString: function (jsonObj){
        return JSON.stringify(jsonObj);
    },
    /**
     * 获取页面传参
     * @param name
     * @returns {null}
     * @constructor
     */
    GetQueryString : function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null){
            return unescape(r[2]);
        }
        return null;
    },
    /**
     * UTC时间转本地时间 yyyy-mm-dd hh:mm:ss
     * @param nS (秒单位)
     * @returns {string}
     */
    utcToTimeStr : function (nS) {
        if(nS == 0){
            return "";
        }else{
            return new Date(parseInt(nS) * 1000).toLocaleString().replace(/年|月/g, "-").replace(/日/g, " ");
        }
    },

    /**
     * 获取当前日期 yyyy-mm-dd
     * @returns {string}
     */
    getNowFormatDate : function () {
        var date = new Date();
        var seperator = "-";
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = year + seperator + month + seperator + strDate
        return currentdate;
    },
    /**
     * 获取当前日期 yyyy-mm-dd 00:00:00
     * @returns {string}
     */
    getNowFormatDate0: function () {
        var date = new Date();
        var seperator = "-";
        var seperator1 = ":";
        var ling="00";
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = year + seperator + month + seperator + strDate+" "+ling+seperator1+ling+seperator1+ling;
        return currentdate;
    },
    /**
     * 获取当前日期 yyyy
     * @returns {string}
     */
    getNowFormatDateNian : function () {
        var date = new Date();
        var seperator = "-";
        var seperator1 = ":";
        var ling="00";
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = year;
        return currentdate;
    },
    /**
     * 获取当前时间 yyyy-mm-dd HH:MM:SS.sss
     * @returns {string}
     */
    getNowFormatDate1 : function () {
        var date = new Date();
        var seperator1 = "-";
        var seperator2 = ":";
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds() + "." + date.getMilliseconds();
        return currentdate;
    },
    /**
     * 比较开始时间和结束时间
     * @param d1 开始时间 (yyyy-mm-dd)
     * @param d2 结束时间
     * @returns {boolean}
     * @constructor
     */
    CompareDate : function (d1,d2){
        return ((new Date(d1.replace(/-/g,"\/"))) < (new Date(d2.replace(/-/g,"\/"))));
    },
    /**
     * base64加密
     * @param str
     * @returns {string}
     */
    base64encode : function (str){
        if(str != "" & str != undefined){
            return base64encode(utf16to8(str));
        } else {
            return "";
        }
    },
    /**
     * base64解密
     * @param str
     * @returns {string}
     */
    base64decode : function (str){
        if(str != "" && str != undefined){
            return utf8to16(base64decode(str));
        } else {
            return "";
        }
    },
    /**
     * 是否去除所有空格
     * @param str
     * @param is_global 如果为g或者G去除所有的
     * @returns
     */
    Trim : function (str,is_global) {
        var result;
        result = str.replace(/(^\s+)|(\s+$)/g,"");
        if(is_global.toLowerCase()=="g") {
            result = result.replace(/\s/g,"");
        }
        return result;
    },
    datePro: function(str) {
        if(str==undefined){
            return str;
        }
        var datatime= new Date(Date.parse(str.replace(/-/g,   "/"))); //转换成Data();
        var strnyr=datatime.getFullYear()+"-"+(datatime.getMonth()+1)+"-"+datatime.getDate();
        return strnyr;
    },
    datePro2: function(str) {
        if(str==undefined){
            return str;
        }
        var datatime= new Date(Date.parse(str.replace(/-/g,   "/"))); //转换成Data();
        var strnyr=datatime.getFullYear()+"-"+(datatime.getMonth()+1)+"-"+datatime.getDate()+" "+datatime.getHours()+":"+datatime.getMinutes();
        return strnyr;
    },
    toDecimal2: function toDecimal2(x) {
        var f = parseFloat(x);
        if (isNaN(f)) {
            return false;
        }
        var f = Math.round(x*100)/100;
        var s = f.toString();
        var rs = s.indexOf('.');
        if (rs < 0) {
            rs = s.length;
            s += '.';
        }
        while (s.length <= rs + 2) {
            s += '0';
        }
        return s;
    }
}
var websessionStorage = {
    /**
     * 存储
     */
    setItem : function (key,info){
        sessionStorage.setItem(key, JSON.stringify(info));
    },
    /**
     * 获取
     */
    getItem : function (key){
        var data = JSON.parse(sessionStorage.getItem(key));
        return data;
    },
    /**
     * 删除某个数据
     */
    removeItem: function (key){
        sessionStorage.removeItem(key);
    },
    /**
     * 获取
     */
    clear : function (){
        sessionStorage.clear();
    },

}

