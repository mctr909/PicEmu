using System;

namespace PicEmu {
	public static class OpCodeMask {
		private enum Mask : ushort {
			LEN00 = 0,
			LEN03 = 0b11_1000__0000_0000,
			LEN04 = 0b11_1100__0000_0000,
			LEN05 = 0b11_1110__0000_0000,
			LEN06 = 0b11_1111__0000_0000,
			LEN07 = 0b11_1111__1000_0000,
			LEN09 = 0b11_1111__1110_0000,
			LEN11 = 0b11_1111__1111_1000,
			LEN14 = 0b11_1111__1111_1111
		}

		private static readonly Mask[] OpCode00X = {
			Mask.LEN14, // 0b00_0000__0000
			Mask.LEN11, // 0b00_0000__0001
			Mask.LEN09, // 0b00_0000__0010
			Mask.LEN09, // 0b00_0000__0011

			Mask.LEN00, // 0b00_0000__0100
			Mask.LEN00, // 0b00_0000__0101
			Mask.LEN14, // 0b00_0000__0110
			Mask.LEN00, // 0b00_0000__0111

			Mask.LEN07, // 0b00_0000__1000
			Mask.LEN07, // 0b00_0000__1001
			Mask.LEN07, // 0b00_0000__1010
			Mask.LEN07, // 0b00_0000__1011

			Mask.LEN07, // 0b00_0000__1100
			Mask.LEN07, // 0b00_0000__1101
			Mask.LEN07, // 0b00_0000__1110
			Mask.LEN07  // 0b00_0000__1111
		};

		private static readonly Mask[] OpCode3X = {
			Mask.LEN06, // 0b11_0000
			Mask.LEN07, // 0b11_0001
			Mask.LEN05, // 0b11_0010
			Mask.LEN05, // 0b11_0011

			Mask.LEN06, // 0b11_0100
			Mask.LEN06, // 0b11_0101
			Mask.LEN06, // 0b11_0110
			Mask.LEN06, // 0b11_0111

			Mask.LEN06, // 0b11_1000
			Mask.LEN06, // 0b11_1001
			Mask.LEN06, // 0b11_1010
			Mask.LEN06, // 0b11_1011

			Mask.LEN06, // 0b11_1100
			Mask.LEN06, // 0b11_1101
			Mask.LEN06, // 0b11_1110
			Mask.LEN07  // 0b11_1111
		};

		public static void Decode(ushort instruction, out OpCode opcode, out ushort operand) {
			var opcM = (instruction >> 12) & 0xF;
			var opcL = (instruction >> 8) & 0xF;
			var oprM = (instruction >> 4) & 0xF;
			Mask mask;
			switch (opcM) {
			case 0:
				switch (opcL) {
				case 0:
					mask = OpCode00X[oprM];
					break;
				case 1:
					mask = Mask.LEN07;
					break;
				default:
					mask = Mask.LEN06;
					break;
				}
				break;
			case 1:
				mask = Mask.LEN04;
				break;
			case 2:
				mask = Mask.LEN03;
				break;
			case 3:
				mask = OpCode3X[opcL];
				break;
			default:
				mask = Mask.LEN00;
				break;
			}
			opcode = (OpCode)(instruction & (ushort)mask);
			operand = (ushort)(instruction & ~((ushort)mask));
		}
	}
}
