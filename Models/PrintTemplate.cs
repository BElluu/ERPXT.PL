using ERPXTpl.Enums;

namespace ERPXTpl.Models
{
    public class PrintTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public PrintTypeEnum Type { get; set; }
    }
}
