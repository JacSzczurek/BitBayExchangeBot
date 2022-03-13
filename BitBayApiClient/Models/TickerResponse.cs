using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBayApiClient.Models
{
    public class TickerResponse : BaseResponse
    {
        public Ticker? Ticker { get; set; }
    }
}
