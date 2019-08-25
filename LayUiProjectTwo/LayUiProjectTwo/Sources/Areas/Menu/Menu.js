var oMenu = {

    DataLoad: function () {

        layui.use('table', function () {
            var table = layui.table;
            table.render({
                elem: '#TabShowData',
                url: 'loadlistpage',
                id: "ID",
                cellMinWidth: 80, //全局定义常规单元格的最小宽度，layui 2.2.1 新增
                page: true,//开启分页
                where: { aaa: "aaa" },
                toolbar: '#toolbarDemo',
                defaultToolbar: [],
                limit: 5,
                limits: [5, 10, 15, 20],
                cols: [[
                  { type: 'checkbox', fixed: 'left' },
                  { field: 'ID', width: 120, title: '自动编号' },
                  { field: 'CCODE', width: 120, title: '工单编号' },
                  { field: 'CCODENAME', width: 180, title: '工程名称' },
                  { field: 'DDATE', width: 155, title: '日期' },
                  { field: 'CMAKER', title: '制单人', width: 90, minWidth: 100 }, //minWidth：局部定义当前单元格的最小宽度，layui 2.2.1 新增
                  { field: 'DCREATESYSTIME', title: '制单时间' },
                  { field: 'CSOUCE', title: '数据来源' },
                  { field: 'CTHINGADDRESS', title: '事发地址' },
                  { fixed: 'right', title: '操作', toolbar: '#barDemo', width: 150 }
                ]]
            });


            table.render({
                elem: '#ShowAllData',
                url: 'loadlist',
                id: "ID",
                cellMinWidth: 80, //全局定义常规单元格的最小宽度，layui 2.2.1 新增
                page: false,//开启分页
                where: { aaa: "aaa" },
                toolbar: '#toolbarDemo2',
                defaultToolbar: [],
                cols: [[
                  { type: 'checkbox', fixed: 'left' },
                  { field: 'ID', title: '菜单标识', width: 100 },
                  { field: 'CNAME', title: '菜单名称', width: 300 },
                  { field: 'CURL', title: 'URL', width: 300 },
                  { field: 'PID', title: '分节点标识', width: 100 },
                  { field: 'IORDER', title: '排序', width: 100 },
                  {
                      field: 'IISAVAILABLE', title: '是否显示', width: 100, templet: function (item) {
                          if (item.IISAVAILABLE == 1) {

                              return "是";
                          } else {
                              return "否";
                          }
                      }
                  },
                  { fixed: 'right', title: '操作', toolbar: '#barDemo', width: 150 }
                ]]
            });

            //监听行单击事件（单击事件为：row）
            table.on('rowDouble(ShowAllData)', function (obj) {
                var data = obj.data;

                layer.alert(JSON.stringify(data), {
                    title: '当前行数据：'
                });

                //标注选中样式
                obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            });

            //头工具栏事件
            table.on('toolbar(TabShowData)', function (obj) {
                var checkStatus = table.checkStatus(obj.config.id);
                switch (obj.event) {
                    case 'Add':
                        layer.open({
                            type: 2,
                            area: ['520px', '480px'],
                            shadeClose: true, //点击遮罩关闭
                            maxmin: true, //允许全屏最小化
                            content: '../../ReSources/MainTain/MTOrderFormulateAdd.html',
                            //cancel: function () {
                            //    oMenu.DataLoad();
                            //},
                            end: function () {
                                oMenu.DataLoad();
                            }
                        });
                        break;
                    case 'getCheckData':
                        var data = checkStatus.data;
                        layer.alert(JSON.stringify(data));
                        break;
                    case 'getCheckLength':
                        var data = checkStatus.data;
                        layer.msg('选中了：' + data.length + ' 个');
                        break;
                    case 'isAll':
                        layer.msg(checkStatus.isAll ? '全选' : '未全选');
                        break;
                };
            });
            //监听行工具事件
            table.on('tool(TabShowData)', function (obj) {
                var data = obj.data;
                //console.log(obj)
                if (obj.event === 'del') {
                    layer.confirm('真的删除行么', function (index) {
                        obj.del();
                        layer.close(index);
                    });
                } else if (obj.event === 'edit') {
                    console.info(data)
                    layer.open({
                        type: 2,
                        area: ['520px', '480px'],
                        shadeClose: true, //点击遮罩关闭
                        maxmin: true, //允许全屏最小化
                        content: '../../ReSources/MainTain/MTOrderFormulateAdd.html',
                        //cancel: function () {
                        //    oMenu.DataLoad();
                        //},
                        end: function () {
                            oMenu.DataLoad();
                        }
                    });
                }
            });

            layui.use('form', function () {
                var form = layui.form;
                //监听提交
                form.on('submit(formDemo)', function (data) {
                    layer.msg(JSON.stringify(data.field));
                    return false;
                });
            });

        });
    }
}

$(function ($) {
    oMenu.DataLoad();
});