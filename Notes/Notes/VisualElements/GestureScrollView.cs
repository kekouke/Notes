using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Visual
{
    public class GestureScrollView : ScrollView
    {
        public event EventHandler SwipeLeft;
        public event EventHandler SwipeRight;

        public void OnSwipeLeft() => SwipeLeft?.Invoke(this, null);
        public void OnSwipeRight() => SwipeRight?.Invoke(this, null);
    }
}
