using MVVM;
using MIP.Behavior;
using MIP.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace MIP.MVVM.View
{
	public class ControlExtended : UserControl
	{
		public CWindowExtended ParentWindow;
		public List<ControlExtended> ControlChild { get; set; } 

		public string Token
		{
			get { return CControlBehavior.GetToken(this); }
		}

		public string ParentToken { get; set; }

		//public string TokenBinding
		//{
		//	get { return Token; }
		//	set { throw new NotSupportedException(); }
		//}

		//public static readonly DependencyProperty TokenBindingProperty =
		//	DependencyProperty.Register("TokenBinding", typeof(string), typeof(ControlExtended), new PropertyMetadata());

		public ControlExtended():base()
		{
			ControlChild = new List<ControlExtended>();
			//Binding binding = new Binding();
			//binding.Path = new PropertyPath("Token");

			//SetBinding(TokenBindingProperty, binding);
		}

		private void onchaned(object sender, EventArgs e)
		{

		}

		private void Initiliaze(CWindowExtended wind)
		{
			if (wind == null)
				throw new ArgumentNullException();

			this.ParentWindow = wind;
		}

		void ControlExtended_UnLoaded(object sender, System.Windows.RoutedEventArgs e)
		{
			CleanPrivate();
		}

		private void CleanPrivate()
		{
			ControlChild.Clear();
			AdwancedViewModelBase viewModel = DataContext as AdwancedViewModelBase;
			if (viewModel == null)
				return;

			viewModel.Clean();
			viewModel = null;

			DataContext = null;

			this.Unloaded -= ControlExtended_UnLoaded;
		}

		public virtual void BindingDataContext()
		{
			foreach (var item in ControlChild)
			{
				item.BindingDataContext();

				AdwancedViewModelBase viewModel = item.DataContext as AdwancedViewModelBase;

				if (viewModel == null)
					continue;

				viewModel.Token = Token;
			}
		}

		public virtual void Clean()
		{
			CleanPrivate();
		}
	}
}
