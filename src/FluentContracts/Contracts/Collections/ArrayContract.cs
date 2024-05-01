namespace FluentContracts.Contracts.Collections;

public class ArrayContract(Array argumentValue, string argumentName) 
    : CollectionContract<Array, ArrayContract>(argumentValue, argumentName);