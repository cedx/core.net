namespace Belin.Core.Text.Json;

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts an IP address to or from a string.
/// </summary>
public class JsonIPAddressConverter(): JsonConverter<IPAddress> {

	/// <summary>
	/// Reads and converts the IP address to a string.
	/// </summary>
	/// <param name="reader">The JSON reader.</param>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	/// <returns>The converted value.</returns>
	public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.GetString() is string ipString ? IPAddress.Parse(ipString) : null;

	/// <summary>
	/// Writes the specified IP address as a string.
	/// </summary>
	/// <param name="writer">The JSON writer.</param>
	/// <param name="value">The IP address to convert to a string.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.ToString());
}
