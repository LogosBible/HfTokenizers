using System.Runtime.InteropServices;

namespace Logos.HfTokenizers
{
	internal sealed class TokenizerHandle : SafeHandle
	{
		public TokenizerHandle()
			: base(IntPtr.Zero, true)
		{
		}

		public override bool IsInvalid => handle == IntPtr.Zero;

		protected override bool ReleaseHandle()
		{
			NativeMethods.DestroyTokenizer(handle);
			return true;
		}
	}
}
