namespace Belin.Core.Web.UI;

using Microsoft.AspNetCore.Components;
using Web = Microsoft.AspNetCore.Components.Web;

/// <summary>
/// Provides a mechanism for rendering Razor components non-interactively as HTML markup.
/// </summary>
/// <param name="serviceProvider">The service provider.</param>
public class HtmlRenderer(IServiceProvider serviceProvider) {

	/// <summary>
	/// Renders a component in HTML format.
	/// </summary>
	/// <typeparam name="T">The component type.</typeparam>
	/// <param name="model">The view model.</param>
	/// <returns>The HTML representation of the component.</returns>
	public async Task<string> RenderComponent<T>(object? model = null) where T: IComponent {
		using var scope = serviceProvider.CreateScope();
		await using var htmlRenderer = new Web.HtmlRenderer(scope.ServiceProvider, serviceProvider.GetRequiredService<ILoggerFactory>());
		return await htmlRenderer.Dispatcher.InvokeAsync(async () => {
			var parameters = ParameterView.FromDictionary(new Dictionary<string, object?> { ["Model"] = model });
			return (await htmlRenderer.RenderComponentAsync<T>(parameters)).ToHtmlString();
		});
	}
}
