using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace power_aoi.Tools
{
    public class Rabbitmq
    {
        public static ulong deliveryTag = 0;
        public static statusEnum status;
        public enum statusEnum : int //四大名著枚举
        {
            IDLE = 0,
            HANDLED = 1,
            REPULSE = 2,
        };

        public static int run(Main.RabbitmqMessageCallback doWork)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = ConfigurationManager.AppSettings["HostName"].Trim(),//"192.168.31.157",
                    UserName = ConfigurationManager.AppSettings["UserName"].Trim(),//"admin",//用户名
                    Password = ConfigurationManager.AppSettings["Password"].Trim(),//"admin",//密码
                    VirtualHost = ConfigurationManager.AppSettings["VirtualHost"].Trim(),//"my_vhost"
                };
                var connection = factory.CreateConnection();
               
                var channel = connection.CreateModel();

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: true);
                #region EventingBasicConsumer

                //定义消费者                                      
                var consumer = new EventingBasicConsumer(channel);
                //接收到消息时执行的任务
                consumer.Received += (model, ea) =>
                {

                    string message = Encoding.UTF8.GetString(ea.Body);

                    if (ea.DeliveryTag != deliveryTag)
                    {
                        deliveryTag = ea.DeliveryTag;
                        doWork(channel, message);
                    }
                    else
                    {
                        deliveryTag = ea.DeliveryTag;
                        status = statusEnum.REPULSE;
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }

                };
                //处理消息
                channel.BasicConsume(queue: "work", false, consumer: consumer);
                return 1;
                #endregion
            }
            catch(Exception er)
            {
                return 0;
            }
        }
    }
}

