using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FubarDev.FtpServer;
using FubarDev.FtpServer.FileSystem.DotNet;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.IO;
using Amib.Threading;
using power_aoi.Database;
using power_aoi.Modules;

namespace power_aoi
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                LogHelper.WriteLog("starting");
                string a = "FF";
                int b = Convert.ToInt32(a);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message.ToString(), ex);
            }
          

            //Console.WriteLine(aa.GetResult());
            // The action (file copy) will be done in the background by the Thread Pool
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            // Setup dependency injection
            var services = new ServiceCollection();

            // use %TEMP%/TestFtpServer as root folder
            services.Configure<DotNetFileSystemOptions>(opt => opt
                .RootPath = Path.Combine(Path.GetTempPath(), "TestFtpServer"));

            // Add FTP server services
            // DotNetFileSystemProvider = Use the .NET file system functionality
            //AnonymousMembershipProvider = allow only anonymous logins
            services.AddFtpServer(builder => builder
                .UseDotNetFileSystem() // Use the .NET file system functionality
                .EnableAnonymousAuthentication()); // allow anonymous logins

            // Configure the FTP server
            services.Configure<FtpServerOptions>(opt => opt.ServerAddress = "127.0.0.1");

            // Build the service provider
            using (var serviceProvider = services.BuildServiceProvider())
            {
                // Initialize the FTP server
                var ftpServerHost = serviceProvider.GetRequiredService<IFtpServerHost>();

                // Start the FTP server
                ftpServerHost.StartAsync(CancellationToken.None).Wait();

                //Console.WriteLine("Press ENTER/RETURN to close the test application.");
                //Console.ReadLine();

                // Stop the FTP server
                //ftpServerHost.StopAsync(CancellationToken.None).Wait();
            }
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
 

            htmlLabel1.Text = "asd";
            // Create an instance of the Smart Thread Pool
            SmartThreadPool smartThreadPool = new SmartThreadPool();

            // Queue an action (Fire and forget)
            smartThreadPool.QueueWorkItem(System.IO.File.Copy,
              @"C:\Users\Administrator\Downloads\431.36-desktop-win8-win7-64bit-international-whql.exe", @"C:\Users\Administrator\AppData\Local\Temp\TestFtpServer\aaaa.exe");
            Func<int, int, string> t = (a, b) => { return a + b + "result"; };
            IWorkItemResult<string> aa = smartThreadPool.QueueWorkItem<int, int, string>(t, 1, 2);
            htmlLabel1.Text = aa.GetResult();
        }
    }
}
