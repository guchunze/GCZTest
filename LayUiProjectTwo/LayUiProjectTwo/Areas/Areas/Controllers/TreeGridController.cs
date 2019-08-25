using LayUiProjectTwo.Areas.Areas.Models;
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
using System.Web.UI.WebControls;
using Utility;

namespace LayUiProjectTwo.Areas.Areas.Controllers
{
    public class TreeGridController : Controller
    {
        [Description("菜单页")]
        // GET: Areas/TreeGrid
        public ActionResult Index()
        {
            return View();
        }

        [Description("测试TreeGrid--功能菜单获取")]
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
            result.Add("msg", "ok");
            result.Add("count", list.Count);
            result.Add("data", list);
            return this.JsonFormat(result);
        }


        [Description("测试TreeGrid--功能菜单获取")]
        public object LoadMenu(dynamic obj)
        {
            //Dictionary<string, object> result = new Dictionary<string, object>();
            string strSql = "SELECT SYS_PERMISSION_ID as ID,CNAME AS NAME,PID,CURL,IORDER FROM SYS_PERMISSION order by iorder ";
            var db = DataBaseHelper.CreateDataBase();
            DataTable dt;

            using (DbConnection conn = db.CreateConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                dt = db.ExecuteDataSet(CommandType.Text, strSql).Tables[0];
            }
            var result = new List<MenuModel>();
            var memo = new MenuModel();
            DiGuiDataTable(dt, memo, result, -1);
      
            return this.JsonFormat(result);
        }

         public void DiGuiDataTable(DataTable FromDataTable,MenuModel memo, IList<MenuModel> result, object pid)
        {
            if (FromDataTable.Select("PID='" + pid + "'").Length > 0)
            {
                foreach (DataRow item in FromDataTable.Select("PID='" + pid + "'"))
                {
                    MenuModel mmd = new MenuModel
                    {
                        ID = item["ID"].ToString(),
                        NAME = item["NAME"].ToString(),
                        CURL = item["CURL"].ToString(),
                        PID = item["PID"].ToString(),
                        Childlist = new List<MenuModel>()
                    };
                    if (item["PID"].ToString() == "-1")
                    {
                        result.Add(mmd);
                    }
                    else
                    {
                        memo.Childlist.Add(mmd);
                    }
                    DiGuiDataTable(FromDataTable, mmd, result, item["ID"]);

                }
            }
        }


        [Description("测试TreeGrid--功能菜单添加")]
        public ActionResult AddSys_Permission()
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
            result.Add("msg", "ok");
            result.Add("count", list.Count);
            result.Add("data", list);
            return this.JsonFormat(result);
        }

    }
}