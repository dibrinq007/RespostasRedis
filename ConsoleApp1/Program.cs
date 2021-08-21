using System;
using StackExchange.Redis;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Redis");

            string connectionString = "localhost";
            var redis = ConnectionMultiplexer.Connect(connectionString);
            var db = redis.GetDatabase();
            var sub = redis.GetSubscriber();
            

            sub.Subscribe("perguntas").OnMessage(
                m => {
                    var pergunta = m.Message.ToString().Split(':', '?', ' ', '+');
                    var keyPergunta = pergunta[0];
                    var conta = Convert.ToString(Convert.ToInt32(pergunta[4]) + Convert.ToInt32(pergunta[5]));
                    
                    db.HashSet(keyPergunta, "Grupo02", conta);

                });
            
          Console.ReadLine();
        }
    }
}
