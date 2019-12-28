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
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Timers;
using power_aoi.Tools;

namespace power_aoi
{

    public partial class Main2 : Form
    {
        
        // 队列处理回调！！所有的界面操作方法写在这个函数里
        public void doWork(IModel channel, string message)
        {
            //处理完成，手动确认
            channel.BasicAck(Rabbitmq.deliveryTag, false);
            Thread.Sleep(1000);
            htmlLabel1.Invoke((Action)(() =>
            {
                htmlLabel1.Text = $"{message} is news, deal it";
            }));
            //channel.BasicNack(Rabbitmq.deliveryTag, false, true);
        }

        public delegate void WriteBoxCallback1(IModel channel, string message);
        public WriteBoxCallback1 doWorkD;
        private void getOnePcb(object source, ElapsedEventArgs e)
        {
            if (htmlLabel2.InvokeRequired)
            {
                htmlLabel2.Invoke((Action)(() =>
                {
                    htmlLabel2.Text = "OK, test event is fired at: " + DateTime.Now.ToString();
                }));
            }
        }

        public Main2()
        {
            InitializeComponent();

            //doWorkD = new WriteBoxCallback1(doWork);
            //Rabbitmq.run(doWorkD);
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000; //执行间隔时间,单位为毫秒; 这里实际间隔为10分钟  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(getOnePcb);

            //try
            //{
            //    LogHelper.WriteLog("starting");
            //    string a = "FF";
            //    int b = Convert.ToInt32(a);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteLog(ex.Message.ToString(), ex);
            //}
            

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


            //htmlLabel1.Text = "asd";
            // Create an instance of the Smart Thread Pool
            SmartThreadPool smartThreadPool = new SmartThreadPool();

            // Queue an action (Fire and forget)
            smartThreadPool.QueueWorkItem(System.IO.File.Copy,
              @"C:\Users\Administrator\Downloads\431.36-desktop-win8-win7-64bit-international-whql.exe", @"C:\Users\Administrator\AppData\Local\Temp\TestFtpServer\aaaa.exe");
            Func<int, int, string> t = (a, b) => { return a + b + "result"; };
            IWorkItemResult<string> aa = smartThreadPool.QueueWorkItem<int, int, string>(t, 1, 2);
            //htmlLabel1.Text = aa.GetResult();
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            //var aa = _db.movies.AsNoTracking().ToList();
            //var test = aa.Where(t => t.Price == 10).FirstOrDefault();

            ////声明movie类
            //List<movie> lstmovie = new List<movie> {
            //    new movie{ Title="速度与激情系列1",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列2",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列3",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列4",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列5",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列6",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列7",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列8",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //};
            //_db.movies.AddRange(lstmovie);
            //if (_db.SaveChanges() > 0) { MessageBox.Show("添加成功"); }
            //else { MessageBox.Show("添加失败"); }

            var factory = new ConnectionFactory();
            factory.AutomaticRecoveryEnabled = true;
            factory.HostName = "127.0.0.1"; // RabbitMQ服务在本地运行
            factory.UserName = "admin"; // 用户名
            factory.Password = "admin"; // 密码
            factory.VirtualHost = "my_vhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 将消息标记为持久性。
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.QueueDeclare("work", true, false, false, null); // 创建一个名称为hello的消息队列
                    string message = "asdsdsdssssssssssssssssssssssssssssssssssssssssssssssssssssss"; // 传递的消息内容
                    var body = Encoding.UTF8.GetBytes(message);
                   
                    channel.BasicPublish("", "work", properties, body); // 开始传递
                    //MessageBox.Show("已发送： {0}", message);
                }
            }
        }


        private void MetroButton4_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1"; // RabbitMQ服务在本地运行
            factory.UserName = "admin"; // 用户名
            factory.Password = "admin"; // 密码
            factory.VirtualHost = "my_vhost";
            factory.AutomaticRecoveryEnabled = true;
            htmlLabel1.Text = "sssssss";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("work", false, consumer);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        if (htmlLabel1.InvokeRequired)
                        {
                            htmlLabel1.Invoke((Action)(() =>
                            {
                                htmlLabel1.Text = $"{message} is news, deal it";
                            }));
                        
                        }
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        ////以news开头表示是新闻类型，处理完成后确认消息
                        //if (message.StartsWith("news"))
                        //{
                        //    channel.BasicAck(deliveryTag: ea.DeliveryTag, false);
                        //    if (htmlLabel1.InvokeRequired)
                        //    {
                        //        htmlLabel1.Invoke((Action)(() =>
                        //        {
                        //            htmlLabel1.Text = $"{message} is news, deal it";
                        //        }));
                        //    }
                        //    //channel.BasicAck(deliveryTag: ea.DeliveryTag, false);
                        //}
                        ////不以news开头表示不是新闻类型，不进行处理，把消息退回到queue中
                        //else
                        //{
                        //    if (htmlLabel1.InvokeRequired)
                        //    {
                        //        htmlLabel1.Invoke((Action)(() =>
                        //        {
                        //            htmlLabel1.Text = $"{message} is not news, do note deal it";
                        //        }));
                        //    }
                        //    //channel.BasicReject(ea.DeliveryTag, true);
                        //    // channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: false);
                        //}
                    };
                }
            }
        }

        private void MetroButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void MetroButton6_Click(object sender, EventArgs e)
        {
            htmlLabel1.Text = "aaaa";
        }
    }
}
