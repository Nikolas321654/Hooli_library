using System.ComponentModel.DataAnnotations;

namespace HomeLib.API.Model.DataRequest;

public class PaginationRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}