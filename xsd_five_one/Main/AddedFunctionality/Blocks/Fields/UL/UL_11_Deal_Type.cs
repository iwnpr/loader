using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFiveOne.Main
{
    public partial class UL_11_Deal_Type
    {
        ValueField<bool?> cardExistField;
        ValueField<bool?> novationExistField;
        ValueField<bool?> monetarySourceExistField;
        ValueField<bool?> monetarySubjectExistField;
        ValueField<bool?> partialExistField;
        ValueField<string?> transferUidField;
        ValueField<ushort?> creditLineCodeField;
        ValueField<bool?> creditLineExistField;
        ValueField<bool?> floatRateExistField;
        ValueField<bool?> partialTransferExistField;
        ValueField<bool?> transferFactField;
        ValueField<bool?> partnerFinancingFactField;

        [XmlIgnore]
        public bool? cardExist
        {
            get
            {
                if (!cardExistField.IsInited)
                    cardExistField.Value = Item.GetBool(ItemElementName, "cardExist");
                return cardExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? novationExist
        {
            get
            {
                if (!novationExistField.IsInited)
                    novationExistField.Value = Item1.GetBool(Item1ElementName, "novationExist");
                return novationExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? monetarySourceExist
        {
            get
            {
                if (!monetarySourceExistField.IsInited)
                    monetarySourceExistField.Value = Item2.GetBool(Item2ElementName, "monetarySourceExist");
                return monetarySourceExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? monetarySubjectExist
        {
            get
            {
                if (!monetarySubjectExistField.IsInited)
                    monetarySubjectExistField.Value = Item3.GetBool(Item3ElementName, "monetarySubjectExist");
                return monetarySubjectExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? partialExist
        {
            get
            {
                if (!partialExistField.IsInited)
                    partialExistField.Value = Items.GetBool(ItemsElementName, "partialExist");
                return partialExistField.Value;
            }
        }

        [XmlIgnore]
        public string? transferUid
        {
            get
            {
                if (!transferUidField.IsInited)
                    transferUidField.Value = Items.GetString(ItemsElementName, "transferUid");
                return transferUidField.Value;
            }
        }

        [XmlIgnore]
        public ushort? creditLineCode
        {
            get
            {
                if (!creditLineCodeField.IsInited)
                    creditLineCodeField.Value = Items1.GetUshort(Items1ElementName, "creditLineCode");
                return creditLineCodeField.Value;
            }
        }

        [XmlIgnore]
        public bool? creditLineExist
        {
            get
            {
                if (!creditLineExistField.IsInited)
                    creditLineExistField.Value = Items1.GetBool(Items1ElementName, "creditLineExist");
                return creditLineExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? floatRateExist
        {
            get
            {
                if (!floatRateExistField.IsInited)
                    floatRateExistField.Value = Item4.GetBool(Item4ElementName, "floatRateExist");
                return floatRateExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? partialTransferExist
        {
            get
            {
                if (!partialTransferExistField.IsInited)
                    partialTransferExistField.Value = Item5.GetBool(Item5ElementName, "partialTransferExist");
                return partialTransferExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? transferFact
        {
            get
            {
                if (!transferFactField.IsInited)
                    transferFactField.Value = Item6.GetBool(Item6ElementName, "transferFact");
                return transferFactField.Value;
            }
        }

        [XmlIgnore]
        public bool? partnerFinancingFact
        {
            get
            {
                if (!partnerFinancingFactField.IsInited)
                    partnerFinancingFactField.Value = Item7.GetBool(Item7ElementName, "partnerFinancingFact");
                return partnerFinancingFactField.Value;
            }
        }
    }
}