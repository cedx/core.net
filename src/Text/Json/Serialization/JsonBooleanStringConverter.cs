namespace Belin.Core.Text.Json.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts a string to or from a boolean value.
/// </summary>
/// <param name="trueString">The string representing the boolean value <see langword="true"/>.</param>
/// <param name="falseString">The string representing the boolean value <see langword="false"/>.</param>
public class JsonBooleanStringConverter(string trueString = "True", string falseString = "False"): JsonConverter<string> {

	/// <summary>
	/// Reads and converts the boolean value to a string.
	/// </summary>
	/// <param name="reader">The JSON reader.</param>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	/// <returns>The converted value.</returns>
	public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.GetBoolean() ? trueString : falseString;

	/// <summary>
	/// Writes the specified string as a boolean value.
	/// </summary>
	/// <param name="writer">The JSON writer.</param>
	/// <param name="value">The string to convert to a boolean value.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
		writer.WriteBooleanValue(value.Trim().Equals(trueString, StringComparison.OrdinalIgnoreCase));
}
