using System.Collections;

namespace FluentContracts.Contracts.Collections;

public class ListContract(IList argumentValue, string argumentName) 
    : CollectionContract<IList, ListContract>(argumentValue, argumentName);