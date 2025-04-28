using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labordiagnostik
{
    internal class ObservableList<T> : ObservableCollection<T>
    {
        public event EventHandler ListChanged;
        public readonly object _lock = new object();

        protected virtual void OnListChanged()
        {
            ListChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            lock (_lock)
            {
                foreach (var item in collection)
                {
                    this.Add(item);
                }

                this.OnListChanged();
            }
        }

        public virtual void Add(T item)
        {
            base.Add(item);

            OnListChanged();
        }
    }
}
