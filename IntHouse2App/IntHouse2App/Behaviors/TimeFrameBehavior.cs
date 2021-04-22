using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IntHouse2App.Behaviors
{
    public class TimeFrameBehavior : Behavior<Picker>
    {
        protected override void OnAttachedTo(Picker picker)
        {
            picker.SelectedIndexChanged += OnSelectedIndexChanged;
            base.OnAttachedTo(picker);
        }

        protected override void OnDetachingFrom(Picker picker)
        {
            picker.SelectedIndexChanged -= OnSelectedIndexChanged;
            base.OnDetachingFrom(picker);
        }

        void OnSelectedIndexChanged(object sender, EventArgs args)
        {
            switch (((Picker)sender).SelectedIndex)
            {
                case 0:
                    ((Picker)sender).TextColor = Color.Purple;
                    break;
                case 1:
                    ((Picker)sender).TextColor = Color.Blue;
                    break;
                case 2:
                    ((Picker)sender).TextColor = Color.Green;
                    break;
                default:
                    ((Picker)sender).TextColor = Color.Red;
                    break;
            }
        }
    }
}
