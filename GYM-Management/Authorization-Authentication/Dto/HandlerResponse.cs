namespace Authorization_Authentication.Dto;

public class HandlerResponse:IDto
{
    public static HandlerResponse EmptyResultDto()
    {
        return new HandlerResponse();
    }
}