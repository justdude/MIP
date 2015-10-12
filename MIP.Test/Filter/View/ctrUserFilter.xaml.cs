using MIP.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIP.Test.Filter.View
{
	/// <summary>
	/// Interaction logic for ctrUserFilter.xaml
	/// </summary>
	public partial class ctrUserFilter : ControlExtended
	{
		public ctrUserFilter() : base()
		{
			InitializeComponent();
			this.Loaded += ctrUserFilter_Loaded;
		}

		void ctrUserFilter_Loaded(object sender, RoutedEventArgs e)
		{
			FilterVIewModel vm = new FilterVIewModel();
			DataContext = vm;
			vm.ParentToken = ParentToken;
			vm.Token = Token;
		}

		public override void Clean()
		{
			base.Clean();
			this.Loaded -= ctrUserFilter_Loaded;
		}
	}
}
