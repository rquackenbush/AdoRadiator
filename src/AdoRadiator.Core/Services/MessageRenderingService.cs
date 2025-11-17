using AdoRadiator.Core.ViewModels;
using HandlebarsDotNet;

namespace AdoRadiator.Core.Services;

public class MessageRenderingService
{
    public string RenderMessage(ProgressViewModel viewModel)
    {
        var templatePath = Path.Combine("Templates", "teams-message.mustache");

        return RenderPrivate(templatePath, viewModel);
    }

    public string RenderException(Exception exception)
    {
        var templatePath = Path.Combine("Templates", "error.mustache");

        var viewModel = new ErrorViewModel
        {
            ErrorType = exception.GetType().Name,
            ErrorMessage = exception.Message,
            ErrorDetails = exception.ToString()
        };

        return RenderPrivate(templatePath, viewModel);
    }

    private static string RenderPrivate(string path, object viewModel)
    {
        var tempateText = File.ReadAllText(path);

        var template = Handlebars.Compile(tempateText);

        return template(viewModel);
    }
}
