using MIP.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIP.MVVM.ViewModelSpecial
{
	public class StringItemViewModel : AdwancedViewModelBase
	{
		private string mvText;
		public string Text
		{
			get
			{
				return mvText;
			}
			set
			{
				if (mvText == value)
					return;

				mvText = value;

				RaisePropertyChanged(this.Text);
			}
		}

		public StringItemViewModel():base()
		{

		}

		public StringItemViewModel(string text):this()
		{
			mvText = text;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
