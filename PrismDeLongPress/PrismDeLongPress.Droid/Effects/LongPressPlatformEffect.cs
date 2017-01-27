using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using PrismDeLongPress.Droid.Effects;
using Xamarin.Forms.Platform.Android;
using System.Windows.Input;
using PrismDeLongPress.Controls;

[assembly: ExportEffect(typeof(LongPressPlatformEffect), "LongPressEffect")]

namespace PrismDeLongPress.Droid.Effects
{
    class LongPressPlatformEffect : PlatformEffect
    {
        private ICommand _command;
        private object _commandParameter;
        private Android.Views.View _view;

        protected override void OnAttached()
        {
            _view = Control ?? Container;

            UpdateCommand();
            UpdateCommandParameter();
        }


        protected override void OnDetached()
        {
            var renderer = Container as IVisualElementRenderer;
            if (renderer?.Element != null)
            { 
                _view.LongClick -= OnLongClick;
                if (_view is Android.Widget.ListView)
                {
                    (_view as Android.Widget.ListView).ItemLongClick -= OnItemLongClick;
                }
            }
            _command = null;
            _commandParameter = null;
            _view = null;
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName == LongPressEffect.CommandProperty.PropertyName)
            {
                UpdateCommand();
            }
            else if (e.PropertyName == LongPressEffect.CommandParameterProperty.PropertyName)
            {
                UpdateCommandParameter();
            }
        }

        void UpdateCommand()
        {
            if (_command != null)
            {
                _view.LongClick -= OnLongClick;
            }
            _command = LongPressEffect.GetCommand(Element);
            if (_command == null)
            {
                return;
            }

            _view.LongClick += OnLongClick;

            if (_view is Android.Widget.ListView)
            {
                (_view as Android.Widget.ListView).ItemLongClick += OnItemLongClick;
            }
        }

        void UpdateCommandParameter()
        {
            _commandParameter = LongPressEffect.GetCommandParameter(Element);
        }


        void OnLongClick(object sender, Android.Views.View.LongClickEventArgs e)
        {
            if (_command == null)
            {
                e.Handled = false;
                return;
            }

            _command?.Execute(_commandParameter ?? Element);

            e.Handled = true;
        }

        void OnItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            if (_command == null)
            {
                e.Handled = false;
                return;
            }

            _command?.Execute(_commandParameter ?? Element);

            e.Handled = true;
        }

    }
}