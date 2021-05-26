namespace CoreLib {
    public abstract record AggregateId {
        readonly string _value;
        
        protected AggregateId(string value) {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty");
            
            _value = value;
        }

        public static implicit operator string(AggregateId id) => id._value;
    }
}
