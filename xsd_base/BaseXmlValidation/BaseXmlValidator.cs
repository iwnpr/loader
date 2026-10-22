using loader_utils;
using System.Xml;
using System.Xml.Linq;
using Xsd;

namespace xsd_base.BaseXmlValidation
{
    public abstract class BaseXmlValidator(XmlReader reader)
    {
        protected XmlReader _reader = reader;

        static bool ReadUntilClosingTag(XmlReader _reader, string closingTagName)
        {
            while (_reader.SafeRead())
            {
                if (_reader.NodeType == XmlNodeType.EndElement && _reader.Name == closingTagName)
                {
                    return true;
                }
            }
            return false;
        }

        protected abstract INotificationOfAcceptanceError GetError(string errorType, string errorMessage);

        protected abstract INotificationOfAcceptanceError GetError(IEventData eventData, string fieldName, object fieldValue, string errorMessage);

        protected abstract IEventData GetEventObject(XElement eventElement, SubjectType subjectType);

        public BaseXmlCheckResult CheckXmlStructure()
        {
            BaseXmlCheckResult result = new();
            bool hasOrderNumError = false;
            ulong lastEventNum = 0;
            int subjectCount = 0;
            int eventCount = 0;
            try
            {
                string? schemaVersion = null;
                while (_reader.SafeRead())
                {
                    if (_reader.NodeType == XmlNodeType.Element)
                    {
                        if (_reader.Name != "Document")
                        {
                            return result.SetError(GetError("1", "Головной тег не Document"));
                        }
                        schemaVersion = _reader.GetAttribute("schemaVersion");
                        if (string.IsNullOrEmpty(schemaVersion))
                        {
                            return result.SetError(GetError("1", "Атрибут schemaVersion отсутствует"));
                        }
                        break;
                    }
                }
                _reader.SafeRead();
                if (_reader.NodeType == XmlNodeType.Element && _reader.Name != "Source")
                {
                    return result.SetError(GetError("1", "Отсутствует тег Source"));
                }
                if (!ReadUntilClosingTag(_reader, "Source"))
                {
                    return result.SetError(GetError("1", "Отсутствует закрывающий тег Source"));
                }
                _reader.SafeRead();
                if (_reader.NodeType != XmlNodeType.Element || !_reader.Name.In("Data", "Data_AF"))
                {
                    return result.SetError(GetError("1", "Отсутствует тег Data/Data_AF"));
                }
                var dataElementName = _reader.Name;
                while (_reader.SafeRead())
                {
                    if (_reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (_reader.Name.In("Subject_FL", "Subject_UL"))
                        {
                            continue;
                        }
                        else if (_reader.Name == dataElementName)
                        {
                            break;
                        }
                        else
                        {
                            return result.SetError(GetError("1", $"Неизвестный закрывающий тег в блоке в блоке {dataElementName}"));
                        }
                    }

                    SubjectType subjectType = default;
                    if (_reader.NodeType == XmlNodeType.Element && _reader.Name == "Subject_FL")
                    {
                        subjectType = SubjectType.FL;
                    }
                    else if (_reader.NodeType == XmlNodeType.Element && _reader.Name == "Subject_UL")
                    {
                        subjectType = SubjectType.UL;
                    }
                    else
                    {
                        return result.SetError(GetError("1", "Отсутствует тег Subject_FL/Subject_UL"));
                    }
                    subjectCount++;

                    _reader.SafeRead();

                    if (_reader.NodeType != XmlNodeType.Element || _reader.Name != "Title")
                    {
                        return result.SetError(GetError("1", "Отсутствует тег Title"));
                    }
                    if (!ReadUntilClosingTag(_reader, "Title"))
                    {
                        return result.SetError(GetError("1", "Отсутствует закрывающий тег Title"));
                    }
                    _reader.SafeRead();
                    if (_reader.NodeType != XmlNodeType.Element || _reader.Name != "Events")
                    {
                        return result.SetError(GetError("1", "Отсутствует тег Events"));
                    }
                    _reader.SafeRead();
                    while (true)
                    {
                        if (_reader.NodeType == XmlNodeType.EndElement && _reader.Name == "Events")
                        {
                            _reader.SafeRead();
                            break;
                        }

                        if (_reader.NodeType == XmlNodeType.Element &&
                               (subjectType == SubjectType.FL && !_reader.Name.StartsWith("FL_Event")
                                   || subjectType == SubjectType.UL && !_reader.Name.StartsWith("UL_Event")))
                        {
                            return result.SetError(GetError("1", $"Неизвестный тег в блоке Events"));
                        }
                        bool isEmptyElement = _reader.IsEmptyElement;
                        var eventName = _reader.Name;
                        if (!hasOrderNumError)
                        {
                            if (!ulong.TryParse(_reader.GetAttribute("orderNum"), out ulong eventOrderNum))
                            {
                                return result.SetError(GetError("1", $"Не удалось прочитать аттриту orderNum в событии {eventName}"));
                            }
                            if (eventOrderNum == lastEventNum && eventCount > 0)
                            {
                                hasOrderNumError = true;
                                result.SetError(GetError("2", $@"Порядковый номер записи orderNum=""{eventOrderNum}"" не уникальный в пределах формируемого электронного документа"));
                            }
                            if (eventOrderNum < lastEventNum)
                            {
                                hasOrderNumError = true;
                                result.SetError(GetError("2", $@"Порядковый номер записи orderNum=""{eventOrderNum}"" не порядковый в пределах формируемого электронного документа"));
                            }
                            lastEventNum = eventOrderNum;
                        }
                        eventCount++;
                        if (eventName.In("FL_Event_3_2", "UL_Event_3_2") && !hasOrderNumError)
                        {
                            XElement eventElement = null;
                            try
                            {
                                eventElement = XElement.Parse(_reader.ReadOuterXml());
                            }
                            catch (Exception ex)
                            {
                                return result.SetError(GetError("1", $"Ошибка структуры xml. {ex.Message}"));
                            }
                            var eventXsdError = XsdHelper.ValidateEventByXsd(eventElement, schemaVersion!, subjectType);
                            if (eventXsdError != null)
                            {
                                continue;
                            }
                            ulong lastSubEventOrderNum = 0;
                            foreach (var childElement in eventElement.Elements())
                            {
                                if (!childElement.Name.ToString().Contains("Event"))
                                {
                                    continue;
                                }
                                ulong.TryParse(childElement.Attribute("orderNum")!.Value, out ulong subEventOrderNum);
                                if (subEventOrderNum == lastSubEventOrderNum && eventCount > 0)
                                {
                                    hasOrderNumError = true;
                                    var eventData = GetEventObject(eventElement, subjectType);
                                    result.SetError(GetError(eventData, "orderNum", subEventOrderNum, "OrderNum во вложенном событии должен быть порядковым"));
                                    result.Error!.orderNum_3_2 = subEventOrderNum;
                                    result.Error.orderNum_3_2Specified = true;
                                }
                                if (subEventOrderNum < lastSubEventOrderNum)
                                {
                                    hasOrderNumError = true;
                                    var eventData = GetEventObject(eventElement, subjectType);
                                    result.SetError(GetError(eventData, "orderNum", subEventOrderNum, "OrderNum во вложенном событии должен быть порядковым"));
                                    result.Error!.orderNum_3_2 = subEventOrderNum;
                                    result.Error.orderNum_3_2Specified = true;
                                }
                                lastSubEventOrderNum = subEventOrderNum;
                            }
                            continue;
                        }
                        else if (!isEmptyElement && !ReadUntilClosingTag(_reader, eventName))
                        {
                            return result.SetError(GetError("1", $"Отсутствует закрывающий тег {eventName}"));
                        }
                        _reader.SafeRead();
                    }
                }
                result.SubjectCount = subjectCount;
                result.EventCount = eventCount;
                return result;
            }
            catch (XmlReaderException ex)
            {
                return result.SetError(GetError("1", $"Ошибка структуры xml. {ex.Message}"));
            }
            catch
            {
                throw;
            }
        }
    }
}
