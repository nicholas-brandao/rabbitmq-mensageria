using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPublisher.Model
{
    [NotMapped]
    public class RabbitMq
    {


        #region propriedades

        public ConnectionFactory Factory { get; set; }
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }
        #endregion

        #region contrutores
        public RabbitMq()
        {
            Factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = Factory.CreateConnection();
            Channel = Connection.CreateModel();
        }
        #endregion
    }
}
