using NUnit.Framework;
using Shouldly;

namespace Logos.HfTokenizers.Tests;

internal sealed class TokenizerTests
{
	[Test]
	public void CountTokens()
	{
		using var stream = typeof(TokenizerTests).Assembly.GetManifestResourceStream("Logos.HfTokenizers.Tests.tokenizer.json")!;
		using var reader = new StreamReader(stream);
		using var tokenizer = Tokenizer.FromJson(reader.ReadToEnd());
		tokenizer.CountTokens("Hello, world!").ShouldBe(4);
	}
}
