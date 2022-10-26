using Erdcsharp.Configuration;
using Erdcsharp.Domain;
using Erdcsharp.Provider;
using Erdcsharp.Provider.Dtos;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using WalletConnectSharp.Unity;
using Network = Erdcsharp.Configuration.Network;

public class MaiarProvider
{
    public ElrondProvider provider { get; }

    public MaiarProvider(Network network) {
        provider = new ElrondProvider(new HttpClient(), new ElrondNetworkConfiguration(Erdcsharp.Configuration.Network.DevNet));
    }

    public async Task<Transaction> signAndSend(TransactionRequest tx) {

        SignRequestDTO signRequest = SignRequestDTO.fromTransactionRequest(tx);
        ERDRequest<SignRequestDTO> req = new ERDRequest<SignRequestDTO>("erd_sign", signRequest);
        Debug.Log(req);
        WCRequestResponse<SignResponseDTO> res = await WalletConnect
            .ActiveSession
            .SendRequestAwaitResponse<ERDRequest<SignRequestDTO>, WCRequestResponse<SignResponseDTO>>(req, req.id);

        TransactionRequestDto txRequest = tx.GetTransactionRequest();
        txRequest.signature = res.result.signature;

        var result = await provider.SendTransaction(txRequest);

        return new Transaction(result.TxHash);
    }

}
