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

			Jobs.SelectedItemChanged += Jobs_SelectedItemChanged;
			Persons.SelectedItemChanged += Persons_SelectedItemChanged;
		}

		void Persons_SelectedItemChanged(object selectedItem)
		{
			StringItemViewModel item = selectedItem as StringItemViewModel;
			
			if (item == null)
				return;

			Jobs.Items.Clear();
			Jobs.Items.Add(new StringItemViewModel((item.Text.Contains("Ivan") ? "Programmer":"Warrior" )));
		}

		void Jobs_SelectedItemChanged(object selectedItem)
		{
		}

		public override void Cleanup()
		{
			base.Cleanup();

			Jobs.SelectedItemChanged -= Jobs_SelectedItemChanged;
			Persons.SelectedItemChanged -= Persons_SelectedItemChanged;
		}
	}
}
