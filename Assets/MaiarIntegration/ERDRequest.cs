using Newtonsoft.Json;
using WalletConnectSharp.Core.Utils;

public sealed class ERDRequest<T>
{
    public long id;
    public string jsonrpc;
    [JsonProperty("method")]
    private string Method;
    [JsonProperty("params")]
    private T _parameters;

    [JsonIgnore]
    public T Parameters => _parameters;

    public ERDRequest(string jsonRpcMethodName, T data) : base() {
        this.Method = jsonRpcMethodName;
        this._parameters = data;
        this.jsonrpc = "2.0";
        this.id = RpcPayloadId.Generate(); ;
    }
}
