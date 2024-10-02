using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Runtime.InteropServices;
using System.Text;

namespace Vendas123.Services.Services
{
    public class MessageBrokerService
    {
        private readonly ConnectionFactory _factory;
        private string Queue_Name = "";

        public MessageBrokerService()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }
        public void PostMsg(string msg, Eventos evento)
        {
            switch (evento)
            {
                case Eventos.CompraCriada:
                    Queue_Name = "Compra_Criada"; break;
                case Eventos.CompraAlterada:
                    Queue_Name = "Compra_Alterada"; break;
                case Eventos.CompraCancelada:
                    Queue_Name = "Compra_Cancelada"; break;
                case Eventos.ItemCancelado:
                    Queue_Name = "Item_Cancelado"; break;
            }
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Queue_Name,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var stringMessage = JsonConvert.SerializeObject(msg);
                    var bytes = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: Queue_Name,
                        basicProperties: null,
                        body: bytes);
                }
            }
        }
    }
}
