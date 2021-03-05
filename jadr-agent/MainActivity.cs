using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.AccessibilityServices;
using Android;
using Android.Views.Accessibility;
using System.Text;
using System;
using System.Net;
using System.IO;

namespace jadr_agent
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

	[Service(Label = "jadr-agent", Permission = Manifest.Permission.BindAccessibilityService)]
	[IntentFilter(new[] { "android.accessibilityservice.AccessibilityService" })]
	[MetaData("android.accessibilityservice.AccessibilityService", Resource = "@xml/accessibility_service_config")]
	public class jadragentAccessibilityService : AccessibilityService
	{
		public override void OnAccessibilityEvent(AccessibilityEvent e)
		{
			try
			{
				// Server
				var documents = "/data/local/tmp";
				var file = Path.Combine(documents, "jadr-server.txt");

				var httpWebRequest = (HttpWebRequest)WebRequest.Create(File.ReadAllText(file));
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "POST";

				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					string json = "{\"Event Time\":\"" + e.EventTime + "\"," +
						"\"Event Type\":\"" + e.EventType + "\"," +
						"\"Content Decription\"" + e.ContentDescription + "\"," +
						"\"Package Name\":\"" + e.PackageName + "\"," +
						"\"Source\":\"" + e.Source + "\"," +
                        "\"Class Name\":\"" + e.ClassName + "\"," +
                        "\"Class\":\"" + e.Class + "\"," +
                        "\"Class Name Formatted\":\"" + e.ClassNameFormatted + "\"," +
                        "\"Action\":\"" + e.GetAction() + "\"," +
                        "\"Movement Granularity\":\"" + e.MovementGranularity + "\"," +
                        "\"Scroll X\":\"" + e.ScrollX + "\"," +
                        "\"Scroll Y\":\"" + e.ScrollY + "\"," +
                        "\"Text\":\"" + string.Join(",", e.Text) + "\"," +
                        "\"Enabled\":\"" + e.Enabled + "\"," +
                        "\"Checked\":\"" + e.Checked + "\"," +
                        "\"Window ID\":\"" + e.WindowId + "\"}";

                    println(json);

					streamWriter.Write(json);
				}

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd(); //Ignore
                }
            }
			catch (Exception ex)
			{
                println(ex.ToString());
				Console.WriteLine(ex.ToString()); // Ignore
			}
		}
		public override void OnInterrupt()
		{
			
		}
		protected override void OnServiceConnected()
		{
			var accessibilityServiceInfo = new AccessibilityServiceInfo();
			accessibilityServiceInfo.Flags = AccessibilityServiceFlags.Default;
			accessibilityServiceInfo.EventTypes = EventTypes.AllMask;
			accessibilityServiceInfo.FeedbackType = Android.AccessibilityServices.FeedbackFlags.Generic;
			SetServiceInfo(accessibilityServiceInfo);
		}

        public static void println(String Message)
        {
#if DEBUG
            Console.WriteLine("[" + DateTime.Now + "]" + " " + Message);
#endif

            try
            {
                // Directory
                // Personal folder path
                string documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;

                // Data folder path
                string dataPath = Path.Combine(documentsPath, "jadr-agent");

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                // Logfile
                dataPath = Path.Combine(dataPath, "log");

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                if (!File.Exists(Path.Combine(dataPath, "log.txt")))
                {
                    using (File.Create(Path.Combine(dataPath, "log.txt")))
                    { };
                }

                File.AppendAllText(Path.Combine(dataPath, "log.txt"), "[" + DateTime.Now + "]" + " " + Message + System.Environment.NewLine);
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
#if DEBUG
                Console.WriteLine("[" + DateTime.Now + "]" + " " + ex.ToString());
#endif
            }

        }
    }
}