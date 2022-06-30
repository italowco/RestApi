using RestApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Service
{
    public class ScopedService : IScopedService
    {
        public ScopedService()
        {
            Guid = Guid.NewGuid();
            instanceDate = DateTime.Now;

        }

        public Guid Guid { get; set; }

        public DateTime instanceDate { get; set; }



        public string GetInfo()
        {
            return $"Instancia criada em: {instanceDate}, Guid: {Guid}";
        }
    }
}
