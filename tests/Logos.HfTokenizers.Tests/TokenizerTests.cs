using NUnit.Framework;
using Shouldly;

namespace Logos.HfTokenizers.Tests;

internal sealed class TokenizerTests
{
	[Test]
	public void CountTokens()
	{
		using var tokenizer = Tokenizer.FromJson("{}");
		tokenizer.CountTokens("Hello, world!").ShouldBe(4);
	}
}
