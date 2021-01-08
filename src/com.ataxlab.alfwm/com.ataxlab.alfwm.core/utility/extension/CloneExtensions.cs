using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.utility.extension
{
    /// <summary>
    /// alternate to ICloneable as per
    /// https://www.c-sharpcorner.com/article/cloning-objects-in-net-framework/
    /// </summary>
    public static class CloneExtensions
    {
        public static T CloneObject<T>(this object source)
        {
            T result = Activator.CreateInstance<T>();
            //// **** made things  
            return result;
        }
    }
}
