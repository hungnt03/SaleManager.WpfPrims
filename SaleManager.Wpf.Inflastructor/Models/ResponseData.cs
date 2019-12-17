using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor.Models
{
    public class ResponseData 
    {
        public Object Data { set; get; }
        public string Messages { get; set; }
        public ResponseData(Object data, string mess = "")
        {
            this.Data = data;
            this.Messages = mess;
        }
    }
}
