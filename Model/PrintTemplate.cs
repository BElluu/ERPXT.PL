using ERPXTpl.Enum;

namespace ERPXTpl.Model
{
    public class PrintTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public PrintType Type { get; set; }
    }
}
