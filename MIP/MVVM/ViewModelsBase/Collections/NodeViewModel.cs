using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MIP.MVVM.ViewModelsBase.Collections
{
	public class NodeViewModel<TItem> : AdwancedViewModelBase where TItem : NodeViewModel<TItem>
	{
		#region Fileds

		private bool mvIsSelected = false;
		private bool mvIsExpanded = false;
		private string mvText = string.Empty;

		#endregion

		#region Ctr.

		public NodeViewModel()
			: base()
		{
			Children = new ObservableCollection<NodeViewModel<TItem>>();
		}

		#endregion

		#region Props

		public ObservableCollection<NodeViewModel<TItem>> Children { get; set; }

		public NodeViewModel<TItem> Parent { get; set; }

		public virtual bool IsSelected
		{
			get { return this.mvIsSelected; }
			set
			{
				if (mvIsSelected == value)
					return;

				this.mvIsSelected = value;
				
				RaisePropertyChanged(() => IsSelected);

			}
		}

		public virtual string Text
		{
			get { return mvText; }
			set
			{
				if (mvText == value)
					return;

				mvText = value;

				RaisePropertyChanged(() => Text);
			}
		}

		public object Tag { get; set; }

		public bool IsExpanded
		{
			get { return mvIsExpanded; }
			set
			{
				if (mvIsExpanded == value)
					return;

				mvIsExpanded = value;

				RaisePropertyChanged(() => IsExpanded);

				RefreshParent();
			}
		}

		#endregion

		#region Methods


		public static void GoDown<TConv>(NodeViewModel<TItem> node,
			Action<TConv> process) where TConv : class
		{
			if (node == null || process == null)
				return;

			foreach (var item in node.Children)
			{
				GoDown<TConv>(item, process);
			}

			TConv target = node as TConv;
			
			if (target == null)
				return;
			
			process(target);
		}

		public static void GoUp<TConv>(NodeViewModel<TItem> node,
			Action<TConv> process) where TConv : class
		{
			if (node == null || process == null)
				return;

			TConv target = node.Parent as TConv;

			if (target != null)
				process(target);

			if (node.Parent !=null)
				GoUp<TConv>(node.Parent, process);
		}

		protected void RefreshParent()
		{
			if (Parent == null)
				return;

			Parent.Refresh();
		}

		/*public event PropertyChangedEventHandler PropertyChanged;
		private void RaisePropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}*/

		public override string ToString()
		{
			return Text;
		}

		#endregion

	}//class NodeViewModel

}
