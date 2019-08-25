using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUiProjectTwo.Controllers
{
    public static class FormatJsonExtension
    {
        /// <summary>
        /// 属性转json
        /// </summary>
        /// <param name="c"></param>
        /// <param name="data"></param>
        /// <param name="exceptMemberName">不显示的属性</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, object data, string[] exceptMemberName)
        {
            FormatJsonResult result = new FormatJsonResult();
            result.Data = data;
            result.ExceptMemberName = exceptMemberName;
            return result;
        }

        public static FormatJsonResult JsonFormat(this Controller c, object data)
        {
            return JsonFormat(c, data, null);
        }
        public static List<DataFilter> DesDataFilter(this Controller c, string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            return JsonConvert.DeserializeObject<List<DataFilter>>(json);
        }

        /// <summary>
        /// 将datatable转换为ArrayList
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ArrayList DataTableArrayList(DataTable data)
        {
            return DataTable2ArrayList(data, true);
        }

        /// <summary>
        /// 将datatable转换为ArrayList
        /// </summary>
        /// <param name="data">目标datatable</param>
        /// <param name="IsShowNull">是否转换值未Null的字段，true表示转换,flase表示不转换</param>
        /// <returns></returns>
        public static ArrayList DataTable2ArrayList(DataTable data, bool IsShowNull)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];

                Hashtable record = new Hashtable();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    object cellValue = row[j];
                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                        if (!IsShowNull)
                        {
                            continue;
                        }
                    }
                    record[data.Columns[j].ColumnName] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }

    }
    public class DataFilter
    {
        public string type { get; set; }
        public string value { get; set; }
        public string field { get; set; }
        public string comparison { get; set; }
    }
}