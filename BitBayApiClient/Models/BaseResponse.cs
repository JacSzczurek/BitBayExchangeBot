using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBayApiClient.Models
{
    public class BaseResponse
    {
        public string Status { get; set; }
        public List<ErrorMessagesEnum> Errors { get; set; } 
    }
}
