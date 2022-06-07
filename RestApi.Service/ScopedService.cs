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
        }

        public Guid Guid { get; set; }


        
        public Guid GetInfo()
        {
            return Guid;
        }
    }
}
