using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class SqlPage
    {
        string strReturnSQL = "";
        string strOrderBy = "";
        int iStartCount = 0;
        string strInnerSql = "";

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="selectTarget">查询对象</param>
        /// <param name="columns">查询的列</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="page">当前页</param>
        /// <param name="rowsPerPage">页面显示记录数 小于等于0 不分页</param>
        /// <param name="tolalCount">返回记录集合</param>
        /// <returns></returns>
        public DataSet GetDataSet(string selectTarget, string columns, string strWhere, string orderBy, int page, int rowsPerPage, out int tolalCount)
        {
            try
            {
                //查询条件
                //if (strWhere != "")

                if (strWhere != "" && strWhere != null && strWhere != "null")
                {
                    strWhere = " where " + strWhere;
                }
                else
                {
                    strWhere = "";
                }
                string strSQL = GetListSQL(selectTarget, columns, rowsPerPage, page, orderBy, strWhere);

                //组织获取记录数SQL
                string strTotalSQL = "select count(1) from " + selectTarget + strWhere;

                Database dbbase = DataBaseHelper.CreateDataBase();

                using (DbCommand cmd = dbbase.GetSqlStringCommand(strTotalSQL))
                {
                    tolalCount = Convert.ToInt32(dbbase.ExecuteScalar(cmd));
                }
                DataSet ds = null;
                using (DbCommand cmd = dbbase.GetSqlStringCommand(strSQL))
                {
                    ds = dbbase.ExecuteDataSet(cmd);
                }
                return ds;
            }
            catch (Exception e)
            {
                Utils.ErrorLog("SqlPage-GetDataSet！ ||" + e.ToString());
                tolalCount = 0;
                DataSet ds = null;
                return ds;
            }

        }

        /// <summary>
        /// 分页语句
        /// </summary>
        /// <param name="strSelectTarget"></param>
        /// <param name="columns"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="page"></param>
        /// <param name="orderBy"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public string GetListSQL(string strSelectTarget, string columns, int rowsPerPage, int page, string orderBy, string strWhere)
        {
            try
            {

                if (orderBy.Trim() != "")
                    strOrderBy = " Order by " + orderBy + "";

                if (rowsPerPage <= 0)
                {
                    strReturnSQL = "SELECT " + columns + " FROM " + strSelectTarget + " "
                        + strWhere + " " + strOrderBy;
                }
                else
                {
                    iStartCount = (page - 1) * rowsPerPage + 1;

                    strInnerSql = "SELECT " + columns + " FROM " + strSelectTarget + " "
                        + strWhere + " " + strOrderBy;
                    strReturnSQL = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM (" + strInnerSql
                    + ") A WHERE ROWNUM<= " + page * rowsPerPage + ") WHERE RN >= " + iStartCount;
                }
                return strReturnSQL;

            }
            catch (Exception e)
            {
                Utils.ErrorLog("SqlPage-GetListSQL！ ||" + e.ToString());
                return null;
            }
        }
    }
}
