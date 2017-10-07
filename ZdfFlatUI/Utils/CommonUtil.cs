using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZdfFlatUI.Utils
{
    public class CommonUtil
    {
        /// <summary>
        /// 反射获取指定值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object obj, string path)
        {
            //Type type = obj.GetType();
            //System.Reflection.PropertyInfo propertyInfo = type.GetProperty(path);
            //if(propertyInfo == null)
            //{
            //    return null;
            //}
            //return propertyInfo.GetValue(obj, null);

            if (obj == null) return string.Empty;

            bool flag = !string.IsNullOrEmpty(path);
            object result;
            if (flag)
            {
                PropertyInfo property = obj.GetType().GetProperty(path);
                bool flag2 = property != null;
                if (flag2)
                {
                    result = property.GetValue(obj, null);
                    return result;
                }
            }
            result = obj;
            return result;
        }
    }
}
