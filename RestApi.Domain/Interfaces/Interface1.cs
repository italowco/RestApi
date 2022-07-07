using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Domain.Model.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }

        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
