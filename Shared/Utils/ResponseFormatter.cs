using Shared.Dto;
using Shared.Messages;

namespace Shared.Utils;

public static class ResponseFormatter
{
    public static ApiResponse<T> MakeResponse<T>(this T value, IMessageHandlerService messageHandlingService)
    {
        var response =  ApiResponse<T>.Success(value);
        
        if (!messageHandlingService.HasErrors) return response;

        var errors = GetErrors(messageHandlingService);
        var warnings = GetWarnings(messageHandlingService);
            
        response.Errors = errors;
        response.Warnings = warnings;
        
        return response;
    }

    private static List<ApiError> GetErrors(IMessageHandlerService messageHandlingService)
        => messageHandlingService.GetErrors()
            .Select(e => new ApiError
            {
                ErrorCode = e.Code,
                ErrorMessage = e.Message,
                StackTrace = e.StackTrace
            })
            .ToList();
    
    private static List<ApiWarning> GetWarnings(IMessageHandlerService messageHandlingService)
        => messageHandlingService.GetWarnings()
            .Select(e => new ApiWarning() { WarningCode = e.Code, WarningMessage = e.Message })
            .ToList();
}