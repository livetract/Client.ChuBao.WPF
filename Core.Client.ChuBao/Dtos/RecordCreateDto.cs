namespace Core.Client.ChuBao.Dtos
{
    public class RecordCreateDto
    {
        public string Content { get; set; }
        public string Booker { get; set; }
        public Guid ContactId { get; set; }
    }
}
