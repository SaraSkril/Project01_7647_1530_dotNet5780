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

            T target = (T)Activator.CreateInstance(original.GetType());

            foreach (var originalProp in original.GetType().GetProperties())
            {
            
                originalProp.SetValue(target, originalProp.GetValue(original));
            }


            return target;
        }
    }

}

