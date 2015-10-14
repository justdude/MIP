using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIP.MVVM.ViewModelsBase.Collections
{

	public interface IItemsListViewModel //: IAdvancedViewModel
	{
		IEnumerable Items { get; }

		object SelectedItem { get; set; }

		event ItemsListEventHandler SelectedItemChanged;
	}

	public class ListCollectionViewModel<TItemViewModel> : AdwancedViewModelBase,  IEnumerable<TItemViewModel>, IItemsListViewModel //ISelectionSource,
		where TItemViewModel : AdwancedViewModelBase
	{
		#region Fields
		private TItemViewModel mvSelectedItem;
		private ObservableCollection<TItemViewModel> mvSelectedItems;
		#endregion

		#region Events

		public event ItemsListEventHandler SelectedItemChanged;

		#endregion

		#region Properties

		public ObservableCollection<TItemViewModel> Items { get; private set; }

		object IItemsListViewModel.SelectedItem
		{
			[DebuggerStepThrough]
			get { return SelectedItem; }
			set { SelectedItem = (TItemViewModel)value; }
		}

		public virtual TItemViewModel SelectedItem
		{
			[DebuggerStepThrough]
			get { return mvSelectedItem; }
			set
			{
				if (ReferenceEquals(mvSelectedItem, value))
					return;

				mvSelectedItem = value;
				RaisePropertyChanged(() => SelectedItem);

				RaiseSelectedItemChanged();
			}
		}

		public ObservableCollection<TItemViewModel> SelectedItems
		{
			[DebuggerStepThrough]
			get { return mvSelectedItems; }
			set
			{
				if (ReferenceEquals(mvSelectedItems, value))
					return;

				mvSelectedItems = value;
				RaisePropertyChanged(() => SelectedItems);
			}
		}

		//public object Tag { get; set; }

		#endregion

		#region .Ctr

		public ListCollectionViewModel()
			: base()
		{
			Items = new ObservableCollection<TItemViewModel>();
		}

		#endregion

		#region Methods

		protected void RaiseSelectedItemChanged()
		{
			OnSelectedItemChanged();

			if (SelectedItemChanged == null)
				return;

			SelectedItemChanged(SelectedItem);
		}


		protected virtual void OnSelectedItemChanged()
		{
			RefreshCommands();
		}
		#endregion

		IEnumerable IItemsListViewModel.Items
		{
			get { throw new NotImplementedException(); }
		}

		event ItemsListEventHandler IItemsListViewModel.SelectedItemChanged
		{
			add { throw new NotImplementedException(); }
			remove { throw new NotImplementedException(); }
		}

		public IEnumerator<TItemViewModel> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Items.GetEnumerator();
		}
	}

	public delegate void ItemsListEventHandler(object selectedItem);

}
