namespace Belin.Core.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// When placed on a string property, specifies that its value should be converted to a boolean value.
/// </summary>
/// <param name="trueString">The string representing the boolean value <see langword="true"/>.</param>
/// <param name="falseString">The string representing the boolean value <see langword="false"/>.</param>
[AttributeUsage(AttributeTargets.Property)]
public class JsonBooleanAttribute(string trueString = "True", string falseString = "False"): JsonConverterAttribute {

	/// <summary>
	/// The string representing the boolean value <see langword="false"/>.
	/// </summary>
	public string FalseString => falseString;

	/// <summary>
	/// The string representing the boolean value <see langword="true"/>.
	/// </summary>
	public string TrueString => trueString;

	/// <summary>
	/// Creates a JSON converter in order to pass additional state.
	/// </summary>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <returns>The custom converter.</returns>
	/// <exception cref="ArgumentOutOfRangeException">The type to convert is not a <see cref="string"/>.</exception>
	public override JsonConverter<string> CreateConverter(Type typeToConvert) {
		if (typeToConvert != typeof(string)) throw new ArgumentOutOfRangeException(nameof(typeToConvert));
		return new JsonBooleanConverter(TrueString, FalseString);
	}

	/// <summary>
	/// Converts a string to or from a boolean value.
	/// </summary>
	/// <param name="trueString">The string representing the boolean value <see langword="true"/>.</param>
	/// <param name="falseString">The string representing the boolean value <see langword="false"/>.</param>
	private class JsonBooleanConverter(string trueString, string falseString): JsonConverter<string> {

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
}
