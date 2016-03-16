using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MIP.MVVM
{
	public class MIPEventArgs
	{
		public bool Handled { get; set; }
		public object OldValue { get; set; }
		public object NewValue { get; set; }
	}

	public class ListViewModelExtended<T> : AdwancedViewModelBase where T : AdwancedViewModelBase
	{
		#region Fields

		private object mvSelectedItem;
		private int mvSelectedIndex;
		
		#endregion
		
		#region Properties

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

				MIPEventArgs arg = new MIPEventArgs() { NewValue = value, OldValue = mvSelectedItem };
				
				RaiseOnSelectionChanged(this, arg);
				
				if (arg.Handled)
					return;

				mvSelectedItem = value;

				RaisePropertyChanged(() => SelectedItem);
			}
		}
		
		#endregion

		#region Ctr.

		public ListViewModelExtended():base()
		{
			Items = new ObservableCollection<T>();
		}

		#endregion

		#region Ovverides

		protected override void OnTokenChanged()
		{
			base.OnTokenChanged();
			//SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
		}

		#endregion

		#region  Commented collection changed

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
		
		#endregion

		#region Events

		public event Action<object, MIPEventArgs> SelectionChanged;

		private void RaiseOnSelectionChanged(object sender, MIPEventArgs value)
		{
			if (SelectionChanged == null)
				return;

			SelectionChanged(sender, value);
		}

		#endregion

	}
}
