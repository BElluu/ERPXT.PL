using ERPXTpl.Enums;

namespace ERPXTpl.Models
{
    public class PrintTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public PrintTypeEnum Type { get; set; }
    }
}
