using System.Runtime.InteropServices;

namespace Logos.HfTokenizers;

#pragma warning disable CA5392

internal sealed partial class NativeMethods
{
	private const string c_libraryName = "logoshft";

	[LibraryImport(c_libraryName, EntryPoint = "create_tokenizer", StringMarshalling = StringMarshalling.Utf8)]
	public static partial TokenizerHandle CreateTokenizer(string tokenizerJson);

	[LibraryImport(c_libraryName, EntryPoint = "destroy_tokenizer")]
	public static partial void DestroyTokenizer(IntPtr tokenizer);

	[LibraryImport(c_libraryName, EntryPoint = "count_tokens", StringMarshalling = StringMarshalling.Utf8)]
	public static partial int CountTokens(TokenizerHandle tokenizer, string text);
}
