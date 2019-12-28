using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
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

        public static void run(Main.RabbitmqMessageCallback doWork)
        {
            var factory = new ConnectionFactory()
            {
                //rabbitmq-server所在设备ip，这里就是本机
                HostName = "192.168.31.157",
                UserName = "admin",//用户名
                Password = "admin",//密码
                VirtualHost = "my_vhost"
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
            #endregion
        }
    }
}
