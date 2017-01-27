using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PrismDeLongPress.ViewModels
{
    public class TListItem
    {
        public string Title { get; set; }
        public string Detail { get; set; }
    }


    public class MainPageViewModel : BindableBase
    {
        private IPageDialogService _pageDialogService;

        public ICommand LabelPressCommand { get; }
        public ICommand EntryPressCommand { get; }
        public ICommand TablePressCommand { get; }
        public ICommand ListPressCommand { get; }

        public List<TListItem> ListItems { get; }

        public MainPageViewModel(IPageDialogService PageDialogService)
        {
            _pageDialogService = PageDialogService;

            LabelPressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("Label", "Long Pressed", "OK"); });
            EntryPressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("Entry", "Long Pressed", "OK"); });
            TablePressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("Table", "Long Pressed", "OK"); });
            ListPressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("List", "Long Pressed", "OK"); });

            ListItems = new List<TListItem>();
            for (int i = 0; i < 100; i++)
            {
                ListItems.Add(new TListItem() { Title = $"title-{i}", Detail = $"detail-{i}" });
            }
        }
    }
}
