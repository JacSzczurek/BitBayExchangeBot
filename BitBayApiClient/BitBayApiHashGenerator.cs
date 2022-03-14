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
        private readonly IConfiguration _configuration;

        public BitBayApiHashGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ComputeBitBayHash(object payload, string timeStamp)
        {
            var keyPrivate = _configuration["ApiKeyPrivate"];
            var keyPublic = _configuration["ApiKeyPublic"];

            if (keyPrivate == null)
                throw new ArgumentNullException(nameof(keyPrivate));

            if (keyPublic == null)
                throw new ArgumentNullException(nameof(keyPublic));

            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(keyPrivate);

            byte[] inputBytes;
            if (payload == null)
            {
                inputBytes = Encoding.UTF8.GetBytes(keyPublic + timeStamp);
            }
            else
            {
                var jsonObject = JsonConvert.SerializeObject(payload);
                inputBytes = Encoding.UTF8.GetBytes(keyPublic + timeStamp + jsonObject);
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
