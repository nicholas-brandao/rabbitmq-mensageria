using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;
using WebApiPublisher.Model;

namespace ConsumerApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            RabbitMq rabbitmq = new RabbitMq();

            rabbitmq.Channel.QueueDeclare(queue: "FilaRabbit",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var consumer = new EventingBasicConsumer(rabbitmq.Channel);
            
            consumer.Received += (model, ea) =>
            {

                var mensagem = Encoding.UTF8.GetString(ea.Body);

                List<Pagina> listaPaginas = JsonConvert.DeserializeObject<List<Pagina>>(mensagem);

                if (listaPaginas != null && listaPaginas.Count > 0)
                {

                    var nomeArquivo = $"{DateTime.Now.ToString("dd-MM-yyyy HH.mm")}.json";

                    Cabecalho(nomeArquivo, JsonConvert.SerializeObject(listaPaginas));
                    AdicionarArquivo(nomeArquivo, JsonConvert.SerializeObject(listaPaginas));
                    AdicionarBancoDeDados(listaPaginas);

                }
            };

            rabbitmq.Channel.BasicConsume(queue: "FilaRabbit",
            autoAck: true,
            consumer: consumer);

            Console.WriteLine("---- Consumer Iniciado ----");
            Console.WriteLine();

            Console.ReadLine();

            void Cabecalho(string nomeArquivo, string corpoMsg)
            {
                Console.WriteLine($"---- {nomeArquivo} ----");
                Console.WriteLine($"Arquivo '{nomeArquivo}' gerado!");
                Console.WriteLine();
                Console.WriteLine($"Corpo da fila: {corpoMsg}");
                Console.WriteLine();
            }

            void Rodape(){
                Console.WriteLine("--------------------");
                Console.WriteLine();

            }

            void AdicionarArquivo(string nomeArquivo, string corpoMsg)
            {

                try
                {
                    StreamWriter arquivo = new StreamWriter($"{Directory.GetCurrentDirectory()}\\ArquivosJson\\{nomeArquivo}");

                    arquivo.WriteLine(corpoMsg);
                    arquivo.Close();
                    arquivo.Dispose();

                    
                    Console.WriteLine("Arquivo Gerado com sucesso!");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Erro(e);
                }
            }

            void AdicionarBancoDeDados(List<Pagina> listaPaginas)
            {
                try
                {

                    Pagina pagina = new Pagina();
                    pagina.SalvarLista(listaPaginas);
                    

                    Console.WriteLine("Gravado no banco de dados com sucesso!");
                    Rodape();
                }
                catch (Exception e)
                {
                    Erro(e);
                }

            }

            void Erro(Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
                Rodape();
            }
        }

    }
}
