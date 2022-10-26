namespace Erdcsharp.Provider.Dtos
{
    public class TransactionRequestDto
    {
        public long   nonce     { get; set; }
        public string value     { get; set; }
        public string receiver  { get; set; }
        public string sender    { get; set; }
        public long   gasPrice  { get; set; }
        public long   gasLimit  { get; set; }
        public string data      { get; set; }
        public string chainID   { get; set; }
        public int    version   { get; set; }
        public string signature { get; set; }
    }
}
