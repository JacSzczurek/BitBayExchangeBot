using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BitBayApiClient
{
    public class BitBayApiHashGenerator : IBitBayApiHashGenerator
    {
        private string _keyPublic { get; set; }
        private string _keyPrivate { get; set; }
        public BitBayApiHashGenerator(string keyPublic, string keyPrivate)
        {
            _keyPublic = keyPublic;
            _keyPrivate = keyPrivate;
        }

        public string ComputeBitBayHash(object payload, string timeStamp)
        {           

            if (_keyPrivate == null)
                throw new ArgumentNullException(nameof(_keyPrivate));

            if (_keyPublic == null)
                throw new ArgumentNullException(nameof(_keyPublic));

            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(_keyPrivate);

            byte[] inputBytes;
            if (payload == null)
            {
                inputBytes = Encoding.UTF8.GetBytes(_keyPublic + timeStamp);
            }
            else
            {
                var jsonObject = JsonConvert.SerializeObject(payload);
                inputBytes = Encoding.UTF8.GetBytes(_keyPublic + timeStamp + jsonObject);
            }
             
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

    }

    public interface IBitBayApiHashGenerator
    {
        string ComputeBitBayHash(object payload, string timeStamp);
    }
}
