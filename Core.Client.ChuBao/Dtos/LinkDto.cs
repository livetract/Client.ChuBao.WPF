using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client.ChuBao.Dtos
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Complex { get; set; }
        public string Door { get; set; }
    }
}
