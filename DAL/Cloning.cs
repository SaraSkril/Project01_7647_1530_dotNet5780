using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Reflection;
using System.Linq.Expressions;



namespace DAL
{
    public static class Cloning
    {

        public static T Clone<T>(this T original) where T : new() //generic clonig function 
        {

            T target = new T();

            foreach (var originalProp in original.GetType().GetProperties())
            {
                if (originalProp == typeof(Enum))
                    originalProp.SetValue(target, (Enum)(originalProp.GetValue(original)));
                originalProp.SetValue(target, originalProp.GetValue(original));
            }


            return target;
        }
    }

}

