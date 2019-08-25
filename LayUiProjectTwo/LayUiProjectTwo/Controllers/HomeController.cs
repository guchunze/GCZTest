using LayUiProjectTwo.Models;
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

namespace LayUiProjectTwo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Description("系统框架页")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Description("系统框架页--导航功能菜单获取")]
        public object LoadMenu(dynamic obj)
        {
            //Dictionary<string, object> result = new Dictionary<string, object>();
            string strSql = "SELECT SYS_PERMISSION_ID as ID,CNAME AS NAME,PID,CURL,IORDER FROM SYS_PERMISSION order by iorder ";
            Database db = DataBaseHelper.CreateDataBase();
            DataTable dt;
            //DataTable reDataTable;

            using (DbConnection conn = db.CreateConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                dt = db.ExecuteDataSet(CommandType.Text, strSql).Tables[0];
            }
            ArrayList list = FormatJsonExtension.DataTableArrayList(dt);
            return this.JsonFormat(list);
        }


      
        [HttpPost]
        [Description("系统框架页--功能菜单获取")]
        public object LoadStoredProcedureMenu(dynamic obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string strSql = "存储过程";
            Database db = DataBaseHelper.CreateDataBase();
            DataTable dt;
            using (DbConnection conn = db.CreateConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                dt = db.ExecuteDataSet(CommandType.StoredProcedure, strSql).Tables[0];
            }
            ArrayList list = FormatJsonExtension.DataTableArrayList(dt);

            result.Add("code", "200");
            result.Add("msg", "ok");
            result.Add("data", list);
            result.Add("count", list.Count);

            return result;
        }

        [HttpPost]
        [Description("系统框架页--功能菜单增加")]
        public object AddMenu(dynamic obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Database dbbase = DataBaseHelper.CreateDataBase();
            using (DbConnection conn = dbbase.CreateConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string strSql = "insert into Menus(MENUID,CODE,NAME,ADDRESS,ICONS,ORDER) values (:MENUID,:CODE,:NAME,:ADDRESS,:ICONS,:ORDER)";
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = dbbase.GetSqlStringCommand(strSql))
                        {
                            dbbase.AddInParameter(cmd, "MENUID", DbType.String, "");
                            dbbase.AddInParameter(cmd, "CODE", DbType.String, "");
                            dbbase.AddInParameter(cmd, "NAME", DbType.DateTime, "");
                            dbbase.AddInParameter(cmd, "ADDRESS", DbType.String, "");
                            dbbase.AddInParameter(cmd, "ICONS", DbType.DateTime, "");
                            dbbase.AddInParameter(cmd, "ORDER", DbType.String, "");
                            dbbase.ExecuteNonQuery(cmd, tran);
                        }
                        tran.Commit();
                        result.Add("result", true);
                        result.Add("msg", "添加数据成功");
                        return null;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Utils.ErrorLog("HomeController-AddMenu！ ||" + e.ToString());
                        result.Add("result", false);
                        result.Add("msg", "添加数据失败");
                        return result;
                    }
                }
            }
        }

        [HttpPost]
        [Description("系统框架页--功能菜单增加")]
        public object AddstoredProcedureMenu(dynamic obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Database dbbase = DataBaseHelper.CreateDataBase();
            using (DbConnection conn = dbbase.CreateConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string strSql = "插入存储过程";
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = dbbase.GetSqlStringCommand(strSql))
                        {
                            dbbase.AddInParameter(cmd, "MENUID", DbType.String, "");
                            dbbase.AddInParameter(cmd, "CODE", DbType.String, "");
                            dbbase.AddInParameter(cmd, "NAME", DbType.DateTime, "");
                            dbbase.AddInParameter(cmd, "ADDRESS", DbType.String, "");
                            dbbase.AddInParameter(cmd, "ICONS", DbType.DateTime, "");
                            dbbase.AddInParameter(cmd, "ORDER", DbType.String, "");
                            dbbase.ExecuteNonQuery(cmd, tran);
                        }
                        tran.Commit();
                        result.Add("result", true);
                        result.Add("msg", "添加数据成功");
                        return null;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Utils.ErrorLog("HomeController-AddMenu！ ||" + e.ToString());
                        result.Add("result", false);
                        result.Add("msg", "添加数据失败");
                        return result;
                    }
                }
            }
        }

        [HttpPost]
        [Description("系统框架页--功能菜单编辑")]
        public object EditMenu(dynamic obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Database dbbase = DataBaseHelper.CreateDataBase();
            using (DbConnection conn = dbbase.CreateConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string strSql = "update Menus set CODE=:CODE,NAME=:NAME,ADDRESS=:ADDRESS,ICONS=:ICONS,ORDER=:ORDER where MENUID=:MENUID";
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = dbbase.GetSqlStringCommand(strSql))
                        {
                            dbbase.AddInParameter(cmd, "MENUID", DbType.String, "");
                            dbbase.AddInParameter(cmd, "CODE", DbType.String, "");
                            dbbase.AddInParameter(cmd, "NAME", DbType.DateTime, "");
                            dbbase.AddInParameter(cmd, "ADDRESS", DbType.String, "");
                            dbbase.AddInParameter(cmd, "ICONS", DbType.DateTime, "");
                            dbbase.AddInParameter(cmd, "ORDER", DbType.String, "");
                            dbbase.ExecuteNonQuery(cmd, tran);
                        }
                        tran.Commit();
                        result.Add("result", true);
                        result.Add("msg", "修改数据成功");
                        return null;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Utils.ErrorLog("HomeController-EditMenu！ ||" + e.ToString());
                        result.Add("result", false);
                        result.Add("msg", "修改数据失败");
                        return result;
                    }
                }
            }
        }



        [HttpPost]
        [Description("系统框架页--功能菜单删除")]
        public object DelMenu(dynamic obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Database dbbase = DataBaseHelper.CreateDataBase();
            using (DbConnection conn = dbbase.CreateConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string strSql = "Delete from  Menus where MENUID=:MENUID";
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = dbbase.GetSqlStringCommand(strSql))
                        {
                            dbbase.AddInParameter(cmd, "MENUID", DbType.String, "");
                            dbbase.ExecuteNonQuery(cmd, tran);
                        }
                        tran.Commit();
                        result.Add("result", true);
                        result.Add("msg", "删除数据成功");
                        return null;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Utils.ErrorLog("HomeController-DelMenu！ ||" + e.ToString());
                        result.Add("result", false);
                        result.Add("msg", "删除数据失败");
                        return result;
                    }
                }
            }
        }


    }
}