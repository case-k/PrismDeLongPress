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
using Android.Text.Method;

[assembly: ExportEffect(typeof(LongPressPlatformEffect), "LongPressEffect")]

namespace PrismDeLongPress.Droid.Effects
{
    class LongPressPlatformEffect : PlatformEffect
    {
        private LongPressGestureListener _listener;
        private GestureDetector _detector;


        protected override void OnAttached()
        {
            _listener = new LongPressGestureListener();
            _detector = new GestureDetector(_listener);

            var view = Control ?? Container;
            view.GenericMotion += (s, a) => _detector.OnTouchEvent(a.Event);
            view.Touch += (s, a) => _detector.OnTouchEvent(a.Event);
            _listener.View = view;

            UpdateCommand();
            UpdateCommandParameter();
        }

        protected override void OnDetached()
        {
            _detector.Dispose();
            _listener.Dispose();
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
            _listener.Command = LongPressEffect.GetCommand(Element);
        }

        void UpdateCommandParameter()
        {
            _listener.CommandParameter = LongPressEffect.GetCommandParameter(Element);
        }
    }
}