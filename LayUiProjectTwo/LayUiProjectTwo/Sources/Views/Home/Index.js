var homes = function () {

};

homes.prototype.Show = function () {
    alert("Show Message");
};
homes.prototype.GetQueryString = function (name) {

    var url = "Home/LoadMenu";
    var getData={};

    var load = new Loading();
    load.PostAjaxQueryString(url, getData, function (Data) {
        var ho = new homes();
        var showlist = $("<div></div>");
        ho.GetData(0, Data, showlist);
        $("#menus").append(showlist);
    });
};

homes.prototype.GetData = function (id, arry, parent) {
    var hom = new homes();
    var childArry = hom.GetParentArry(id, arry);
    if (childArry.length > 0) {
        for (var i in childArry) {
            if (childArry[i].PID == 0) {
                var li = $('<li class="layui-nav-item"></li>');
                var la = $('<a href="javascript:;">' + childArry[i].NAME + '</a>');
                var dl = $('<dl class="layui-nav-child"></dl>');
                li.append(la);
                li.append(dl);
                hom.GetData(childArry[i].ID, arry, dl);
                parent.append(li);
            } else {
                var dd = $('<dd></dd>');
                var linka = $('<a href="javascript:;" data-url="' + childArry[i].CURL + '" data-id="' + childArry[i].ID + '">' + childArry[i].NAME + '</a>')
                dd.append(linka);
                parent.append(dd);
            }
        }
    }
};
homes.prototype.GetParentArry = function (ID, arry) {
    var newArry = new Array();
    for (var i in arry) {
        if (arry[i].PID == ID)
            newArry.push(arry[i]);
    }
    return newArry;

};

//自动加载
$(function ($) {
    var ho = new homes();
    ho.GetQueryString();
    
});