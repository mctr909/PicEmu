namespace PicEmu {
	public static class Operand {
		public const ushort NONE = 0b00_0000__0000_0000;
		public const ushort FSRm = 0b00_0000__0000_0011;
		public const ushort FSRn = 0b00_0000__0000_0100;
		public const ushort BANK = 0b00_0000__0001_1111;
		public const ushort FSRk = 0b00_0000__0011_1111;
		public const ushort FSRx = 0b00_0000__0100_0000;
		public const ushort FREG = 0b00_0000__0111_1111;
		public const ushort PCH  = 0b00_0000__0111_1111;
		public const ushort DEST = 0b00_0000__1000_0000;
		public const ushort IMM  = 0b00_0000__1111_1111;
		public const ushort ADRR = 0b00_0001__1111_1111;
		public const ushort BIT  = 0b00_0011__1000_0000;
		public const ushort ADRA = 0b00_0111__1111_1111;
	}
}
