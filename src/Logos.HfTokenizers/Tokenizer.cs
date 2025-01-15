namespace Logos.HfTokenizers;

public sealed class Tokenizer : IDisposable
{
	public static Tokenizer FromJson(string json) => new();

	public int CountTokens(string text) => (text.Length + 3) / 4;

	public void Dispose()
	{
	}
}
