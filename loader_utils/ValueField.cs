namespace loader_utils
{
    public struct ValueField<T>
    {
        private bool _isInited;

        private T _value;

        public T Value
        {
            set
            {
                _value = value;
                _isInited = true;
            }
            get
            {
                return _value;
            }
        }

        public bool IsInited
        {
            get
            {
                return _isInited;
            }
        }
    }
}
