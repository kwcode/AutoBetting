using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    public class DBParamEntity
    {
        /// <summary>
        /// 字段名 不带 @
        /// </summary>
        public string FieldName { get; set; }
        public object Value { get; set; }

        public DBParamEntity() { }
        public DBParamEntity(string fieldName, object value)
        {
            FieldName = fieldName;
            Value = value;
        }
    }
}
