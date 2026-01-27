using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class MessageTranslation
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
