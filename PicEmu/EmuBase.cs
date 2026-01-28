namespace PicEmu {
	internal abstract class EmuBase {
		protected const int BankSize = 128;
		protected readonly ushort[] Program;
		protected readonly byte[] Data;

		public int PC {
			get { return Data[0x02] | Data[0x0A] << 8; }
			private set {
				Data[0x02] = (byte)(value & 0xFF);
				Data[0x0A] = (byte)((value >> 8) & 0xFF);
			}
		}

		public byte STATUS {
			get { return Data[0x03]; }
			private set { Data[0x03] = value; }
		}

		public int FSR0 {
			get { return Data[0x04] | Data[0x05] << 8; }
			private set {
				Data[0x04] = (byte)(value & 0xFF);
				Data[0x05] = (byte)((value >> 8) & 0xFF);
			}
		}

		public int FSR1 {
			get { return Data[0x06] | Data[0x07] << 8; }
			private set {
				Data[0x06] = (byte)(value & 0xFF);
				Data[0x07] = (byte)((value >> 8) & 0xFF);
			}
		}

		public byte BSR {
			get { return Data[0x08]; }
			private set { Data[0x08] = value; }
		}

		public byte WREG {
			get { return Data[0x09]; }
			private set { Data[0x09] = value; }
		}

		public byte INTCON {
			get { return Data[0x0B]; }
			private set { Data[0x0B] = value; }
		}

		protected EmuBase(int programSize, int bankCount) {
			Program = new ushort[programSize];
			Data = new byte[BankSize * bankCount];
		}
	}
}
