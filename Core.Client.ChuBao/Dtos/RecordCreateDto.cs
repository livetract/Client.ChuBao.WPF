using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Client.ChuBao.Dtos
{
    public class RecordCreateDto
    {
        [Required]
        public string Content { get; set; }
        public string Booker { get; set; }
        public Guid ContactId { get; set; }
    }
}
