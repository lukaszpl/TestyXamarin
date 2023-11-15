
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TestyXamarin.View.CustomListView), typeof(TestyXamarin.Droid.CustomListViewRenderer))]
namespace TestyXamarin.Droid
{
    [System.Obsolete]
    class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var listView = this.Control as Android.Widget.ListView; listView.NestedScrollingEnabled = true;
            }
        }
    }
}