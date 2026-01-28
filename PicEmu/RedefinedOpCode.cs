namespace PicEmu {
	/// <summary>
	/// <see cref="OpCode"/>を実行速度最適化のため再定義した列挙体
	/// </summary>
	public enum RedefinedOpCode : byte {
		#region 転送命令
		/// <summary><inheritdoc cref="OpCode.CLRW"/></summary>
		CLRW,
		/// <summary><inheritdoc cref="OpCode.CLRF"/></summary>
		CLRF,
		/// <summary><inheritdoc cref="OpCode.MOVLW"/></summary>
		MOVLW,
		/// <summary><inheritdoc cref="OpCode.MOVWF"/></summary>
		MOVWF,
		/// <summary><inheritdoc cref="OpCode.MOVF"/></summary>
		MOVF,
		#endregion

		#region ビット演算命令
		/// <summary><inheritdoc cref="OpCode.IORWF"/></summary>
		IORWF,
		/// <summary><inheritdoc cref="OpCode.IORLW"/></summary>
		IORLW,
		/// <summary><inheritdoc cref="OpCode.ANDWF"/></summary>
		ANDWF,
		/// <summary><inheritdoc cref="OpCode.ANDLW"/></summary>
		ANDLW,
		/// <summary><inheritdoc cref="OpCode.XORWF"/></summary>
		XORWF,
		/// <summary><inheritdoc cref="OpCode.XORLW"/></summary>
		XORLW,
		/// <summary><inheritdoc cref="OpCode.COMF"/></summary>
		COMF,
		/// <summary><inheritdoc cref="OpCode.SWAPF"/></summary>
		SWAPF,
		#endregion

		#region シフト命令
		/// <summary><inheritdoc cref="OpCode.LSLF"/></summary>
		LSLF,
		/// <summary><inheritdoc cref="OpCode.LSRF"/></summary>
		LSRF,
		/// <summary><inheritdoc cref="OpCode.ASRF"/></summary>
		ASRF,
		/// <summary><inheritdoc cref="OpCode.RLF"/></summary>
		RLF,
		/// <summary><inheritdoc cref="OpCode.RRF"/></summary>
		RRF,
		#endregion

		#region 減算命令
		/// <summary><inheritdoc cref="OpCode.DECFSZ"/></summary>
		DECFSZ,
		/// <summary><inheritdoc cref="OpCode.DECF"/></summary>
		DECF,
		/// <summary><inheritdoc cref="OpCode.SUBWFB"/></summary>
		SUBWFB,
		/// <summary><inheritdoc cref="OpCode.SUBWF"/></summary>
		SUBWF,
		/// <summary><inheritdoc cref="OpCode.SUBLW"/></summary>
		SUBLW,
		#endregion

		#region 加算命令
		/// <summary><inheritdoc cref="OpCode.INCFSZ"/></summary>
		INCFSZ,
		/// <summary><inheritdoc cref="OpCode.INCF"/></summary>
		INCF,
		/// <summary><inheritdoc cref="OpCode.ADDWFC"/></summary>
		ADDWFC,
		/// <summary><inheritdoc cref="OpCode.ADDWF"/></summary>
		ADDWF,
		/// <summary><inheritdoc cref="OpCode.ADDLW"/></summary>
		ADDLW,
		#endregion

		#region ビット操作命令
		/// <summary><inheritdoc cref="OpCode.BCF"/></summary>
		BCF,
		/// <summary><inheritdoc cref="OpCode.BSF"/></summary>
		BSF,
		/// <summary><inheritdoc cref="OpCode.BTFSC"/></summary>
		BTFSC,
		/// <summary><inheritdoc cref="OpCode.BTFSS"/></summary>
		BTFSS,
		#endregion

		#region 関節参照命令
		/// <summary><inheritdoc cref="OpCode.MOVIWn"/></summary>
		MOVIWn,
		/// <summary><inheritdoc cref="OpCode.MOVIWx"/></summary>
		MOVIWx,
		/// <summary><inheritdoc cref="OpCode.MOVWIn"/></summary>
		MOVWIn,
		/// <summary><inheritdoc cref="OpCode.MOVWIx"/></summary>
		MOVWIx,
		/// <summary><inheritdoc cref="OpCode.ADDFSR"/></summary>
		ADDFSR,
		#endregion

		#region 制御命令
		/// <summary><inheritdoc cref="OpCode.GOTO"/></summary>
		GOTO,
		/// <summary><inheritdoc cref="OpCode.BRA"/></summary>
		BRA,
		/// <summary><inheritdoc cref="OpCode.BRW"/></summary>
		BRW,
		/// <summary><inheritdoc cref="OpCode.CALL"/></summary>
		CALL,
		/// <summary><inheritdoc cref="OpCode.CALLW"/></summary>
		CALLW,
		/// <summary><inheritdoc cref="OpCode.RETURN"/></summary>
		RETURN,
		/// <summary><inheritdoc cref="OpCode.RETFIE"/></summary>
		RETFIE,
		/// <summary><inheritdoc cref="OpCode.RETLW"/></summary>
		RETLW,
		#endregion

		/// <summary><inheritdoc cref="OpCode.MOVLB"/></summary>
		MOVLB,
		/// <summary><inheritdoc cref="OpCode.MOVLP"/></summary>
		MOVLP,

		/// <summary><inheritdoc cref="OpCode.TRIS_A"/></summary>
		TRIS_A,
		/// <summary><inheritdoc cref="OpCode.TRIS_B"/></summary>
		TRIS_B,
		/// <summary><inheritdoc cref="OpCode.TRIS_C"/></summary>
		TRIS_C,

		/// <summary><inheritdoc cref="OpCode.SLEEP"/></summary>
		SLEEP,
		/// <summary><inheritdoc cref="OpCode.RESET"/></summary>
		RESET,
		/// <summary><inheritdoc cref="OpCode.CLRWDT"/></summary>
		CLRWDT,

		/// <summary><inheritdoc cref="OpCode.NOP"/></summary>
		NOP
	}
}
