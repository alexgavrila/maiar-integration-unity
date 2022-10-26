using Erdcsharp.Configuration;
using Erdcsharp.Domain;

public class SignRequestDTO
{
    public long nonce { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public string amount { get; set; }
    public long gasPrice { get; set; }
    public long gasLimit { get; set; }
    public string data { get; set; }
    public string chainId { get; set; }
    public int version { get; set; }

    public static SignRequestDTO fromTransactionRequest(TransactionRequest tx) {
        return new SignRequestDTO {
            nonce = tx.Nonce,
            from = tx.Sender.ToString(),
            to = tx.Receiver.ToString(),
            amount = tx.Value.ToString(),
            gasPrice = tx.GasPrice,
            gasLimit = tx.GasLimit.Value,
            data = tx.GetDecodedData(),
            chainId = tx._chainId,
            version = 4,
        };
    }
}