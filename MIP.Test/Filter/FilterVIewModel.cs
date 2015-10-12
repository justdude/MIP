using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MIP.MVVM;
using MIP.Test.ViewModel;
using MIP.MVVM.ViewModelsBase.Collections;
namespace MIP.Test.Filter
{
	public class FilterVIewModel : AdwancedViewModelBase
	{
		private StringItemViewModel mvSelectedPerson;
		private StringItemViewModel mvSelectedJob;

		public FilterVIewModel()
		{
			Jobs = new ListCollectionViewModel<StringItemViewModel>();
			Persons = new ListCollectionViewModel<StringItemViewModel>();
		}

		public ListCollectionViewModel<StringItemViewModel> Persons
		{ get; set; }

		public ListCollectionViewModel<StringItemViewModel> Jobs
		{ get; set; }

		protected override void OnTokenChanged()
		{
			base.OnTokenChanged();

			Jobs.Items.Add(new StringItemViewModel("Work1"));
			Jobs.Items.Add(new StringItemViewModel("Work2"));

			Persons.Items.Add(new StringItemViewModel("Ivan"));
			Persons.Items.Add(new StringItemViewModel("Andrew"));
		}

		public override void Cleanup()
		{
			base.Cleanup();
		}
	}
}
