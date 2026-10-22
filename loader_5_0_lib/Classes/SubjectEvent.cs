using System.Xml.Linq;

namespace loader_5_0_lib.Classes
{
    public class SubjectEvent(XElement xEvent)
    {
        public ulong OrderNum { get; } = ulong.Parse(xEvent?.Attribute("orderNum")?.Value ?? "0");
        public string EventDate { get; } = xEvent?.Attribute("eventDate")?.Value ?? string.Empty;
        public string? EventTime { get; } = xEvent?.Attribute("eventTime")?.Value;
        public string LocalName { get; } = xEvent?.Name.LocalName ?? "";
        public XElement? Xml { get; } = xEvent;
    }
}
