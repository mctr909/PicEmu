namespace PicEmu {
	/// <summary>
	/// PIC命令コード
	/// </summary>
	public enum OpCode : ushort {
		#region 無オペランド命令
		NOP    = 0b00_0000__0000_0000,
		RESET  = 0b00_0000__0000_0001,
		RETURN = 0b00_0000__0000_1000,
		RETFIE = 0b00_0000__0000_1001,
		CALLW  = 0b00_0000__0000_1010,
		BRW    = 0b00_0000__0000_1011,
		SLEEP  = 0b00_0000__0110_0011,
		CLRWDT = 0b00_0000__0110_0100,
		TRIS_A = 0b00_0000__0110_0101,
		TRIS_B = 0b00_0000__0110_0110,
		TRIS_C = 0b00_0000__0110_0111,
		#endregion

		#region ALU命令
		/// <summary>MOVe Wreg to Freg</summary>
		MOVWF  = 0b00_0000__1000_0000,
		/// <summary>CLeaR Wreg</summary>
		CLRW   = 0b00_0001__0000_0000,
		/// <summary>CLeaR Freg</summary>
		CLRF   = 0b00_0001__1000_0000,
		/// <summary>freg - wreg</summary>
		SUBWF  = 0b00_0010__0000_0000,
		/// <summary>DECrement Freg</summary>
		DECF   = 0b00_0011__0000_0000,
		/// <summary>freg OR wreg</summary>
		IORWF  = 0b00_0100__0000_0000,
		/// <summary>freg AND wreg</summary>
		ANDWF  = 0b00_0101__0000_0000,
		/// <summary>freg XOR wreg</summary>
		XORWF  = 0b00_0110__0000_0000,
		/// <summary>freg + wreg</summary>
		ADDWF  = 0b00_0111__0000_0000,
		/// <summary>MOVe Freg</summary>
		MOVF   = 0b00_1000__0000_0000,
		/// <summary>COMplement Freg</summary>
		COMF   = 0b00_1001__0000_0000,
		/// <summary>INCrement Freg</summary>
		INCF   = 0b00_1010__0000_0000,
		/// <summary>DECrement Freg, Skip if Zero</summary>
		DECFSZ = 0b00_1011__0000_0000,
		/// <summary>Rotate Right Freg through carry</summary>
		RRF    = 0b00_1100__0000_0000,
		/// <summary>Rotate Left Freg through carry</summary>
		RLF    = 0b00_1101__0000_0000,
		/// <summary>SWAP nibbles in Freg</summary>
		SWAPF  = 0b00_1110__0000_0000,
		/// <summary>INCrement Freg, Skip if Zero</summary>
		INCFSZ = 0b00_1111__0000_0000,

		/// <summary>Bit Clear Freg</summary>
		BCF    = 0b01_0000__0000_0000,
		/// <summary>Bit Set Freg</summary>
		BSF    = 0b01_0100__0000_0000,
		/// <summary>Bit Test Freg, Skip if Clear</summary>
		BTFSC  = 0b01_1000__0000_0000,
		/// <summary>Bit Test Freg, Skip if Set</summary>
		BTFSS  = 0b01_1100__0000_0000,

		/// <summary>MOVe Literal to Wreg</summary>
		MOVLW  = 0b11_0000__0000_0000,
		/// <summary>RETurn with Literal in Wreg</summary>
		RETLW  = 0b11_0100__0000_0000,
		/// <summary>Logical Shift Left Freg</summary>
		LSLF   = 0b11_0101__0000_0000,
		/// <summary>Logical Shift Right Freg</summary>
		LSRF   = 0b11_0110__0000_0000,
		/// <summary>Arithmetic Shift Right Freg</summary>
		ASRF   = 0b11_0111__0000_0000,
		/// <summary>literal OR wreg</summary>
		IORLW  = 0b11_1000__0000_0000,
		/// <summary>literal AND wreg</summary>
		ANDLW  = 0b11_1001__0000_0000,
		/// <summary>literal XOR wreg</summary>
		XORLW  = 0b11_1010__0000_0000,
		/// <summary>freg - wreg - (1 - carry)</summary>
		SUBWFB = 0b11_1011__0000_0000,
		/// <summary>literal - wreg</summary>
		SUBLW  = 0b11_1100__0000_0000,
		/// <summary>freg + wreg + carry</summary>
		ADDWFC = 0b11_1101__0000_0000,
		/// <summary>literal + wreg</summary>
		ADDLW  = 0b11_1110__0000_0000,
		#endregion

		/// <summary>レジスタバンク設定</summary>
		MOVLB  = 0b00_0000__0010_0000,
		/// <summary>PC上位設定</summary>
		MOVLP  = 0b11_0001__1000_0000,

		/// <summary>絶対アドレスジャンプ</summary>
		CALL   = 0b10_0000__0000_0000,
		/// <summary>絶対アドレスサブルーチンコール</summary>
		GOTO   = 0b10_1000__0000_0000,
		/// <summary>相対アドレスジャンプ</summary>
		BRA    = 0b11_0010__0000_0000,

		#region 関節参照命令
		/// <summary></summary>
		MOVIWn = 0b00_0000__0001_0000,
		/// <summary></summary>
		MOVWIn = 0b00_0000__0001_1000,
		/// <summary></summary>
		ADDFSR = 0b11_0001__0000_0000,
		/// <summary></summary>
		MOVIWx = 0b11_1111__0000_0000,
		/// <summary></summary>
		MOVWIx = 0b11_1111__1000_0000,
		#endregion

		INVALID = 0xFFFF
	}
}
