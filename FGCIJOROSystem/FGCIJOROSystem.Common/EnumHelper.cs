using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FGCIJOROSystem.Common
{
    public class EnumHelper<T>
    {
        public static List<GenericClass> GetTypeEnum()
        {
            List<GenericClass> gcs = new List<GenericClass>();

            int[] bytID = (int[])Enum.GetValues(typeof(T));
            string[] strDesc = Enum.GetNames(typeof(T));
            

            for (int xx = 0; xx <= bytID.Length - 1; xx++)
            {
                GenericClass gc = new GenericClass();

                gc.Id = bytID[xx];
                gc.Name = strDesc[xx].Replace("_", " ");
                gcs.Add(gc);
            }

            return gcs;
        }
        public static List<GenericClass> GetTypeEnum2()
        {
            List<GenericClass> gcs = new List<GenericClass>();

            FieldInfo fi = default(FieldInfo);
            DescriptionAttribute da = default(DescriptionAttribute);
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                fi = typeof(T).GetField((enumValue.ToString()));
                da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (da != null)
                {
                    gcs.Add(new GenericClass() { Id = Convert.ToInt32(enumValue), Name = da.Description });
                }
            }

            return gcs;
        }

        public static Dictionary<string, int> GetEnumDesAttrib()
        {
            Dictionary<string, int> RetList = new Dictionary<string, int>();



            FieldInfo fi = default(FieldInfo);
            DescriptionAttribute da = default(DescriptionAttribute);
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                fi = typeof(T).GetField((enumValue.ToString()));
                da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (da != null)
                {
                    RetList.Add(da.Description, Convert.ToInt32(enumValue));
                }
            }

            //ComboBoxName.DisplayMember = "Key";
            //ComboBoxName.ValueMember = "Value";

            return RetList;
        }


    }
    public class GenericClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GenericClass<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

}
