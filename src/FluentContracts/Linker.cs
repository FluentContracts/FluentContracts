namespace FluentContracts
{
    public class Linker<T>
        where T : class
    {
        private readonly T _linked;

        public Linker(T linked)
        {
            Validator.CheckNotNull(linked, nameof(linked), "Argument linked cannot be null");

            _linked = linked;
        }

        public T And()
        {
            return _linked;
        }
    }
}