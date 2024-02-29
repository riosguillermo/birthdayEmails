using System;
using System.Collections.Generic;
using System.Net;

namespace cumples.DataModel.Entity;

public class ResponseDto<T>
{
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}
