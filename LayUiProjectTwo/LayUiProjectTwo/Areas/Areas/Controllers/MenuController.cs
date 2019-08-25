using LayUiProjectTwo.Controllers;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace LayUiProjectTwo.Areas.Areas.Controllers
{
    public class MenuController : Controller
    {
        // GET: Areas/Menu
        [Description("菜单页")]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询数据显示
        /// </summary>
        /// <returns></returns>
   
        [Description("菜单页--功能菜单获取")]
        public ActionResult loadlist()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string strSql = "select T.Sys_Permission_Id as ID,T.CNAME,T.CURL,T.PID,IORDER,IISAVAILABLE from sys_permission t";
            var dbbase = DataBaseHelper.CreateDataBase();
            DataTable dt;
            using (DbConnection conn = dbbase.CreateConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                dt = dbbase.ExecuteDataSet(CommandType.Text, strSql).Tables[0];
            }
            ArrayList list = FormatJsonExtension.DataTableArrayList(dt);
            result.Add("code", "0");
            result.Add("msg", "");
            result.Add("count", list.Count);
            result.Add("data", list);
            return this.JsonFormat(result);
        }

        /// <summary>
        /// 查询数据分页显示
        /// </summary>
        /// <returns></returns>
        public ActionResult loadlistpage()
        {
            //页码
            int page = Convert.ToInt32(Request["page"]);
            //页条数
            int rows = Convert.ToInt32(Request["limit"]);
            //where
            string sqlwhere = "1=1";

            string aaa = Request["aaa"];

            //排序
            string sort = "ID desc";
            //表名
            string tableName = "MT_WORKORDER";
            //列
            string columns = "ID,CCODE,CCODENAME,DDATE,CMAKER,DCREATESYSTIME,CSOUCE,CTHINGADDRESS";
            //总条数
            int toalCount = 0;

            Database db = DataBaseHelper.CreateDataBase();
                //DatabaseFactory.CreateDatabase("DEFAULT_CONNECTION_STRING");

            SqlPage sqlpage = new SqlPage();
            DataTable dt = sqlpage.GetDataSet(tableName, columns, sqlwhere, sort, page, rows, out toalCount).Tables[0];

            ArrayList list= FormatJsonExtension.DataTableArrayList(dt);

            Dictionary<String, object> result = new Dictionary<string, object>();
            result.Add("code", "0");
            result.Add("msg", "");
            result.Add("count",toalCount);
            result.Add("data", list);

            return this.JsonFormat(result);
        }

        /// <summary>
        /// 数据添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return null;
        }

        /// <summary>
        /// 数据编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            return null;
        }

        /// <summary>
        /// 数据删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Del()
        {
            return null;
        }

    }
}