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
    public class MainPageViewModel : BindableBase
    {
        private IPageDialogService _pageDialogService;

        public ICommand LabelPressCommand { get; }
        public ICommand EntryPressCommand { get; }
        public ICommand TablePressCommand { get; }

        public MainPageViewModel(IPageDialogService PageDialogService)
        {
            _pageDialogService = PageDialogService;

            LabelPressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("Label", "Long Pressed", "OK"); } );
            EntryPressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("Entry", "Long Pressed", "OK"); });
            TablePressCommand = new DelegateCommand(async () => { await _pageDialogService.DisplayAlertAsync("Table", "Long Pressed", "OK"); });
        }
    }
}
