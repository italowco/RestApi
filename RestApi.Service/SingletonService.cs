using RestApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Service
{
    public class SingletonService : ISingletonService
    {
        public SingletonService()
        {
            Guid = Guid.NewGuid();
            date = DateTime.Now;

        }

        public Guid Guid { get; set; }
        
        public DateTime date { get; set; }



        public string GetInfo()
        {
            return $"Instancia criada em: {date}, Guid: {Guid}";
        }
    }
}
