using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor.Models
{
    public class ResponseData<T> where T : class
    {
        public T Data { set; get; }
        public HttpStatusCode StatusCode { get; set; }
        public ResponseData(T data, HttpStatusCode status)
        {
            Data = data;
            StatusCode = status;
        }
        public bool IsSuccess()
        {
            return StatusCode.Equals(HttpStatusCode.OK);
        }
        public string GetMessErr()
        {
            string mess = string.Empty;
            switch (StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    mess = "BadRequest";
                    break;
                case HttpStatusCode.NotFound:
                    mess = "NotFound";
                    break;
                case HttpStatusCode.InternalServerError:
                    mess = "InternalServerError";
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    mess = "ServiceUnavailable";
                    break;
            }
            return mess;
        }
    }
}
