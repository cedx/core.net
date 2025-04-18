namespace Belin.Core.Text.Json;

using System.Text.Json.Serialization;

/// <summary>
/// When placed on a string property, specifies that its value should be converted to a boolean value.
/// </summary>
/// <param name="trueString">The string representing the boolean value <see langword="true"/>.</param>
/// <param name="falseString">The string representing the boolean value <see langword="false"/>.</param>
[AttributeUsage(AttributeTargets.Property)]
public class JsonBooleanStringAttribute(string trueString = "True", string falseString = "False"): JsonConverterAttribute {

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
		return new JsonBooleanStringConverter(TrueString, FalseString);
	}
}
