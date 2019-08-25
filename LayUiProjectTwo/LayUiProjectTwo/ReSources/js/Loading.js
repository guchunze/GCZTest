//var Loading = {
//    Directory: function () {
//        var obj = window.location;
//        var contextPath = obj.pathname.split("/")[1];
//        var basePath = obj.protocol + "//" + obj.host + "/" + contextPath + "/";
//        return basePath;
//    }
//}

var Loading = function () {

};

Loading.prototype.Directory = function () {
    var obj = window.location;
    var contextPath = obj.pathname.split("/")[1];
    var basePath = obj.protocol + "//" + obj.host + "/" + contextPath + "/";
    return basePath;
};

Loading.prototype.GetAjaxQueryString = function (url, getData, fnCallback, isAsync) {
    var load = new Loading();
    var directory = load.Directory();

    if (typeof (isAsync) == "undefined") {
        isAsync = false;
    }
    $.ajax({
        url: directory+url,
        type: "get",
        data: getData,
        contentType: "application/json",
        cache: false,
        async: isAsync,
        success: function (sRtn) {
            fnCallback(sRtn);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if ($.messager) {
                Global.closeWaiting();
                $.messager.alert('提示', jqXHR.statusText, "error");
            }
            else {
                alert(jqXHR.statusText);
            }
            return;
        }
    })
};

Loading.prototype.PostAjaxQueryString = function (url, postData, fnCallback, isAsync) {
    if (typeof (isAsync) == "undefined") {
        isAsync = false;
    }
    var load = new Loading();
    var directory = load.Directory();
    $.ajax({
        url:directory+url,
        type: "post",
        data: JSON.stringify(postData),
        contentType: "application/json",
        cache: false,
        async: isAsync,
        success: function (sRtn) {
            fnCallback(sRtn);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $.messager.alert('提示', jqXHR.statusText, "error");
            return;
        }
    })
};