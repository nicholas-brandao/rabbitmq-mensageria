using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using WebApiPublisher.Model;

namespace WebApiPublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PolicyCorsLocal")]
    public class RabbitMqPublisherController : Controller
    {
        // GET api/values
        [HttpGet]

        public ActionResult<IEnumerable<string>> Get()
        {

            var teste = HttpContext.Request;


            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post(List<Pagina> Paginas)
        {

            try
            {

                RabbitMq rabbitmq = new RabbitMq();

                rabbitmq.Channel.QueueDeclare(queue: "FilaRabbit",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);


                var msg = JsonConvert.SerializeObject(Paginas);
                var body = Encoding.UTF8.GetBytes(msg);

                rabbitmq.Channel.BasicPublish(exchange: "",
                                                    routingKey: "FilaRabbit",
                                                    basicProperties: null,
                                                    body: body);

                return StatusCode(200, JsonConvert.SerializeObject("Fila gravada com sucesso!"));

            }
            catch (RabbitMQ.Client.Exceptions.RabbitMQClientException e)
            {

                return StatusCode(400, JsonConvert.SerializeObject(e.StackTrace));
            }


        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}