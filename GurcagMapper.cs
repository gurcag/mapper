using System;
using System.Collections.Generic;
using System.Reflection;

namespace GurcagExtensions
{
    public static class GurcagMapper
    {
        public static K MapItem<T, K>(T from)
            where K : new()
        {
            PropertyInfo toItemsProp;

            K to = new K();

            foreach (PropertyInfo fromProp in from.GetType().GetProperties())
            {
                try
                {
                    toItemsProp = to.GetType().GetProperty(fromProp.Name);
                }
                catch (Exception ex)
                {
                    toItemsProp = null;
                    continue;
                }
                if (toItemsProp != null) // if K has corresponding prop
                {
                    var newValue = fromProp.GetValue(from);
                    if (toItemsProp.PropertyType == fromProp.PropertyType)
                    {
                        toItemsProp.SetValue(to, newValue, null);
                    }
                    else
                    {
                        //Convert.ChangeType(newValue, toItemsProp.GetType());
                        try
                        {
                            toItemsProp.SetValue(to, newValue.ToString(), null);
                        }
                        catch (Exception ex) { throw ex; }
                    }
                }
            }

            return to;
        }

        public static K MapItem<T, K>(T from, K to)
        {
            PropertyInfo toItemsProp;

            foreach (PropertyInfo fromProp in from.GetType().GetProperties())
            {
                try
                {
                    toItemsProp = to.GetType().GetProperty(fromProp.Name);
                }
                catch (Exception ex)
                {
                    toItemsProp = null;
                    continue;
                }
                if (toItemsProp != null) // if K has corresponding prop
                {
                    var newValue = fromProp.GetValue(from);
                    if (toItemsProp.PropertyType == fromProp.PropertyType)
                    {
                        toItemsProp.SetValue(to, newValue, null);
                    }
                    else
                    {
                        //Convert.ChangeType(newValue, toItemsProp.GetType());
                        try
                        {
                            toItemsProp.SetValue(to, newValue.ToString(), null);
                        }
                        catch (Exception ex) { throw ex; }
                    }
                }
            }

            return to;
        }

        public static List<K> MapList<T, K>(List<T> from, List<K> to)
            where K : new()
        {
            if (!from.ListNullOrEmpty())
            {
                K toItem;
                foreach (T fromItem in from)
                {
                    toItem = new K();
                    toItem = MapItem(fromItem, toItem);
                    to.Add(toItem);
                }
            }

            return to;
        }

        public static List<K> MapList<T, K>(List<T> from)
            where K : new()
        {
            List<K> to = new List<K>();

            if (!from.ListNullOrEmpty())
            {
                K toItem;
                foreach (T fromItem in from)
                {
                    toItem = new K();
                    toItem = MapItem(fromItem, toItem);
                    to.Add(toItem);
                }
            }

            return to;
        }
    }
}
