using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIP.Collections
{
	public class ObservableRangeCollection<T> : ObservableCollection<T>
	{
		private const string CountString = "Count";
		private const string IndexerName = "Item[]";

		public enum ProcessRangeAction
		{
			Add,
			Replace,
			Remove
		};

		public event Action<object, ProcessRangeAction> BeforeCollectionChanged;

		public ObservableRangeCollection()
			: base()
		{
		}

		public ObservableRangeCollection(IEnumerable<T> collection)
			: base(collection)
		{
		}

		public ObservableRangeCollection(List<T> list)
			: base(list)
		{
		}

		protected virtual void ProcessRange(IEnumerable<T> collection, ProcessRangeAction action)
		{
			if (collection == null) throw new ArgumentNullException("collection");

			var items = collection as IList<T> ?? collection.ToList();
			if (!items.Any()) return;

			ExecuteOnCollectionChanged(collection, action);

			this.CheckReentrancy();

			if (action == ProcessRangeAction.Replace) this.Items.Clear();
			foreach (var item in items)
			{
				if (action == ProcessRangeAction.Remove) this.Items.Remove(item);
				else this.Items.Add(item);
			}

			//if (action == ProcessRangeAction.Remove) { }
			//else
			//	(this.Items as List<T>).AddRange(items);

			this.OnPropertyChanged(new PropertyChangedEventArgs(CountString));
			this.OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private void ExecuteOnCollectionChanged(IEnumerable<T> collection, ProcessRangeAction state)
		{
			if (BeforeCollectionChanged == null)
				return;

			BeforeCollectionChanged(collection, state);
		}

		public void AddRange(IEnumerable<T> collection)
		{
			this.ProcessRange(collection, ProcessRangeAction.Add);
		}

		public void ReplaceRange(IEnumerable<T> collection)
		{
			this.ProcessRange(collection, ProcessRangeAction.Replace);
		}

		public void RemoveRange(IEnumerable<T> collection)
		{
			this.ProcessRange(collection, ProcessRangeAction.Remove);
		}
	}
}
