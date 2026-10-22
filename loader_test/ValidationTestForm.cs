using loader_utils;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Xsd;
using xsd_base.BaseXmlValidation;

namespace loader_test
{
    public partial class ValidationTestForm : Form
    {
        public ValidationTestForm()
        {
            InitializeComponent();
            string CultureName = Thread.CurrentThread.CurrentCulture.Name;
            CultureInfo ci = new(CultureName);
            if (ci.NumberFormat.NumberDecimalSeparator != ".")
            {
                // Forcing use of decimal separator for numerical values
                ci.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = ci;
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = openFileDialog.FileName;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                tbFile.Text = openFileDialog.FileName;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFile.Text))
            {
                MessageBox.Show("Укажите путь к файлу");
                return;
            }
            tbResult.Clear();

            INotificationOfAcceptance notificationOfAcceptance = new Xsd.VersionFiveOne.Acceptance.NotificationOfAcceptance();

            XmlReaderSettings xmlReaderSettings = new()
            {
                ValidationType = ValidationType.None,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true
            };

            bool isError = false;

            try
            {
                string schemaVersion;
                using var memoryStream = File.Open(tbFile.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                using (var reader = XmlReader.Create(memoryStream, xmlReaderSettings))
                {
                    schemaVersion = DocumentHelper.GetSchemaVersion(reader);
                }
                if (schemaVersion == null)
                {
                    notificationOfAcceptance.AddError(CreateError("Атрибут schemaVersion отсутствует"));
                    return;
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var reader = XmlReader.Create(memoryStream, xmlReaderSettings))
                {
                    BaseXmlValidator baseXmlValidator = schemaVersion switch
                    {
                        "5.0" => new Xsd.VersionFiveZero.Main.BaseXmlValidator(reader),
                        "5.1" => new Xsd.VersionFiveOne.Main.BaseXmlValidator(reader),
                        _ => throw new NotImplementedException()
                    };

                    var result = baseXmlValidator.CheckXmlStructure();
                    if (result.Error != null)
                    {
                        ///Вот тут должно быть формирование квитка.
                    }
                }
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var reader = XmlReader.Create(memoryStream, xmlReaderSettings))
                {
                    XElement? documentElement = default;

                    string? readError = null;

                    using (var sw = new StringWriter(CultureInfo.InvariantCulture))
                    using (var xmlWriter = XmlWriter.Create(sw, new()
                    {
                        Indent = true
                    }))
                    {
                        while (reader.SafeRead(out readError))
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "Document")
                                {
                                    xmlWriter.WriteStartElement(reader.Name);
                                    xmlWriter.WriteAttributes(reader, default);
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.Flush();
                                    documentElement = XElement.Parse(sw.ToString());

                                    break;
                                }
                                else
                                {
                                    notificationOfAcceptance.AddError(CreateError("Головной тег не Document"));
                                    return;
                                }
                            }
                        }
                        if (documentElement == null)
                        {
                            notificationOfAcceptance.AddError(CreateError($"Ошибка структуры xml. {readError}"));
                            return;
                        }
                    }
                    var documentForXsdCheck = new XDocument(new XElement(documentElement));
                    documentForXsdCheck.Root!.Name = "DocumentRoot";
                    var documentXsdError = XsdHelper.ValidateByXsd(documentForXsdCheck, schemaVersion, SchemaSetType.DateTimeCheck);
                    if (documentXsdError != null)
                    {
                        notificationOfAcceptance.AddError(CreateError($"Головной тег не соответствует xsd. {documentXsdError}"));
                        return;
                    }

                    ObjectDeserializer objectDeserializer = schemaVersion switch
                    {
                        "5.0" => new Xsd.VersionFiveZero.Main.ObjectDeserializer(),
                        "5.1" => new Xsd.VersionFiveOne.Main.ObjectDeserializer(),
                        _ => throw new NotImplementedException()
                    };

                    if (schemaVersion.StartsWith("5.0"))
                    {
                        notificationOfAcceptance = new Xsd.VersionFiveZero.Acceptance.NotificationOfAcceptance();
                    }

                    notificationOfAcceptance.DocInfoExt.FillFields(documentElement);

                    var document = objectDeserializer.GetDocumentObject(documentElement);

