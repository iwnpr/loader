using loader_utils;
using System.Xml.Serialization;

namespace Xsd.VersionFour.Main
{
    public partial class FL_18_Deal_Type
    {
        ValueField<bool?> partialExistField;
        ValueField<string?> transferUidField;
        ValueField<bool?> creditLineExistField;
        ValueField<ushort?> creditLineCodeField;
        ValueField<bool?> consumerExistField;
        ValueField<bool?> cardExistField;
        ValueField<bool?> novationExistField;
        ValueField<bool?> monetarySourceExistField;
        ValueField<bool?> monetarySubjectExistField;
        ValueField<bool?> floatRateExistField;
        ValueField<bool?> partialTransferExistField;
        ValueField<bool?> repaymentFactField;
        ValueField<bool?> transferFactField;
        ValueField<bool?> partnerFinancingFactField;

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
        public bool? creditLineExist
        {
            get
            {
                if (!creditLineExistField.IsInited)
                    creditLineExistField.Value = Items1.GetBool(ItemsElementName, "creditLineExist");
                return creditLineExistField.Value;
            }
        }



        [XmlIgnore]
        public ushort? creditLineCode
        {
            get
            {
                if (!creditLineCodeField.IsInited)
                    creditLineCodeField.Value = Items1.GetUshort(ItemsElementName, "creditLineCode");
                return creditLineCodeField.Value;
            }
        }

        [XmlIgnore]
        public bool? consumerExist
        {
            get
            {
                if (!consumerExistField.IsInited)
                    consumerExistField.Value = Item.GetBool(ItemElementName, "consumerExist");
                return consumerExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? cardExist
        {
            get
            {
                if (!cardExistField.IsInited)
                    cardExistField.Value = Item1.GetBool(Item1ElementName, "cardExist");
                return cardExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? novationExist
        {
            get
            {
                if (!novationExistField.IsInited)
                    novationExistField.Value = Item2.GetBool(Item2ElementName, "novationExist");
                return novationExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? monetarySourceExist
        {
            get
            {
                if (!monetarySourceExistField.IsInited)
                    monetarySourceExistField.Value = Item3.GetBool(Item3ElementName, "monetarySourceExist");
                return monetarySourceExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? monetarySubjectExist
        {
            get
            {
                if (!monetarySubjectExistField.IsInited)
                    monetarySubjectExistField.Value = Item4.GetBool(Item4ElementName, "monetarySubjectExist");
                return monetarySubjectExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? floatRateExist
        {
            get
            {
                if (!floatRateExistField.IsInited)
                    floatRateExistField.Value = Item5.GetBool(Item5ElementName, "floatRateExist");
                return floatRateExistField.Value;
            }
        }



        [XmlIgnore]
        public bool? partialTransferExist
        {
            get
            {
                if (!partialTransferExistField.IsInited)
                    partialTransferExistField.Value = Item6.GetBool(Item6ElementName, "partialTransferExist");
                return partialTransferExistField.Value;
            }
        }

        [XmlIgnore]
        public bool? repaymentFact
        {
            get
            {
                if (!repaymentFactField.IsInited)
                    repaymentFactField.Value = Item7.GetBool(Item7ElementName, "repaymentFact");
                return repaymentFactField.Value;
            }
        }

        [XmlIgnore]
        public bool? transferFact
        {
            get
            {
                if (!transferFactField.IsInited)
                    transferFactField.Value = Item8.GetBool(Item8ElementName, "transferFact");
                return transferFactField.Value;
            }
        }

        [XmlIgnore]
        public bool? partnerFinancingFact
        {
            get
            {
                if (!partnerFinancingFactField.IsInited)
                    partnerFinancingFactField.Value = Item9.GetBool(Item9ElementName, "partnerFinancingFact");
                return partnerFinancingFactField.Value;
            }
        }
    }
}
