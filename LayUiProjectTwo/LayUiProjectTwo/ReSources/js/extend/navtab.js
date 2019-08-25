
//设置导航与tab绑定
layui.use('element', function() {
    var $ = layui.jquery
        , element = layui.element; //Tab的切换功能，切换事件监听等，需要依赖element模块
    var active = {
        tabAdd: function(text,url,id){
            //新增一个Tab项
            element.tabAdd('tabtest', {
                title: text
                ,content:'<iframe data-frameid="'+id+'" frameborder="0" name="content" scrolling="yes" width="100%" src="' + url + '"></iframe>'
                ,id: id
            });
            CustomRightClick(id);//绑定右键菜单
            FrameWH();//计算框架高度
        }
        , tabChange: function (id) {
            //切换到指定Tab项
            element.tabChange('tabtest', id);
            $("iframe[data-frameid='"+id+"']").attr("src",$("iframe[data-frameid='"+id+"']").attr("src"))//切换后刷新框架
        }
        , tabDelete: function (id) {
            element.tabDelete("tabtest", id);//删除
        }
        , tabDeleteAll: function (ids) {//删除所有
            $.each(ids, function (i,item) {
                element.tabDelete("tabtest", item);
            })
        }

    };

    //结合左侧菜单展示内容
    $(".layui-side-scroll a").click(function () {
        var dataid = $(this);
        if ($(".layui-tab-title li[lay-id]").length <= 0) {
            active.tabAdd(dataid.context.text,dataid.attr("data-url"), dataid.attr("data-id"));
        } else {
            var isData = false;
            $.each($(".layui-tab-title li[lay-id]"), function () {
                if ($(this).attr("lay-id") == dataid.attr("data-id")) {
                    isData = true;
                }
            })
            if (isData == false) {
                if(dataid.attr("data-id")!=undefined&&dataid.attr("data-id")!=''){
                    active.tabAdd(dataid.context.text,dataid.attr("data-url"), dataid.attr("data-id"));
                }
            }
        }
        active.tabChange(dataid.attr("data-id"));
    });
    //结合右上角菜单展示内容
    $(".layadmin a").click(function () {
        var dataid = $(this);
        if ($(".layui-tab-title li[lay-id]").length <= 0) {
            active.tabAdd(dataid.context.text,dataid.attr("data-url"), dataid.attr("data-id"));
        } else {
            var isData = false;
            $.each($(".layui-tab-title li[lay-id]"), function () {
                if ($(this).attr("lay-id") == dataid.attr("data-id")) {
                    isData = true;
                }
            })
            if (isData == false) {
                if(dataid.attr("data-id")!=undefined&&dataid.attr("data-id")!=''){
                    active.tabAdd(dataid.context.text,dataid.attr("data-url"), dataid.attr("data-id"));
                }
            }
        }
        active.tabChange(dataid.attr("data-id"));
    });

    //绑定右键菜单
    function CustomRightClick(id) {
        //取消右键
        $('.layui-tab-title li').on('contextmenu', function () { return false; })
        $('.layui-tab-title,.layui-tab-title li').click(function () {
            $('.rightmenu').hide();
        });
        //桌面点击右击
        $('.layui-tab-title li').on('contextmenu', function (e) {
            var popupmenu = $(".rightmenu");
            popupmenu.find("li").attr("data-id",id);
            l = ($(document).width() - e.clientX-200) < popupmenu.width() ? (e.clientX-200 - popupmenu.width()) : e.clientX-200;
            t = ($(document).height() - e.clientY-70) < popupmenu.height() ? (e.clientY-70 - popupmenu.height()) : e.clientY-70;
            popupmenu.css({ left: l, top: t }).show();
            //alert("右键菜单")
            return false;
        });
    }
    $(".rightmenu li").click(function () {
        if ($(this).attr("data-type") == "closethis") {
            active.tabDelete($(this).attr("data-id"))
        } else if ($(this).attr("data-type") == "closeall") {
            var tabtitle = $(".layui-tab-title li");
            var ids = new Array();
            $.each(tabtitle, function (i) {
                ids[i] = $(this).attr("lay-id");
            })

            active.tabDeleteAll(ids);
        }

        $('.rightmenu').hide();
    });
    function FrameWH() {
        var h = $(window).height() -41- 10 - 60 -10-44 -10;
        $("iframe").css("height",h+"px");
    }

    $(window).resize(function () {
        FrameWH();
    });


});