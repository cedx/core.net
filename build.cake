using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using static System.IO.File;

var release = HasArgument("r") || HasArgument("release");
var target = Argument<string>("t", null) ?? Argument("target", "default");
var version = Context.Configuration.GetValue("package_version");

Task("build")
	.Description("Builds the project.")
	.Does(() => DotNetBuild("Core.slnx", new() { Configuration = release ? "Release" : "Debug" }));

Task("clean")
	.Description("Deletes all generated files.")
	.Does(() => EnsureDirectoryDoesNotExist("bin"))
	.DoesForEach(GetDirectories("**/obj"), EnsureDirectoryDoesNotExist)
	.Does(() => CleanDirectory("var", fileSystemInfo => fileSystemInfo.Path.Segments[^1] != ".gitkeep"));

Task("format")
	.Description("Formats the source code.")
	.DoesForEach(GetDirectories("*/src"), project => DotNetFormat(project.FullPath))
	.Does(() => DotNetFormat("test"));

Task("publish")
	.Description("Publishes the package.")
	.WithCriteria(release, @"the ""Release"" configuration must be enabled")
	.IsDependentOn("default")
	.DoesForEach(["tag", "push origin"], action => StartProcess("git", $"{action} v{version}"));

Task("test")
	.Description("Runs the test suite.")
	.Does(() => DotNetTest("Core.slnx", new() { Settings = ".runsettings" }));

Task("version")
	.Description("Updates the version number in the sources.")
	.DoesForEach(GetFiles("**/*.csproj"), file => ReplaceInFile(file, @"<Version>\d+(\.\d+){2}</Version>", $"<Version>{version}</Version>"));

Task("default")
	.Description("The default task.")
	.IsDependentOn("clean")
	.IsDependentOn("version")
	.IsDependentOn("build");

RunTarget(target);

/// <summary>
/// Replaces the specified pattern in a given file.
/// </summary>
/// <param name="file">The path of the file to be processed.</param>
/// <param name="pattern">The regular expression to find.</param>
/// <param name="replacement">The replacement text.</param>
/// <param name="options">The regular expression options to use.</param>
void ReplaceInFile(FilePath file, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, string replacement, RegexOptions options = RegexOptions.None) =>
	WriteAllText(file.FullPath, Regex.Replace(ReadAllText(file.FullPath), pattern, replacement, options));
