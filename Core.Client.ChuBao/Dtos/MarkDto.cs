using System;

namespace Core.Client.ChuBao.Dtos
{
    public class MarkDto
    {
        public Guid Id { get; set; }
        public bool HasWeChat { get; set; }
        public bool HasArrived { get; set; }
        public bool HasDeposit { get; set; }
        public bool HasContract { get; set; }
        public bool IsLose { get; set; }
        public bool IsAttention { get; set; }

        public Guid ContactId { get; set; }
    }
}
