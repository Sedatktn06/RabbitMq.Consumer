using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqp://localhost:5672");

//Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Kuyruk Oluşturma
//Publisher kısmındaki yapılandırma ile aynı olmalıdır. Yoksa tutarsızlık olur ve hata çıkar.
channel.QueueDeclare(queue: "test-queue", exclusive: false);

//Kuyruktan mesajı okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:"test-queue",autoAck: false,consumer);
consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yer
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();