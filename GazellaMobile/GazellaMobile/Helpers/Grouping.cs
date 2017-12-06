using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.Helpers
{
    public class Grouping <K,T>: ObservableCollection<T>
    {
        public K Key { get; private set; }
        public Grouping(K key, IEnumerable<T> items)
        {
            this.Key = key;
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }
    }
}
