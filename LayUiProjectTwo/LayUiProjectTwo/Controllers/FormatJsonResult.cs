using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUiProjectTwo.Controllers
{
    public class FormatJsonResult : ActionResult
    {
        public string[] ExceptMemberName { get; set; }
        public Object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";

            StringWriter sw = new StringWriter();
            //日期格式
            string strTime = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

            JsonSerializer serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    Converters = new JsonConverter[] { new IsoDateTimeConverter() { DateTimeFormat = strTime } },
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new NHibernateContractResolver(ExceptMemberName)
                });

            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;

                serializer.Serialize(jsonWriter, Data);
            }
            response.Write(sw.ToString());

            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            ////这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            //timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            ////jsonObject是准备转换的对象
            //string strJSON = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.Indented, timeConverter);
            //response.Write(strJSON);
        }
    }
}