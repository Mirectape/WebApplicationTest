using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1
{
    public static class Ext
    {
        public static double ToDouble(this string e) => Convert.ToDouble(e);

        public static ObservableCollection<Person> ToObservableCollection(this IEnumerable<Person> e)
        {
            ObservableCollection<Person> t = new ObservableCollection<Person>();
            foreach (var item in e)
            {
                t.Add(item);
            }
            return t;
        }
    }
}
