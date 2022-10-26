using WalletConnectSharp.Core.Models;

public class WCRequestResponse<T>: JsonRpcResponse
{
    public T result;
}
