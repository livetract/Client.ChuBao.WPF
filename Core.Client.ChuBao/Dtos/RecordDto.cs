namespace Core.Client.ChuBao.Dtos
{
    public class RecordDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
        public string Booker { get; set; }
        public Guid ContactId { get; set; }
    }
}
