using Erdcsharp.Configuration;
using Erdcsharp.Domain;
using Erdcsharp.Domain.Helper;
using Erdcsharp.Domain.Values;
using Erdcsharp.Provider;
using Erdcsharp.Provider.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Core.Utils;
using WalletConnectSharp.Unity;
using Address = Erdcsharp.Domain.Address;
using Network = Erdcsharp.Configuration.Network;

public class GameManager : MonoBehaviour
{
    public Text addressText;
    public Image qrCode;
    public Text status;
    public Button button;

    private Account account;
    private MaiarProvider maiarProvider;
    public void Start() {
        maiarProvider = new MaiarProvider(Network.DevNet);
    }

    public void OnWalletConnect(WalletConnectUnitySession wc) {
        qrCode.gameObject.SetActive(false);
        addressText.gameObject.SetActive(true);
        status.gameObject.SetActive(true);
        button.gameObject.SetActive(true);

        addressText.text = wc.Accounts[0];
        account = new Account(Address.FromBech32(wc.Accounts[0]));
    }

    public async void SendTx() {
        await account.Sync(maiarProvider.provider);
        var config = await NetworkConfig.GetFromNetwork(maiarProvider.provider);

        var txRequest = TransactionRequest.CreateCallSmartContractTransactionRequest(
                                                                        config,
                                                                        account,
                                                                        Address.FromBech32("erd1qqqqqqqqqqqqqpgqkz5xj35lzy55y3weqxmx25gqllpft07jks0s97jcj3"),
                                                                        "add",
                                                                        TokenAmount.Zero(),
                                                                        NumericValue.BigUintValue(100));

        status.text = "Please sign the transaction!";
        Transaction tx = await maiarProvider.signAndSend(txRequest);
        status.text = "Signed waiting for execution";
        await tx.AwaitExecuted(maiarProvider.provider);
        status.text = "Finished";
    }
}
