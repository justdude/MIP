using GalaSoft.MvvmLight.Messaging;
using MIP.MVVM.View;
using MVVM;
using MIP.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MIP.MVVM.View
{
	public class CWindowExtended : Window
	{
		protected string Token
		{
			get { return CControlBehavior.GetToken(this); }
		}

		private Action<object> OnClosedDel { get; set; }
		private List<ControlExtended> modControls { get; set; }

		public ControlExtended this[int i]
		{
			get
			{
				return modControls[i];
			}
		}

		public CWindowExtended()
		{
			modControls = new List<ControlExtended>();
			Closing += ControlExtended_UnLoaded;
			RegisterMessages();
		}

		private void RegisterMessages()
		{
			Messenger.Default.Register<Messages.CloseWindowMessage>(this, Token, OnClose);
		}

		private void OnClose(Messages.CloseWindowMessage obj)
		{
			OnClosedDel = obj.OnClosed;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			if (OnClosedDel != null)
				OnClosedDel(this);

		}

		private void ControlExtended_UnLoaded(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Clean();
			Closing -= ControlExtended_UnLoaded;

			Messenger.Default.Unregister(this);

			modControls.ForEach(p=> {p.Clean(); p = null; });
			modControls.Clear();
		}

		public void Clean()
		{
			AdwancedViewModelBase viewModel = GetVM(DataContext);

			if (viewModel == null)
				return;

			viewModel.Clean();

			DataContext = null;
			viewModel = null;
		}

		private AdwancedViewModelBase GetVM(object target)
		{
			if (target == null)
				return null;

			AdwancedViewModelBase viewModel = target as AdwancedViewModelBase;
			return viewModel;
		}

		public void AddControl(ControlExtended control)
		{
			if (control == null)
				return;

			try
			{
				this.modControls.Add(control);
				control.ParentToken = Token;

				AdwancedViewModelBase dataContext = GetVM(control);

				if (dataContext == null)
					return;

				dataContext.ParentToken = Token;

				control.Initiliaze(this);
			}
			catch(Exception ex)
			{

			}
		}

	}
}
