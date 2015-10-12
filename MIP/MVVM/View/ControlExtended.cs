using MVVM;
using MIP.Behavior;
using MIP.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MIP.MVVM.View
{
	public class ControlExtended : UserControl
	{
		private CWindowExtended modParentWindow;

		protected string Token
		{
			get { return CControlBehavior.GetToken(this); }
		}

		public string ParentToken { get; set; }

		public ControlExtended()
		{
			this.Unloaded += ControlExtended_UnLoaded;
		}

		public void Initiliaze(CWindowExtended wind)
		{
			if (wind == null)
				throw new ArgumentNullException();

			this.modParentWindow = wind;
		}

		void ControlExtended_UnLoaded(object sender, System.Windows.RoutedEventArgs e)
		{
			AdwancedViewModelBase viewModel = DataContext as AdwancedViewModelBase;
			if (viewModel == null)
				return;

			viewModel.Clean();
			viewModel = null;
			DataContext = null;

			this.Unloaded -= ControlExtended_UnLoaded;
		}


		public virtual void Clean()
		{
			
		}
	}
}
