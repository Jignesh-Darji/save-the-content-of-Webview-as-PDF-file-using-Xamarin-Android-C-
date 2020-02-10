using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.IO;
using System;
using System.Text;
using Android.Webkit;
using Android.Print;
using Android.Views;
using AlertDialog = Android.App.AlertDialog;


namespace App4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btn1;
        private WebView myWebView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btn1 = FindViewById<Button>(Resource.Id.button1);
            btn1.Click += Button_Clicked;
        }
        private void CreatePrintWebView(View printView)
        {
            WebView webView = printView.FindViewById<WebView>(Resource.Id.printWebView);

            string htmlDocument =
      "<html><body><h1>Xamarin Android Print Test</h1><p>"
        + "This is some sample page content.</p></body></html>";

            webView.LoadDataWithBaseURL(null, htmlDocument,
                    "text/HTML", "UTF-8", null);
        }
        private void LoadPrintWebVIew()
        {
            // PrescriptionView is the design view
            var printView = LayoutInflater.Inflate(Resource.Layout.print_view, null);
            CreatePrintWebView(printView);
            Android.App.AlertDialog dialog = null;
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            alert.SetView(printView);

            alert.SetPositiveButton("Print", (senderAlert, args) =>
            {
                var webView = printView.FindViewById<WebView>(Resource.Id.printWebView);
                string fileName = "MyPrintFile_" + Guid.NewGuid().ToString() + ".pdf";
                var printMgr = (PrintManager)GetSystemService(MainActivity.PrintService);
                printMgr.Print("MyPrintJob", webView.CreatePrintDocumentAdapter(fileName), new PrintAttributes.Builder().Build());
            });

            alert.SetNegativeButton("Close", (senderAlert, args) =>
            {
                dialog.Dismiss();
            });

            dialog = alert.Create();
            dialog.Show();
            // dialogPrescription.Window.SetLayout(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
        }

        /// <summary>
        /// Save or print the PDF file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            LoadPrintWebVIew();
        }

    }
}