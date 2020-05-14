using Android.Content;
using Anunturi.Mobile.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPicker))]
namespace Anunturi.Mobile.Droid
{
    public class CustomPicker : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public CustomPicker(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetHintTextColor(Android.Graphics.Color.Rgb(211, 211, 211));
                Control.SetTextColor(Android.Graphics.Color.Rgb(255, 255, 255));
                Control.TextSize = 13;
            }
        }

    }

}