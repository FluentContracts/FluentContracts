namespace FluentContracts.Infrastructure
{
    public class Linker<T>
    {
        public Linker(Contract<T> linked)
        {
            Validator.CheckForNotNull(linked, nameof(linked), "Argument linked cannot be null");

            And = linked;
        }

        public Contract<T> And { get; }
    }
}