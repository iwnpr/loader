using loader_5_0_lib.Helpers;
using System.Xml.Linq;
using Xsd;

namespace loader_5_0_lib.Classes
{
    public class UploadSubject(ulong subjectNum, XElement subjectNode, XElement xSubject, string dataNodeName)
    {
        public ulong Num { get; } = subjectNum;
        public XElement SubjectNode { get; } = subjectNode;
        public string? SubjectCode { get; } = xSubject?.Element("Title")?.Attribute("subjectCode")?.Value;
        public string? SubjectID { get; } = xSubject?.Element("Title")?.Attribute("subjectID")?.Value;
        public ulong? FirstOrderNum { get; } = xSubject?.Element("Events")?.HasElements ?? false ?
            xSubject?.Element("Events")?.Elements().First()?.Attribute("orderNum")?.Value != null ?
                ulong.Parse(xSubject?.Element("Events")?.Elements().First()?.Attribute("orderNum")?.Value ?? "0") :
                null : null;
        public string? FirstEventName { get; } = xSubject?.Element("Events") != null ?
                xSubject?.Element("Events")?.HasElements ?? false ? xSubject?.Element("Events")?.Elements().First().Name.LocalName : null : null;
        public string? LocalName { get; } = xSubject?.Name?.LocalName;
        public String DataNodeName { get; } = dataNodeName;
        public SubjectType SubjectType { get; } = dataNodeName.Equals("Data_AF") ? SubjectType.AF : (xSubject?.Name?.LocalName ?? string.Empty).Equals("Subject_FL") ? SubjectType.FL : SubjectType.UL;
        public string SubjectTypeValue { get; } = xSubject?.Name?.LocalName ?? string.Empty;
        public XElement TitleXml { get { return Xml?.Element("Title") ?? new("Title"); } }
        public HashSet<string> Events { get; } = xSubject?.Element("Events")?.Elements()?.Select(x => x.Name.LocalName).Distinct().ToHashSet() ?? [];
        public ulong EventsCount { get; } = (ulong)(xSubject?.Element("Events")?.Elements()?.Count() ?? 0);
        public string TitleHash { get; } = HashHelper.GetTitleHash(xSubject);
        public XElement? Xml { get { return xSubject; } }
        public IEnumerator<SubjectEvent> GetEnumerator()
        {
            var dro = xSubject?.Element("Events")?.Elements().Select(x => Enum.Parse<refEventNameType>(x.Name.LocalName)).Distinct().ToHashSet();
            foreach (XElement xEvent in Xml?.Element("Events")?.Elements() ?? [])
            {
                SubjectEvent subjectEvents = new(xEvent);
                yield return subjectEvents;
            }
        }
    }
}
