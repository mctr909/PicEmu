using System;

namespace PicEmu {
	[Flags]
	public enum Status : byte {
		C = 1 << 0,
		DC = 1 << 1,
		Z = 1 << 2
	}
}