                    var dateCheckError = XsdHelper.ValidateByXsd(documentForXsdCheck, schemaVersion, SchemaSetType.DateTimeCheck);
                    if (dateCheckError != null)
                    {
                        var dateError = DateErrorParser.Parse(dateCheckError);
                        if (dateError != null)
                        {
                            notificationOfAcceptance.AddError(document.CreateError(null, null, dateError.Field, dateError.Value, "Дата в головном теге меньше минимально допустимого значения"));
                            return;
                        }
                    }

                    if (!reader.SafeRead(out readError))
                    {
                        notificationOfAcceptance.AddError(document.CreateError($"Ошибка структуры xml. {readError}"));
                        return;
                    }
                    if (reader.NodeType == XmlNodeType.Element && reader.Name != "Source")
                    {
                        notificationOfAcceptance.AddError(document.CreateError("Отсутствует тег Source"));
                        return;
                    }

                    XElement? sourceElement = null;
                    try
                    {
                        sourceElement = XElement.Parse(reader.ReadOuterXml());
                    }
                    catch (Exception ex)
                    {
                        notificationOfAcceptance.AddError(document.CreateError($"Ошибка чтения тега Source. {ex.Message}"));
                        return;
                    }

                    var sourceXsdErrors = XsdHelper.ValidateByXsd(new XDocument(sourceElement), schemaVersion);
                    if (sourceXsdErrors != null)
                    {
                        notificationOfAcceptance.AddError(document.CreateError($"Часть с данными источника не соответствует xsd. {sourceXsdErrors}"));
                        return;
                    }

                    dateCheckError = XsdHelper.ValidateByXsd(new XDocument(sourceElement), schemaVersion, SchemaSetType.DateTimeCheck);
                    if (dateCheckError != null)
                    {
                        var dateError = DateErrorParser.Parse(dateCheckError);
                        if (dateError != null)
                        {
                            notificationOfAcceptance.AddError(document.CreateError(null, null, dateError.Field, dateError.Value, "Дата в данных источника меньше минимально допустимого значения"));
                            return;
                        }
                    }

                    ISource sourceItem = objectDeserializer.GetSourceObject(sourceElement);

                    var errors = sourceItem.Check(document);
                    if (errors.Count != 0)
                    {
                        notificationOfAcceptance.AddErrors(errors);
                        return;
                    }
                    if (reader.NodeType != XmlNodeType.Element || !reader.Name.In("Data", "Data_AF"))
                    {
                        notificationOfAcceptance.AddError(document.CreateError($"Отсутствует тег Data/Data_AF"));
                        return;
                    }
                    var dataElementName = reader.Name;
                    bool titleError = false;
                    while (reader.SafeRead(out readError))
                    {
                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (reader.Name.In("Subject_FL", "Subject_UL"))
                            {
                                continue;
                            }
                            else if (reader.Name == dataElementName)
                            {
                                break;
                            }
                            else
                            {
                                notificationOfAcceptance.AddError(document.CreateError($"Неизвестный закрывающий тег в блоке в блоке {dataElementName}"));
                                return;
                            }
                        }

                        SubjectType subjectType = default;
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Subject_FL")
                        {
                            if (dataElementName == "Data")
                            {
                                subjectType = SubjectType.FL;
                            }
                            else if (dataElementName == "Data_AF")
                            {
                                subjectType = SubjectType.AF;
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Subject_UL")
                        {
                            subjectType = SubjectType.UL;
                        }
                        else
                        {
                            notificationOfAcceptance.AddError(document.CreateError("Отсутствует тег Subject_FL/Subject_UL"));
                            return;
                        }

                        if (!reader.SafeRead(out readError))
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Ошибка структуры xml. {readError}"));
                            return;
                        }

                        if (reader.NodeType != XmlNodeType.Element || reader.Name != "Title")
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Отсутствует тег Title"));
                            return;
                        }

