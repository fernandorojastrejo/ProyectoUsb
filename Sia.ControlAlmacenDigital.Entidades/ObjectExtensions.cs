using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Entidades
{
    public static class ObjectExtensions
    {
        public static void CopyObject(this object objTo, object objFrom)
        {
            Type tObjFrom = objFrom.GetType();
            Type tObjTo = objTo.GetType();

            var listPropObj1 = tObjFrom.GetProperties().Where(p => p.GetValue(objFrom) != null).ToList();

            foreach (var item in listPropObj1)
            {
                if (tObjTo.GetProperty(item.Name) != null)
                {
                    tObjTo.GetProperty(item.Name).SetValue(objTo, item.GetValue(objFrom));
                }
            }
        }
    }
}