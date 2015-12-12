using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIP.MVVM
{
	public class ListViewModelExtended<T> : AdwancedViewModelBase where T : AdwancedViewModelBase
	{
		private object mvSelectedItem;
		private int mvSelectedIndex;
		public ObservableCollection<T> Items { get; set; }

		public int SelectedIndex
		{
			get { return mvSelectedIndex; }
			set
			{
				if (mvSelectedIndex == value)
					return;

				mvSelectedIndex = value;

				RaisePropertyChanged(() => SelectedIndex);
			}
		}

		public object SelectedItem
		{
			get { return mvSelectedItem; }
			set
			{
				if (mvSelectedItem == value)
					return;

				mvSelectedItem = value;

				RaisePropertyChanged(() => SelectedItem);
			}
		} 


		public ListViewModelExtended():base()
		{
			Items = new ObservableCollection<T>();
		}


		protected override void OnTokenChanged()
		{
			base.OnTokenChanged();
			//SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
		}

		//void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		//{
		//	switch (e.Action)
		//	{
		//		case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		//			break;
		//		case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
		//			break;
		//		case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
		//			break;
		//		case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
		//			break;
		//		case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
		//			break;
		//		default:
		//			break;
		//	}
		//}

	}
}
