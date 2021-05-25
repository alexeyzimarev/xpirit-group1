namespace CoreLib {
    public abstract record AggregateId {
        readonly string _value;
        
        protected AggregateId(string value) => _value = value;

        public static implicit operator string(AggregateId id) => id._value;
    }
}
