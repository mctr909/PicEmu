using System.Collections;

namespace PicEmu {
	internal abstract class EmuBase {
		private const int IX_PCL = 0x02;
		private const int IX_STATUS = 0x03;
		private const int IX_FSR0L = 0x04;
		private const int IX_FSR0H = 0x05;
		private const int IX_FSR1L = 0x06;
		private const int IX_FSR1H = 0x07;
		private const int IX_BSR = 0x08;
		private const int IX_WREG = 0x09;
		private const int IX_PCH = 0x0A;
		private const int IX_INTCON = 0x0B;

		protected const int BankSize = 128;
		protected readonly uint[] Program;
		protected readonly byte[] Data;

		private readonly Stack CallStack = new Stack();

		protected long Clock { get; private set; } = 0;

		public int PC {
			get { return Data[IX_PCL] | Data[IX_PCH] << 8; }
			private set {
				Data[IX_PCL] = (byte)(value & 0xFF);
				Data[IX_PCH] = (byte)((value >> 8) & 0xFF);
			}
		}

		public Status STATUS => (Status)Data[IX_STATUS];

		public int FSR0 {
			get { return Data[IX_FSR0L] | Data[IX_FSR0H] << 8; }
			private set {
				Data[IX_FSR0L] = (byte)(value & 0xFF);
				Data[IX_FSR0H] = (byte)((value >> 8) & 0xFF);
			}
		}

		public int FSR1 {
			get { return Data[IX_FSR1L] | Data[IX_FSR1H] << 8; }
			private set {
				Data[IX_FSR1L] = (byte)(value & 0xFF);
				Data[IX_FSR1H] = (byte)((value >> 8) & 0xFF);
			}
		}

		public byte BSR => Data[IX_BSR];

		public byte WREG => Data[IX_WREG];

		public byte INTCON => Data[IX_INTCON];

		protected EmuBase(int programSize, int bankCount) {
			Program = new uint[programSize];
			Data = new byte[BankSize * bankCount];
		}

