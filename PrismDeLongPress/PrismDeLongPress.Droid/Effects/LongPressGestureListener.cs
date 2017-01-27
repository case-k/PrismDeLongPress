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
using System.Windows.Input;

namespace PrismDeLongPress.Droid.Effects
{
    internal class LongPressGestureListener : GestureDetector.SimpleOnGestureListener
    {
        public ICommand Command { private get; set; }
        public object CommandParameter { private get; set; }
        public Android.Views.View View { private get; set; }


        public override void OnLongPress(MotionEvent e)
        {
            base.OnLongPress(e);

            if (Command == null)
            {
                return;
            }

            Command?.Execute(CommandParameter ?? View);
        }
    }
}