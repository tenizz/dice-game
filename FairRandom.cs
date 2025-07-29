using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

namespace DiceGame
{
    public class FairRandom
    {
        public byte[] Key { get; private set; }
        public int ComputerNumber { get; private set; }
        public string HmacHex { get; private set; }

        public FairRandom(int range)
        {
            Key = GenerateKey();
            ComputerNumber = GenerateSecureNumber(range);
            HmacHex = ComputeHmacSha3(Key, ComputerNumber);
        }

        private byte[] GenerateKey()
        {
            byte[] key = new byte[32];
            RandomNumberGenerator.Fill(key);
            return key;
        }

        private int GenerateSecureNumber(int range)
        {
            return RandomNumberGenerator.GetInt32(0, range);
        }

        public static string ComputeHmacSha3(byte[] key, int value)
        {
            var hmac = new HMac(new Sha3Digest(256));
            hmac.Init(new KeyParameter(key));

            byte[] message = BitConverter.GetBytes(value);
            hmac.BlockUpdate(message, 0, message.Length);

            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);

            return Convert.ToHexString(result).ToLower();
        }

        public int FinalResult(int userNumber, int range) => (ComputerNumber + userNumber) % range;

        public string GetKeyHex() => Convert.ToHexString(Key).ToLower();

    }
}