                        titleError = false;
                        XElement? titleElement = null;
                        try
                        {
                            titleElement = XElement.Parse(reader.ReadOuterXml());
                        }
                        catch (Exception ex)
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Ошибка чтения тега Title. {ex.Message}"));
                            return;
                        }

                        ISubjectTitleData? titleObject = null;
                        var titleXsdError = XsdHelper.ValidateSubjectByXsd(titleElement, schemaVersion, subjectType);
                        if (titleXsdError != null)
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Титульная часть не соответствует xsd. {titleXsdError}"));
                            titleError = true;
                        }
                        else
                        {
                            dateCheckError = XsdHelper.ValidateSubjectByXsd(titleElement, schemaVersion, subjectType, SchemaSetType.DateTimeCheck);
                            if (dateCheckError != null)
                            {
                                var dateError = DateErrorParser.Parse(dateCheckError);
                                if (dateError != null)
                                {
                                    notificationOfAcceptance.AddError(document.CreateError(null, null, dateError.Field, dateError.Value, "Дата в титульной части меньше минимально допустимого значения"));
                                    titleError = true;
                                }
                            }

                            titleObject = objectDeserializer.GetTitleObject(titleElement, subjectType);
                            errors = titleObject.Check(sourceItem);
                            if (errors.Count != 0)
                            {
                                notificationOfAcceptance.AddErrors(errors);
                                titleError = true;
                            }
                        }

                        if (reader.NodeType != XmlNodeType.Element || reader.Name != "Events")
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Отсутствует тег Events"));
                            return;
                        }
                        if (!reader.SafeRead(out readError))
                        {
                            notificationOfAcceptance.AddError(document.CreateError($"Ошибка структуры xml. {readError}"));
                            return;
                        }

                        while (true)
                        {
                            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Events")
                            {
                                if (!reader.SafeRead(out readError))
                                {
                                    notificationOfAcceptance.AddError(document.CreateError($"Ошибка структуры xml. {readError}"));
                                    return;
                                }
                                break;
                            }

                            if (reader.NodeType == XmlNodeType.Element &&
                                (subjectType == SubjectType.FL && !reader.Name.StartsWith("FL_Event")
                                    || subjectType == SubjectType.UL && !reader.Name.StartsWith("UL_Event")))
                            {
                                notificationOfAcceptance.AddError(document.CreateError($"Неизвестный тег в блоке Events"));
                                return;
                            }

                            XElement? eventElement = null;
                            var eventName = reader.Name;
                            try
                            {
                                eventElement = XElement.Parse(reader.ReadOuterXml());
                            }
                            catch (Exception ex)
                            {
                                notificationOfAcceptance.AddError(document.CreateError($"Ошибка чтения тега {eventName}. {ex.Message}"));
                                return;
                            }

                            if (!titleError)
                            {
                                var eventXsdError = XsdHelper.ValidateEventByXsd(eventElement, schemaVersion, subjectType);
                                if (eventXsdError != null)
                                {
                                    ulong.TryParse(eventElement.Attribute("orderNum")?.Value, out ulong orderNum);
                                    notificationOfAcceptance.AddError(document.CreateError($"Событие {eventName} c orderNum({orderNum}) не соответствует xsd. {eventXsdError}"));
                                    continue;
                                }

                                IEventData eventObject = objectDeserializer.GetEventObject(eventElement, subjectType);

                                dateCheckError = XsdHelper.ValidateEventByXsd(eventElement, schemaVersion, subjectType, SchemaSetType.DateTimeCheck);
                                if (dateCheckError != null)
                                {
                                    var dateError = DateErrorParser.Parse(dateCheckError);
                                    if (dateError != null)
                                    {
                                        notificationOfAcceptance.AddError(document.CreateError(eventObject, null, dateError.Field, dateError.Value, "Дата меньше минимально допустимого значения"));
                                    }
                                }

                                notificationOfAcceptance.AddErrors(eventObject.Check(sourceItem, titleObject!));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                tbResult.Text = ex.Message;
                return;
            }
            finally
            {
                if (!isError)
                {
                    notificationOfAcceptance.sendingDateTime = DateTime.Now.RemoveMilliseconds();
                    tbResult.Text = SerializeHelper.Serialize(notificationOfAcceptance);
                }
            }
        }

        private static Xsd.VersionFiveZero.Acceptance.NotificationOfAcceptanceError CreateError(string errorMessage)
        {
            return new Xsd.VersionFiveZero.Acceptance.NotificationOfAcceptanceError(errorMessage);
        }
    }
}