		protected int Execute(uint instruction) {
			var status = (Status)Data[IX_STATUS];
			var statusZ = 1 & (byte)(status & Status.Z) >> 2;
			var statusDC = 1 & (byte)(status & Status.DC) >> 1;
			var statusC = 1 & (byte)(status & Status.C);

			var opcode = (RedefinedOpCode)(instruction >> 16);
			var operand = (ushort)(instruction & 0xFFFF);
			var imm = (byte)operand;
			var bit = (byte)((operand & Operand.BIT) >> 7);
			var ix_freg = operand & Operand.FREG;
			var wreg = Data[IX_WREG];
			var freg = Data[ix_freg];

			int cycle = 1;
			int pcStep = 1;
			int temp;

			switch (opcode) {
			#region 転送命令
			case RedefinedOpCode.CLRW:
				temp = 0;
				statusZ = 1;
				break;
			case RedefinedOpCode.CLRF:
				temp = 0;
				statusZ = 1;
				break;
			case RedefinedOpCode.MOVLW:
				temp = imm;
				/* wregに反映 */
				operand = 0;
				break;
			case RedefinedOpCode.MOVWF:
				temp = wreg;
				break;
			case RedefinedOpCode.MOVF:
				temp = freg;
				statusZ = temp == 0 ? 1 : 0;
				break;
			#endregion

			#region ビット演算命令
			case RedefinedOpCode.IORWF:
				temp = freg | wreg;
				statusZ = temp == 0 ? 1 : 0;
				break;
			case RedefinedOpCode.IORLW:
				temp = imm | wreg;
				statusZ = temp == 0 ? 1 : 0;
				/* wregに反映 */
				operand = 0;
				break;
			case RedefinedOpCode.ANDWF:
				temp = freg & wreg;
				statusZ = temp == 0 ? 1 : 0;
				break;
			case RedefinedOpCode.ANDLW:
				temp = imm & wreg;
				statusZ = temp == 0 ? 1 : 0;
				/* wregに反映 */
				operand = 0;
				break;
			case RedefinedOpCode.XORWF:
				temp = freg ^ wreg;
				statusZ = temp == 0 ? 1 : 0;
				break;
			case RedefinedOpCode.XORLW:
				temp = imm ^ wreg;
				statusZ = temp == 0 ? 1 : 0;
				/* wregに反映 */
				operand = 0;
				break;
			case RedefinedOpCode.COMF:
				temp = ~freg & 0xFF;
				statusZ = temp == 0 ? 1 : 0;
				break;
			case RedefinedOpCode.SWAPF:
				temp = ((freg << 4) & 0xF0) | ((freg >> 4) & 0x0F);
				break;
			#endregion

			#region シフト命令
			case RedefinedOpCode.LSLF:
				temp = (freg << 1) & 0xFF;
				statusZ = temp == 0 ? 1 : 0;
				statusC = (freg >> 7) & 1;
				break;
			case RedefinedOpCode.LSRF:
				temp = (freg >> 1) & 0xFF;
				statusZ = temp == 0 ? 1 : 0;
				statusC = freg & 1;
				break;
			case RedefinedOpCode.ASRF:
				statusC = (freg >> 7) & 1;
				temp = (freg >> 1) | (statusC << 7);
				statusZ = temp == 0 ? 1 : 0;
				statusC = freg & 1;
				break;
			case RedefinedOpCode.RLF:
				temp = ((freg << 1) & 0xFF) | statusC;
				statusC = (freg >> 7) & 1;
				break;
			case RedefinedOpCode.RRF:
				temp = (freg >> 1) | (statusC << 7);
				statusC = freg & 1;
				break;
			#endregion

			#region 減算命令
			case RedefinedOpCode.DECFSZ:
				temp = (freg - 1) & 0xFF;
				/* PCを進めるかどうかを決定 */
				pcStep = temp == 0 ? 2 : 1;
				cycle = pcStep;
				break;
			case RedefinedOpCode.DECF:
				temp = (freg - 1) & 0xFF;
				statusZ = temp == 0 ? 1 : 0;
				break;
			case RedefinedOpCode.SUBWFB:
				statusC = 1 - statusC;
				temp = freg - wreg - statusC;
				statusZ = temp == 0 ? 1 : 0;
				statusDC = (wreg & 0xF) - (freg & 0xF) + statusC;
				statusDC = (statusDC >> 7) & 1;
				statusC = wreg - freg + statusC;
				statusC = (statusC >> 7) & 1;
				break;
			case RedefinedOpCode.SUBWF:
				temp = freg - wreg;
				statusZ = temp == 0 ? 1 : 0;
				statusDC = (wreg & 0xF) - (freg & 0xF);
				statusDC = (statusDC >> 7) & 1;
				statusC = wreg - freg;
				statusC = (statusC >> 7) & 1;
				break;
			case RedefinedOpCode.SUBLW:
				temp = imm - wreg;
				statusZ = temp == 0 ? 1 : 0;
				statusDC = (wreg & 0xF) - (imm & 0xF);
				statusDC = (statusDC >> 7) & 1;
				statusC = wreg - imm;
				statusC = (statusC >> 7) & 1;
				/* wregに反映 */
				operand = 0;
				break;
			#endregion

			#region 加算命令
			case RedefinedOpCode.INCFSZ:
				temp = (freg + 1) & 0xFF;
				/* PCを進めるかどうかを決定 */
				pcStep = temp == 0 ? 2 : 1;
				cycle = pcStep;
				break;
			case RedefinedOpCode.INCF:
				temp = (freg + 1) & 0xFF;
				statusZ = temp == 0 ? 1 : 0;
				break;
			case RedefinedOpCode.ADDWFC:
				temp = freg + wreg + statusC;
				statusZ = temp == 0 ? 1 : 0;
				statusDC = (freg & 0xF) + (wreg & 0xF) + statusC;
				statusDC = (statusDC >> 4) & 1;
				statusC = (temp >> 8) & 1;
				break;
			case RedefinedOpCode.ADDWF:
				temp = freg + wreg;
				statusZ = temp == 0 ? 1 : 0;
				statusDC = (freg & 0xF) + (wreg & 0xF);
				statusDC = (statusDC >> 4) & 1;
				statusC = (temp >> 8) & 1;
				break;
			case RedefinedOpCode.ADDLW:
				temp = imm + wreg;
				statusZ = temp == 0 ? 1 : 0;
				statusDC = (imm & 0xF) + (wreg & 0xF);
				statusDC = (statusDC >> 4) & 1;
				statusC = (temp >> 8) & 1;
				/* wregに反映 */
				operand = 0;
				break;
			#endregion

			#region ビット操作命令
			case RedefinedOpCode.BCF:
				temp = freg & ~(1 << bit);
				/* fregに反映 */
				operand |= Operand.DEST;
				break;
			case RedefinedOpCode.BSF:
				temp = freg | (1 << bit);
				/* fregに反映 */
				operand |= Operand.DEST;
				break;
			case RedefinedOpCode.BTFSC:
				/* fregの書換を回避 */
				temp = wreg;
				operand = 0;
				/* PCを進めるかどうかを決定 */
				pcStep = (freg & (1 << bit)) == 0 ? 2 : 1;
				cycle = pcStep;
				break;
			case RedefinedOpCode.BTFSS:
				/* fregの書換を回避 */
				temp = wreg;
				operand = 0;
				/* PCを進めるかどうかを決定 */
				pcStep = (freg & (1 << bit)) == 0 ? 1 : 2;
				cycle = pcStep;
				break;
			#endregion

			#region 制御命令
			case RedefinedOpCode.RETURN:
				/* fregの書換を回避 */
				temp = wreg;
				operand = 0;
				/* PCの差分 */
				pcStep = (int)(CallStack.Pop() ?? 0) - PC;
				cycle = 2;
				break;
			case RedefinedOpCode.RETLW:
				/* wregに反映 */
				temp = imm;
				operand = 0;
				/* PCの差分 */
				pcStep = (int)(CallStack.Pop() ?? 0) - PC;
				cycle = 2;
				break;
			#endregion

			default:
				/* fregの書換を回避 */
				temp = wreg;
				operand = 0;
				break;
			}

			status &= ~Status.Z;
			status &= ~Status.DC;
			status &= ~Status.C;
			status |= (Status)((int)Status.Z * statusZ);
			status |= (Status)((int)Status.DC * statusDC);
			status |= (Status)((int)Status.C * statusC);

			var dest_f = (operand & Operand.DEST) == Operand.DEST ? 1 : 0;
			var dest_w = 1 - dest_f;

			Data[IX_STATUS] = (byte)status;
			Data[IX_WREG] = (byte)(temp * dest_w + wreg * dest_f);
			Data[ix_freg] = (byte)(temp * dest_f + freg * dest_w);
			PC += pcStep;
			return cycle;
		}
	}
}
