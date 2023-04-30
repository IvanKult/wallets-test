namespace dto;

public interface IPaged
{
    uint Page { get; set; }
    uint PageSize { get; set; }
}