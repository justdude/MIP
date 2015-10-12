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


		public StringItemViewModel SelectedPerson 
		{
			get
			{
				return mvSelectedPerson;
			}
			set
			{
				if (value == mvSelectedPerson)
					return;

				mvSelectedPerson = value;

				RaisePropertyChanged(() => this.SelectedPerson);
			}
		}

		public StringItemViewModel SelectedJob
		{
			get
			{
				return mvSelectedJob;
			}
			set
			{
				if (value == mvSelectedJob)
					return;

				mvSelectedJob = value;

				RaisePropertyChanged(() => this.SelectedJob);
			}
		}

		protected override void OnTokenChanged()
		{
			base.OnTokenChanged();

			Jobs.Items.Add(new StringItemViewModel("Work1"));
			Jobs.Items.Add(new StringItemViewModel("Work2"));

			Jobs.Items.Add(new StringItemViewModel("Ivan"));
			Jobs.Items.Add(new StringItemViewModel("Andrew"));
		}

		public override void Cleanup()
		{
			base.Cleanup();
		}
	}
}
