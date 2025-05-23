namespace Belin.Core.Data.Mapping;

using System.Data;
using System.Net;

/// <summary>
/// Maps an Internet Protocol (IP) address to or from a string.
/// </summary>
public class IPAddressTypeHandler: SqlMapper.TypeHandler<IPAddress> {

	/// <summary>
	/// Assigns the value of a parameter before a command executes.
	/// </summary>
	/// <param name="parameter">The parameter to configure.</param>
	/// <param name="value">The parameter value.</param>
	public override void SetValue(IDbDataParameter parameter, IPAddress? value) =>
		parameter.Value = value?.MapToIPv6().ToString();

	/// <summary>
	/// Parses a database value back to a typed value.
	/// </summary>
	/// <param name="value">The value from the database.</param>
	/// <returns>The typed value.</returns>
	public override IPAddress? Parse(object value) =>
		value is string ipString && !string.IsNullOrWhiteSpace(ipString) ? IPAddress.Parse(ipString) : null;
}
