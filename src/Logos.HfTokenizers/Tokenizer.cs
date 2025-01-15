namespace Logos.HfTokenizers;

public sealed class Tokenizer : IDisposable
{
	public static Tokenizer FromJson(string json) => new(NativeMethods.CreateTokenizer(json));

	public int CountTokens(string text) => NativeMethods.CountTokens(m_handle, text);

	public void Dispose() => m_handle.Dispose();

	private Tokenizer(TokenizerHandle handle)
	{
		m_handle = handle;
	}

	private readonly TokenizerHandle m_handle;
}
