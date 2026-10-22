namespace Xsd
{
    public interface IDocumentSource
    {
        public object Item
        {
            get;
        }

        public ISource Source
        {
            get
            {
                return (ISource)Item;
            }
        }
    }
}

