namespace PicEmu {
	public static class OpCodeMask {
		private const ushort INV = 0;
		private const ushort L03 = 0b11_1000__0000_0000;
		private const ushort L04 = 0b11_1100__0000_0000;
		private const ushort L05 = 0b11_1110__0000_0000;
		private const ushort L06 = 0b11_1111__0000_0000;
		private const ushort L07 = 0b11_1111__1000_0000;
		private const ushort L09 = 0b11_1111__1110_0000;
		private const ushort L11 = 0b11_1111__1111_1000;
		private const ushort L14 = 0b11_1111__1111_1111;

		private static readonly ushort[] OpCode00X = {
			L14, // 0b00_0000__0000
			L11, // 0b00_0000__0001
			L09, // 0b00_0000__0010
			L09, // 0b00_0000__0011

			INV, // 0b00_0000__0100
			INV, // 0b00_0000__0101
			L14, // 0b00_0000__0110
			INV, // 0b00_0000__0111

			L07, // 0b00_0000__1000
			L07, // 0b00_0000__1001
			L07, // 0b00_0000__1010
			L07, // 0b00_0000__1011

			L07, // 0b00_0000__1100
			L07, // 0b00_0000__1101
			L07, // 0b00_0000__1110
			L07  // 0b00_0000__1111
		};

		private static readonly ushort[] OpCode3X = {
			L06, // 0b11_0000
			L07, // 0b11_0001
			L05, // 0b11_0010
			L05, // 0b11_0011

			L06, // 0b11_0100
			L06, // 0b11_0101
			L06, // 0b11_0110
			L06, // 0b11_0111

			L06, // 0b11_1000
			L06, // 0b11_1001
			L06, // 0b11_1010
			L06, // 0b11_1011

			L06, // 0b11_1100
			L06, // 0b11_1101
			L06, // 0b11_1110
			L07  // 0b11_1111
		};

		public static OpCode Decode(ushort instruction) {
			instruction >>= 4;
			var code4 = instruction & 0xF;
			instruction >>= 4;
			var code8 = instruction & 0xF;
			instruction >>= 4;
			var code12 = instruction & 0xF;
			ushort mask;
			switch (code12) {
			case 0:
				switch (code8) {
				case 0:
					mask = OpCode00X[code4];
					break;
				case 1:
					mask = L07;
					break;
				default:
					mask = L06;
					break;
				}
				break;
			case 1:
				mask = L04;
				break;
			case 2:
				mask = L03;
				break;
			case 3:
				mask = OpCode3X[code8];
				break;
			default:
				mask = INV;
				break;
			}
			return (OpCode)(instruction & mask);
		}
	}
}
